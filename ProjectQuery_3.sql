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
	[description] [varchar](25) NULL,
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

/****  Customer ****/
Insert into customer values ('001','Mark','DCruz', 'some', 'Edmonton', 'AB', 1);
Insert into customer values ('002','John','Doe', 'street', 'Calgary', 'AB', 1);
Insert into customer values ('003','Beth','Lro', 'name', 'Red Deer', 'AB', 1);
Insert into customer values ('004','Lee','Bets', 'random', 'Calgary', 'AB', 0);
Insert into customer values ('005','Kurt','Genova', 'exist', 'Edmonton', 'AB', 0);
Insert into customer values ('006','Jean','Sky', 'here', 'Edmonton', 'AB', 1);

/****  Branch ****/


Insert into branch values ('001','Edmonton', 'some', 'AB', '780-123-6543');
Insert into branch values ('002','Calgary', 'jake', 'BC', '780-113-6443');
Insert into branch values ('003','Red Deer', 'connect', 'SA', '780-765-2563');


/**** Employee ****/
Insert into employee values ('001', '001', 'Stuart', 'Ball', '50,000');
Insert into employee values ('002', '001', 'Lea', 'Sin', '45,000');
Insert into employee values ('003', '002', 'John', 'Rock', '55,000');
Insert into employee values ('004', '003', 'Gab', 'Ting', '52,000');


/****  Type ****/
Insert into [type] values ('001',49.99, 199.99, 2999.99,'Luxury', 29.99, 99.99); 
Insert into [type] values ('002',29.99, 149.99, 1999.99, 'Compact', 19.99, 49.99); 
Insert into [type] values ('003',19.99, 99.99, 499.99, 'SUV', 9.99, 29.99);
 
/****  Car ***			car_id, car_type, car_branch, model, year, plate_num */
Insert into car values ('001','001','001', 'Toyota', '2009', '208IJN');
Insert into car values ('002','002','002', 'Ford', '2010', 'KMW923');
Insert into car values ('003','003','003', 'Volkswagen', '2022', 'J28SDF');
Insert into car values ('004','002','002', 'Toyota', '2020', 'SK8JS2');
Insert into car values ('005','001','001', 'Kia', '2022', 'XK3AG4');
Insert into car values ('006','002','002', 'Honda', '1984', 'K87MGS');
Insert into car values ('007','003','003', 'Ford', '2020', 'KW7NS1');
Insert into car values ('008','002','002', 'Volksvagen', '2016', 'JSM987');

/****  Booking ***/
Insert into booking values ('001','001','001','2009-11-12', '2009-12-12', NULL, 50, '003', '001', NULL);
Insert into booking values ('002','002','001','2009-9-1', '2009-9-5', '2009-9-5',50, '002', '002', '002');
Insert into booking values ('003','003','004','2009-1-2', '2009-2-12', '2009-2-12',30, '001', '001', '003');
Insert into booking values ('004','004','005','2009-7-24', '2009-8-1', NULL,30, '001', '002', '002');
Insert into booking values ('005','005','006','2009-2-21', '2009-2-24', NULL, 20, '002', '001', '001');
Insert into booking values ('006','006','007','2009-6-14', '2009-6-20', NULL,20, '001', '003', '002');


	 



select * from employee;
select * from car;
select * from booking;
select * from [type];
select * from branch;
select * from customer;


select branchFrom, sum(price) 
from booking 
where branchFrom = 'Red Deer'
group by branchFrom;

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

