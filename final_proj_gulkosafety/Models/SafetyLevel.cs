using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class SafetyLevel
    {
        int reportNum;
        project project;
        List<defect_for_algo> currentReportDefects;
        List<defect_in_report> previousReportDefects;
        double[] projReportGrades;
        List<proj_type_weight> projTypeWeights;
        double reportGrade;
        double safetyLvlGrade;
        string alert;

        public int ReportNum { get => reportNum; set => reportNum = value; }
        public project Project { get => project; set => project = value; }
        public List<defect_for_algo> CurrentReportDefects { get => currentReportDefects; set => currentReportDefects = value; }
        public List<defect_in_report> PreviousReportDefects { get => previousReportDefects; set => previousReportDefects = value; }
        public double[] ProjReportGrades { get => projReportGrades; set => projReportGrades = value; }
        public List<proj_type_weight> ProjTypeWeights { get => projTypeWeights; set => projTypeWeights = value; }
        public double ReportGrade { get => reportGrade; set => reportGrade = value; }
        public double SafetyLvlGrade { get => safetyLvlGrade; set => safetyLvlGrade = value; }
        public string Alert { get => alert; set => alert = value; }

        public SafetyLevel() { }

        public SafetyLevel(int reportNum, project project, List<defect_for_algo> currentReportDefects, List<defect_in_report> previousReportDefects, double[] projReportGrades, List<proj_type_weight> projTypeWeights)
        {
            ReportNum = reportNum;
            Project = project;
            CurrentReportDefects = currentReportDefects;
            PreviousReportDefects = previousReportDefects;
            ProjReportGrades = projReportGrades;
            ProjTypeWeights = projTypeWeights;
        }

        public SafetyLevel calcGradeAndSafetyLVL()
        {

            //gradesARR.pop();
            const int maxGradeForDefectType = 25;
            //מספר הנקודות שיורדות כתוצאה מבדיקת כללים בדוח הנוכחי
            const int point = 10;
            const int maxSevereDefects = 2;
            const int maxdefects = 9;
            const int minGradeAllow = 75;
            double report_Grade = 0;
            double safetyLvL_Grade = 0;
            double total = 0;

            int pointTOreduce = 0;
            int pointsOnDefectGrade10 = 0;

            List<defect_for_algo> severeDefects = new List<defect_for_algo>();
            List<defect_for_algo> TenGradeDefects = new List<defect_for_algo>();
            List<defect_for_algo> returnDefects = new List<defect_for_algo>();
            List<defect_in_report> notFixedDefects = new List<defect_in_report>();

            //calculate report grade
            for (int i = 0; i < ProjTypeWeights.Count; i++)
            {
                double sum = 0;
                //runs over current report defects and sum the defects grades
                for (int j = 0; j < CurrentReportDefects.Count; j++)
                {
                    if (ProjTypeWeights[i].Defect_type_num == CurrentReportDefects[j].Defect_type_num)
                    {
                        sum += CurrentReportDefects[j].Grade;

                        if (CurrentReportDefects[j].Grade >= 9)
                        { //counting severe defects = 9 or 10 grade
                            severeDefects.Add(CurrentReportDefects[j]);
                            if (CurrentReportDefects[j].Grade == 10)
                            { //reduce 5 points for every 10 grade defect
                                pointsOnDefectGrade10 = pointsOnDefectGrade10 + 5;
                                //counting 10 grade defects
                                TenGradeDefects.Add(CurrentReportDefects[j]);
                            }
                        }

                    }
                    //checking return defects from last week
                    if (i < 1)//loop runs 1 time only
                    {
                        for (var f = 0; f < PreviousReportDefects.Count; f++)
                        {
                            if (PreviousReportDefects[f].Defect_num == CurrentReportDefects[j].Defect_num)
                            {
                                returnDefects.Add(CurrentReportDefects[j]);

                                switch (CurrentReportDefects[j].Grade)
                                {//reduce points for return def by severe
                                    case 1 - 5:
                                        pointTOreduce += 1;
                                        break;
                                    case 6 - 8:
                                        pointTOreduce += 3;
                                        break;
                                    default:
                                        pointTOreduce += 5;
                                        break;
                                }

                            }

                        }
                    }

                }
  
                //if sum of all defects in same category > 25, the category gets max weight
                if (sum > maxGradeForDefectType)
                {
                    sum = 100;
                }
                //calc total grade of defect type and add to report grade
                total = sum * ProjTypeWeights[i].Weight;
                report_Grade += total;
            }

            //reducing 10 grade points from report grade
            report_Grade = report_Grade + pointsOnDefectGrade10;

            //reducing severe def points from report grade
            if (severeDefects.Count >= maxSevereDefects)
            {
                report_Grade += point;
            }

            //reducing reached max defects points from report grade
            if (CurrentReportDefects.Count > maxdefects)
            {
                report_Grade += point;
            }

            //calc final report grade
            report_Grade = 100 - report_Grade;

            //start calc safety level
            //אם ליקוי לא תוקן משבוע שעבר ועבר לו תאריך התיקון נוסיף 2 מקודות להורדת הציון הכללית
            for (var p = 0; p < PreviousReportDefects.Count; p++)
            {
                DateTime date = DateTime.UtcNow.Date;
                if (PreviousReportDefects[p].Fix_status == 0 && PreviousReportDefects[p].Fix_date < date)
                {
                    pointTOreduce += 2;
                    notFixedDefects.Add(PreviousReportDefects[p]);
                }
            }

            //calc sum of all reports grades
            double sumOfAllReports = 0;
            foreach (int grade in ProjReportGrades)
            {
                sumOfAllReports += grade;
            }

            //calc reports avg
            double avg = Math.Round((sumOfAllReports + report_Grade) / (ProjReportGrades.Length + 1), 2);

            //avg is base grade, reducing points from rules
            safetyLvL_Grade = avg - pointTOreduce;

            //if current report grade higher in 10 points than the last one- add points
            if (ProjReportGrades.Length>0)
            {
                if((report_Grade - ProjReportGrades[ProjReportGrades.Length - 1]) >= 10)
                {
                    safetyLvL_Grade = safetyLvL_Grade + 5;
                }
            }
            report_Grade = Math.Round(report_Grade, 0);
            safetyLvL_Grade = Math.Round(safetyLvL_Grade, 0);
            UpdateReportGrade(ReportNum, report_Grade);
            UpdateProjectSafetyLvl(Project.Project_num, safetyLvL_Grade);

            //build alerts in html format
            string alretHtml = "<h2>סיכום עדכון רמת בטיחות בפרויקט " + Project.Name + "</h2></br>";
            alretHtml += "<h3>ציון דוח ביקור מספר " + ReportNum + ": " + report_Grade + "</h3>";
            alretHtml += "<h3>ציון רמת בטיחות פרויקט: " + safetyLvL_Grade + "</h3><br />";
            alretHtml += "<h3>פירוט ממצאים דוח ביקור:</h3>";
            
            //check if safety level is under 75 = risky project!
            if (safetyLvL_Grade <= minGradeAllow)
                alretHtml += "<p>שים לב! ציון הפרויקט חורג מרמת הבטיחות המינימלית שהוגדרה. שקול-העברת הדרכת בטיחות לעובדים ולמנהל העבודה, ביקור פתע באתר, פיזור שלטי בטיחות באתר וצעדי מניעה נוספים./p>";

            //if num of defects bigger than 9 reduce points
            if (CurrentReportDefects.Count > maxdefects)
            {
                alretHtml += "<p>נמצא מספר חריג של ליקויים: מעל 9 ליקויים</p>";
            }

            if (severeDefects.Count > 0)
            {
                alretHtml += "<p>נמצאו " + severeDefects.Count + " ליקויים חמורים בביקור הנוכחי:</p>";
                for (var d = 0; d < severeDefects.Count; d++)
                {
                    alretHtml += "<p>" + (d+1) +". " + severeDefects[d].Defect_type_name + "- " + severeDefects[d].Defect_name + " </p>";
                }
            }
            else alretHtml += "<p>לא נמצאו ליקויים חמורים</p>";

            if (TenGradeDefects.Count > 0)
            {
                if (TenGradeDefects.Count == severeDefects.Count)
                {
                    alretHtml += "<p>שים לב! כל הליקויים החמורים שנמצאו בציון 10";
                }
                else {
                    alretHtml += "<p>נמצאו " + TenGradeDefects.Count + " ליקויים חמורים ביותר בציון 10: ";
                    for (var d1 = 0; d1 < TenGradeDefects.Count; d1++)
                        alretHtml += "<p>" + (d1 + 1) + ". " + TenGradeDefects[d1].Defect_name + " </p>";
                }
                
            }
            
            alretHtml += "<h3>פירוט ממצאים רמת הבטיחות:</h3>";
            alretHtml += "<p>ממוצע ציוני דוחות הביקור בפרויקט עד כה: " + avg + "</p >";

            if (returnDefects.Count > 0)
            {
                alretHtml += "<p>מספר הליקויים החוזרים מהביקור הקודם: " + returnDefects.Count + "</p>";
                for (var r = 0; r < returnDefects.Count; r++)
                {
                    alretHtml += "<p>" + (r + 1) + ". " + returnDefects[r].Defect_type_name + "- " + returnDefects[r].Defect_name + " </p>";
                }
            }
            else alretHtml += "<p>לא נמצאו ליקויים שחזרו על עצמם מהביקור הקודם</p>";

            if (notFixedDefects.Count > 0)
            {
                alretHtml += "<p>ליקויים מהביקור הקודם שלא תוקנו בזמן: </p>";
                for (var n = 0; n < notFixedDefects.Count; n++)
                {
                    alretHtml += "<p>"+(n+1)+". " + notFixedDefects[n].Defect_name + " </p>";
                }
            }

            if (ProjReportGrades.Length>0)
            {
                if((report_Grade - ProjReportGrades[ProjReportGrades.Length - 1]) >= 10)
                    alretHtml += "<p>ניכר שיפור משמעותי- ציון הדוח הנוכחי גבוה ב-10 נק מציון הדוח הקודם</p>";
                if ((ProjReportGrades[ProjReportGrades.Length - 1] - report_Grade) >= 10)
                    alretHtml += "<p>שים לב! חלה הדרדרות משמעותית מהדוח הקודם שציונו גבוה ב-10 נק מהדוח הנוכחי</p>";
            }

            ReportGrade = report_Grade;
            SafetyLvlGrade = safetyLvL_Grade;
            Alert = alretHtml;
            return this;

        }

        public void UpdateReportGrade(int report_num, double grade)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateReportGrade(report_num, grade);
        }

        public void UpdateProjectSafetyLvl(int project_num, double safety_lvl)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateProjectSafetyLvl(project_num, safety_lvl);
        }
    }

}