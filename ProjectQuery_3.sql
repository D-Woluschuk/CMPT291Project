USE [Project]

DROP TABLE [booking];
DROP TABLE [cus_phone];
DROP TABLE [emp_phone];
DROP TABLE [customer];
DROP TABLE [employee];
DROP TABLE [car];
DROP TABLE [branch];
DROP TABLE [type];




CREATE TABLE [customer](
	[cust_ID] [varchar](25) NOT NULL,
	[FName] [varchar](25) NULL,
	[LName] [varchar](25) NULL,
	[Street] [varchar](25) NULL,
	[City] [varchar](25) NULL,
	[Province] [varchar](25) NULL,
	[Gold_status] [BIT] NULL,
	PRIMARY KEY (cust_ID),
	)

CREATE TABLE branch(
	[branch_id] [varchar](25) NOT NULL,
	[city] [varchar](25) NOT NULL,
	[street] [varchar](25) NULL,
	[province] [varchar](25) NULL,
	[phone] [varchar](25) NULL,
	PRIMARY KEY (branch_id)
	)

CREATE TABLE [type](
	[type_id] [varchar](25) NOT NULL,
	[daily] [float] NOT NULL,
	[weekly] [float] NOT NULL,
	[monthly] [float] NOT NULL,
	[description] [nchar](25) NULL,
	[change] [float] NOT NULL,
	[late_fee] [float] NOT NULL,
	PRIMARY KEY ([type_id]) 
	)

CREATE TABLE [car](
	[car_id] [varchar](25) NOT NULL,
	[car_type] [varchar](25) NOT NULL,
	[car_branch] [varchar](25) NULL,
	[model] [varchar](25) NULL,
	[year] [varchar](25) NULL,
	[plate_num] [varchar](25) NULL,
	PRIMARY KEY (car_id),
	FOREIGN KEY (car_type) REFERENCES [type]([type_id]),
	FOREIGN KEY (car_branch) REFERENCES [branch]([branch_id])

	)

CREATE TABLE [booking](
	[booking_id] [varchar](25) NOT NULL,
	[cust_id] [varchar](25) NOT NULL,
	[car_id] [varchar](25) NOT NULL,
	[date_from] [date] NOT NULL,
	[date_to] [date] NOT NULL,
	[returned] [date] NULL,
	[price] [int] NULL,
	[type_requested] [varchar](25) NOT NULL,
	[branchFrom] [varchar](25) NOT NULL,
	[branchTo] [varchar](25) NULL,
	PRIMARY KEY (booking_id),
	FOREIGN KEY (cust_id) REFERENCES [customer](cust_id),
	FOREIGN KEY (car_id) REFERENCES [car]([car_id]),
	FOREIGN KEY (branchFrom) REFERENCES [branch]([branch_id]),
	FOREIGN KEY (branchTo) REFERENCES [branch]([branch_id]),
	FOREIGN KEY ([type_requested]) REFERENCES [type]([type_id])
	) 

CREATE TABLE [employee](
	[emp_id] [varchar](25) NOT NULL,
	[emp_branch] [varchar](25) NOT NULL,
	[Fname] [varchar](25) NULL,
	[LName] [varchar](25) NULL,
	[salary] [varchar](25) NULL,
	PRIMARY KEY (emp_id),
	FOREIGN KEY (emp_branch) REFERENCES [branch](branch_id)
	)

CREATE TABLE [cus_phone](
	[id] [varchar](25) NOT NULL,
	[phone] [varchar](25) NOT NULL,
	PRIMARY KEY (id, phone),
	FOREIGN KEY (id) REFERENCES [customer]([cust_id])
	)

CREATE TABLE [emp_phone](
	[id] [varchar](25) NOT NULL,
	[phone] [varchar](25) NOT NULL,
	PRIMARY KEY (id, phone),
	FOREIGN KEY (id) REFERENCES [employee]([emp_id])
)



USE [Project]

DELETE FROM booking;
DELETE FROM car;
DELETE FROM [type];
DELETE FROM customer;
DELETE FROM employee;
DELETE FROM branch;
DELETE FROM cus_phone;
DELETE FROM emp_phone;


/****  Branch ****/
Insert into branch values ('001','Edmonton', 'Some', 'AB', '780-123-6543');
Insert into branch values ('002','Calgary', 'Jake', 'AB', '780-113-6443');
Insert into branch values ('003','Red Deer', 'Connect', 'AB', '780-765-2563');
Insert into branch values ('004', 'Banff', 'Red', 'AB', '780-291-3791');
Insert into branch values ('005', 'Saskatoon', 'Wild', 'SA', '780-926-2755');
Insert into branch values ('006', 'Victoria', 'Sea', 'BC', '780-275-1747');

/****  Customer ****/
Insert into customer values ('001','Mark','DCruz', 'some', 'Edmonton', 'AB', 0);
Insert into customer values ('002','John','Doe', 'street', 'Calgary', 'AB', 0);
Insert into customer values ('003','Beth','Lro', 'name', 'Red Deer', 'AB', 0);
Insert into customer values ('004','Lee','Bets', 'random', 'Calgary', 'AB', 0);
Insert into customer values ('005','Kurt','Genova', 'exist', 'Edmonton', 'AB', 0);
Insert into customer values ('006','Jean','Sky', 'here', 'Edmonton', 'AB', 0);
Insert into customer values ('007','Lisa','Light', 'Burn', 'Banff', 'AB', 0);
Insert into customer values ('008','Eric','Grass', 'Fall', 'Victoria', 'BC', 0);
Insert into customer values ('009','Dylan','Tango', 'Alpha', 'Saskatoon', 'SA', 0);
Insert into customer values ('010','Eula','Frost', 'Sword', 'Victoria', 'BC', 0);
Insert into customer values ('011','Sara','Bird', 'Light', 'Red Deer', 'AB', 0);


