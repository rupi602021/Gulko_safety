create table [user](
[email] nvarchar (150) NOT NULL,
[name] nvarchar (50) NOT NULL,
[phone] nvarchar (50),
[password] nvarchar (20) NOT NULL,
[user_type_num] smallint NOT NULL,
constraint fk_user foreign key(user_type_num) references user_type(user_type_num),
primary key(email)
)

drop table defect

create table [user_type](
[user_type_num] smallint IDENTITY (1,1) NOT NULL,
[type_name] nvarchar (50) NOT NULL,
primary key(user_type_num)
)

create table [contact](
[id] nvarchar (50) NOT NULL,
[full_name] nvarchar (50) NOT NULL,
[phone] nvarchar (50),
[mail] nvarchar (150),
primary key(id)
)


create table [order](
[order_num] smallint IDENTITY (1,1) NOT NULL,
[bill_num] nvarchar (50) NOT NULL,
[date] date,
[price] float NOT NULL,
[contact_id] nvarchar (50) NOT NULL,
constraint fk_order foreign key(contact_id) references contact(id),
primary key(order_num)
)

create table [report](
[report_num] smallint IDENTITY (1,1) NOT NULL,
[date] date NOT NULL,
[time] time NOT NULL,
[grade] float,
[comment] nvarchar (300),
[project_num] smallint NOT NULL,
constraint fk_report foreign key(project_num) references project(project_num),
primary key(report_num)
)

create table[defect_in_report](
[report_num] smallint NOT NULL,
[defect_num] smallint NOT NULL,
[fix_date] date NOT NULL,
[fix_time] time NOT NULL,
[picture_link] nvarchar (250),
[fix_status] int,
[description] nvarchar (300),
primary key(report_num,defect_num),
constraint fk_report1 foreign key(report_num) references report(report_num),
constraint fk_defect1 foreign key(defect_num) references defect(defect_num)
 )

INSERT INTO contact (id,full_name,phone,mail)
VALUES ('845249009',   'מני בר לב','0547782834','meni@gmail.com')
VALUES ('458166632',  'איבראים שראק','0521112731','ibrahim@gmail.com')
VALUES ('243568479',  'נאביל נששבי', '0529375962','nabil@gmail.com')
VALUES ('172967185',  'פארח מוכברי', '0548792491','farah@gmail.com')
VALUES ('438992004',  'פראנק בן שושן','0508693712','frank@gmail.com')
VALUES ('145668193', 'צחי כוכבי', '0502123481', 'tzahi@gmail.com')
VALUES ('438710936', 'אסף אזולאי', '0523749602','assaf@gmail.com')
VALUES ('116063919', 'יזאן אבעדי' , '0543928374', 'abadi@gmail.com')
VALUES ('477210406', 'מוחמד ספדי', '0523434612', 'mohamad@gmail.com')
VALUES ('357189733',  'פאדי מגדלה', '0542847582','fadi@gmail.com')
VALUES ('399173392', 'תום שושני', '0506954521', 'tom@gmail.com')
VALUES ('134130044', 'גיין יאנג','0505738163','jain@gmail.com')
VALUES ('429551586','חמודי גברי' ,'0523758391', 'jabri@gmail.com')
VALUES ('228864100', 'איציק כהן','0528491834','itzik@gmail.com')
VALUES ('102744393', 'שלומי בן עזרא','0528496831','shlomi@gmail.com')
VALUES ('369432862','מוטי אופיר', '0547827482','motti@gmail.com')
VALUES ('337882923','עידן אורלב','0524895693','idan@gmail.com')
VALUES ('326491862', 'גלעד אביב','0508294869','gilad@gmail.com')
VALUES ('113732012','אבנר רובי','0502375835','avner@gmail.com')
VALUES ('266673209','משה כהן', '0523849681','moshe@gmail.com')
VALUES ('232372725','ראובן אבי', '0529865374','avi@gmail.com')
VALUES ('481238923','מוחמד אלי','0548678322','ali@gmail.com')
VALUES ('389842100',' גיל אלון', '0546954542', 'gil@gmail.com')

INSERT INTO [order](bill_num,date,price,contact_id)
VALUES ('234567','2020-05-05',9781,'116063919')
VALUES ('226658','2020-04-01',5858,'845249009')
VALUES ('246890','2020-03-30',770,'438992004')
VALUES ('245797','2020-03-11',7657,'337882923')
VALUES ('263533','2020-01-30',2380,'134130044')
VALUES ('285677','2020-01-29',2950,'481238923')

INSERT item (name,price)
VALUES ('ערכת עזרה ראשונה גדולה',359)

INSERT INTO items_in_order(order_num,item_num,quantity)
VALUES (7,14,19)
VALUES (7,13,16)
VALUES (6,12,130)
VALUES (6,11,12)
VALUES (4,10,13)
VALUES (5,9,10)
VALUES (5,8,12)
VALUES (4,7,7)
VALUES (4,6,10)
VALUES (4,5,13)
VALUES (3,3,8)
VALUES (2,2,10)
VALUES (2,1,5)

INSERT INTO [defect](grade,defect_type_num,name)
VALUES  (6,8,'הצבת חפצים להגבהה על רצפת הפיגום ועבודה מהם')

INSERT INTO items_in_order(order_num,item_num,quantity)
VALUES  (5,10,13)

select * from [user]

SELECT * FROM instruction


select o.*,io.order_num,i.*
from [order] o inner join items_in_order io on o.order_num=io.order_num inner join item i on io.item_num=i.item_num
where o.order_num=(select order_num, sum(quantity)
from items_in_order
)
select i.*, io.order_num, io.quantity
from item i inner join items_in_order io on i.item_num=io.item_num  where io.order_num=6

select * from contact_in_instruction

ALTER TABLE project
ADD delete_status int;

select * from contact

select u.*, ut.type_name from [user] u inner join [user_type] ut on u.user_type_num = ut.user_type_num

select distinct o.order_num, o.bill_num,o.contact_id,o.total_price,oi.quantity
from [order] o left join items_in_order oi on o.order_num=oi.order_num 
group by o.order_num, o.bill_num,o.contact_id,o.total_price ,oi.item_num,oi.quantity

SELECT i.*, it.type_name,it.expiration
FROM instruction i left join instruction_type it on i.instruction_type_num = it.instruction_type_num 

select c.*, ci.*
from contact_in_instruction ci inner join contact c on ci.contact_id= c.id
where ci.instruction_num= 2

ALTER TABLE instruction
ALTER COLUMN [time] datetime;
sp_rename 'order.bill_num', 'invoice_num', 'COLUMN';

ALTER TABLE contact 
ALTER COLUMN phome strin;