/****  Type ****/
Insert into [type] values ('001',9.99, 59.99, 99.99, 'Economy', 4.99, 9.99);
Insert into [type] values ('002',19.99, 69.99, 129.99, 'Convertable', 9.99, 19.99);
Insert into [type] values ('003',29.99, 79.99, 149.99, 'Compact', 9.99, 19.99); 
Insert into [type] values ('004',39.99, 89.99, 169.99, 'SUV', 19.99, 19.99);
Insert into [type] values ('005',49.99, 99.99, 189.99,'Luxury', 29.99, 39.99); 

 
/****  Car ****/
Insert into car values ('001','005','001', 'Toyota', '2015', '208IJN');
Insert into car values ('002','005','002', 'Ford', '2015', 'KMW923');
Insert into car values ('003','005','003', 'Volkswagen', '2015', 'J28SDF');
Insert into car values ('004','003','001', 'Toyota', '2017', 'SK8JS2');
Insert into car values ('005','003','002', 'Kia', '2180', '89WL23');
Insert into car values ('006','004','001', 'Honda', '1999', 'K87MGS');
Insert into car values ('007','004','002', 'Ford', '2019', 'KW7NS1');
Insert into car values ('008','004','003', 'Volksvagen', '2016', 'JSM987');
Insert into car values ('012','004','006', 'Volksvagen', '2017', 'JMM987');
Insert into car values ('009','003','004', 'Super', '2019', 'AK8787');
Insert into car values ('010','001','005', 'Honda', '2020', 'QK91KQ');
Insert into car values ('013','001','006', 'Honda', '2021', 'QKA9KQ');
Insert into car values ('011','002','001', 'Ford', '2018', 'JA71JJ');


/**** Employee ****/
Insert into employee values ('001', '001', 'Stuart', 'Ball', '50,000');
Insert into employee values ('002', '001', 'Lea', 'Sin', '45,000');
Insert into employee values ('003', '002', 'John', 'Rock', '55,000');
Insert into employee values ('004', '003', 'Jake', 'Ting', '52,000');
Insert into employee values ('005', '004', 'Emily', 'Bell', '51,000');
Insert into employee values ('006', '005', 'Gronk', 'Rong', '76,000');
Insert into employee values ('007', '006', 'Cthun', 'Lun', '20,000');

/**** Customer Phone****/
Insert into cus_phone values ('001', '780-182-3939');
Insert into cus_phone values ('002', '780-121-6539');
Insert into cus_phone values ('003', '780-765-6219');
Insert into cus_phone values ('004', '780-104-9876');
Insert into cus_phone values ('005', '780-502-1784');
Insert into cus_phone values ('006', '780-572-1864');
Insert into cus_phone values ('007', '780-687-6925');
Insert into cus_phone values ('008', '780-602-2057');
Insert into cus_phone values ('009', '780-719-6723');
Insert into cus_phone values ('010', '780-687-5028');
Insert into cus_phone values ('011', '780-599-1947');

/**** Employee Phone ****/
Insert into emp_phone values ('001', '780-295-1947');
Insert into emp_phone values ('002', '780-487-4010');
Insert into emp_phone values ('003', '780-588-2100');
Insert into emp_phone values ('004', '780-300-1984');
Insert into emp_phone values ('005', '780-102-7452');
Insert into emp_phone values ('006', '780-682-6629');
Insert into emp_phone values ('007', '780-492-2091');

/*******/

	 
/****  Booking ****/
Insert into booking values ('001','001','001','2009-11-12', '2009-12-12', NULL, 499.99, '001', '001', '002');
Insert into booking values ('002','002','001','2009-9-1', '2009-9-5', '2009-9-5',199.96, '002', '001', '001');
Insert into booking values ('003','003','004','2009-1-2', '2009-2-12', '2009-2-12',679.94, '003', '002', '002');
Insert into booking values ('004','004','005','2009-7-24', '2009-8-1', '2009-8-3',309.94, '002', '003', '001');
Insert into booking values ('005','005','006','2009-2-21', '2009-2-24', '2009-2-25', 99.95, '004', '003', '001');
Insert into booking values ('006','006','007','2009-6-14', '2009-6-20', NULL,119.94, '001', '004', '003');
Insert into booking values ('007','007','008','2009-7-24', '2009-8-21', '2009-8-22',459.89, '004', '001', '003');
Insert into booking values ('008','001','009','2009-8-6', '2009-8-21', '2009-9-1',459.96, '001', '004', '004');
Insert into booking values ('009','003','010','2009-9-20', '2009-9-28', '2009-9-28',89.98, '004', '003', '002');
Insert into booking values ('010','005','011','2009-10-11', '2009-11-20', '2009-11-20',274.06, '001', '002', '002');




select * from employee;
select * from car;
select * from booking;
select * from [type];
select * from branch;
select * from customer;


SELECT branchFrom, sum(price) as [sum]
FROM booking 
WHERE branchFrom = '001' and type_requested = '001' and date_from >= '2000-02-01' and date_to <= '2022-06-07'
GROUP BY branchFrom;

/*
select branchFrom, count(*)
from booking
where [status] = 0
group by branchFrom;
*/


select car.car_id, maximum
from car, booking, (
select max(num1) as maximum
from (
select car.car_id, count(*) as num1
from car, booking
where car.car_id = booking.car_id
group by car.car_id) as temp) as tem
where car.car_id = booking.car_id
group by car.car_id, maximum
having count(*) >= maximum

select date_to
from booking

select date_from
from booking

select *
from booking, [type]
where type_requested = [type_id]
