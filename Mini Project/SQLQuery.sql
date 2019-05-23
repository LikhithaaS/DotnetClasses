IF EXISTS (SELECT * FROM SYSOBJECTS WHERE Name='Customers')
DROP TABLE Customers

CREATE SEQUENCE Customer_Seq AS
INT START WITH 1
INCREMENT BY 1 ;

drop Sequence Customer_Seq


CREATE TABLE Customers
(
	CustomerNumber VARCHAR(8) NOT NULL CONSTRAINT PK_CustomerNumber PRIMARY KEY DEFAULT FORMAT((NEXT VALUE FOR Customer_Seq),'CUS0000#'),
	CustomerName VARCHAR(50) NOT NULL,
	Address VARCHAR(250) NOT NULL,
	Telephone VARCHAR(15) CONSTRAINT CHK_Telephone CHECK(Telephone LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') ,
	Gender CHAR CONSTRAINT CHK_Gender CHECK (Gender IN ('M','F','O')),
	Dob DATE CONSTRAINT CHK_Dob CHECK(Dob<=getdate()),
	Smoker VARCHAR(5) CONSTRAINT CHK_Smoker CHECK(Smoker IN ('Y','N')),
	Hobbies VARCHAR(250) NULL,
	CreateID VARCHAR(50) NULL DEFAULT System_User,
	CreateDate DATETIME NULL DEFAULT Getdate(),
	UpdateID VARCHAR(50) NULL DEFAULT System_User ,
	UpdateDate DATETIME NULL DEFAULT Getdate()
)

INSERT INTO Customers(CustomerName,Address,Telephone,Gender,Dob,Smoker)
VALUES
('Likhithaa','Kurnool,AndhraPradesh','8500788030','F','1996/10/27','N')

select * from Customers

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE Name='Products')
DROP TABLE Products

CREATE SEQUENCE Products_Seq AS
INT START WITH 1
INCREMENT BY 1 ;

drop Sequence Products_Seq

CREATE TABLE Products
(
	ProductName VARCHAR(50) NOT NULL,
	ProductID VARCHAR(8) CONSTRAINT PK_ProductID PRIMARY KEY DEFAULT FORMAT((NEXT VALUE FOR Products_Seq),'PRO0000#'),
	ProductType VARCHAR(60) NOT NULL CONSTRAINT CHK_ProductType CHECK(ProductType IN('Life','Non-Life')),
	CustomerNumber VARCHAR(8) CONSTRAINT FK_CustomerNumber FOREIGN KEY (CustomerNumber)
	REFERENCES Customers(CustomerNumber),
	CreateID VARCHAR(50) NULL DEFAULT System_User,
	CreateDate DATETIME NULL DEFAULT Getdate(),
	UpdateID VARCHAR(50) NULL DEFAULT System_User ,
	UpdateDate DATETIME NULL DEFAULT Getdate()
)

INSERT INTO Products(ProductName,ProductType,CustomerNumber)
VALUES
('Life Insurance','Life','CUS00001')

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE Name='Policy')
DROP TABLE Policy

CREATE SEQUENCE Policy_Seq AS
INT START WITH 1
INCREMENT BY 1 ;

CREATE TABLE Policy
(
	PolicyID VARCHAR(8) CONSTRAINT PK_PolicyID PRIMARY KEY DEFAULT FORMAT((NEXT VALUE FOR Policy_Seq),'POL0000#'),
	InsuredName VARCHAR(50) NOT NULL,
	InsuredAge INT NOT NULL,
	Nominee VARCHAR(50),
	Relation VARCHAR(50),
	PremiumFrequency VARCHAR(50) CONSTRAINT CHK_PremiumFrequency CHECK(PremiumFrequency IN ('Monthly','Quaterly','Half Yearly','Annually')),
	CustomerNumber VARCHAR(8) CONSTRAINT FK_CustomerName FOREIGN KEY (CustomerNumber) REFERENCES Customers(CustomerNumber),
	ProductID VARCHAR(8) CONSTRAINT FK_ProductID FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
	CreateID VARCHAR(50) NULL DEFAULT System_User,
	CreateDate DATETIME NULL DEFAULT Getdate(),
	UpdateID VARCHAR(50) NULL DEFAULT System_User ,
	UpdateDate DATETIME NULL DEFAULT Getdate()
)

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE Name='Endorsement')
DROP TABLE Endorsement

CREATE TABLE Endorsement
(
	TransactionID INT IDENTITY(1,1) CONSTRAINT PK_TransactionID PRIMARY KEY,
	PolicyID VARCHAR(8) CONSTRAINT FK_PolicyID FOREIGN KEY (PolicyID) REFERENCES Policy(PolicyID),
	InsuredName VARCHAR(50),
	InsuredAge INT,
	Dob DATE CONSTRAINT CHK_Dob_Endorsement CHECK(Dob<=getdate()),
	Gender CHAR CONSTRAINT CHK_Gender_Endorsement CHECK (Gender IN ('M','F','O')),
	Nominee VARCHAR(50),
	Relation VARCHAR(50),
	Smoker CHAR  CONSTRAINT CHK_Smoker_Endorsement CHECK(Smoker IN ('Y','N')),
	Address VARCHAR(250),
	Telephone VARCHAR(15) CONSTRAINT CHK_Telephone_Endorsement CHECK(Telephone LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	PremiumFrequency VARCHAR(30) CONSTRAINT CHK_PremiumFrequency_Endorsement CHECK(PremiumFrequency IN ('Monthly','Quaterly','Half Yearly','Annually')),
	CreateID VARCHAR(50) NULL DEFAULT System_User,
	CreateDate DATETIME NULL DEFAULT Getdate(),
	UpdateID VARCHAR(50) NULL DEFAULT System_User ,
	UpdateDate DATETIME NULL DEFAULT Getdate()
)

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE Name='Login')
DROP TABLE Login

CREATE TABLE Login
(
	LoginID VARCHAR(30) CONSTRAINT PK_LoginID PRIMARY KEY,
	Password VARCHAR(30) NOT NULL,
	CustomerNumber VARCHAR(8) CONSTRAINT FK_CustomerNumber_Login FOREIGN KEY(CustomerNumber) REFERENCES Customers(CustomerNumber),
	CreateID VARCHAR(50) NULL DEFAULT System_User,
	CreateDate DATETIME NULL DEFAULT Getdate(),
	UpdateID VARCHAR(50) NULL DEFAULT System_User ,
	UpdateDate DATETIME NULL DEFAULT Getdate()
)

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE Name='EndorsementStatus')
DROP TABLE EndorsementStatus

CREATE TABLE EndorsementStatus
(
	TransactionID INT CONSTRAINT FK_TransactionID FOREIGN KEY(TransactionID) REFERENCES Endorsement(TransactionID) ,
	StatusID INT IDENTITY(1,1) CONSTRAINT PK_StatusID PRIMARY KEY,
	CurrentStatus VARCHAR CONSTRAINT CHK_CurrentStatus CHECK(CurrentStatus IN ('Pending','Accepted','Rejected')) 
)

drop proc prcCustomerInsert 

CREATE PROC prcCustomerInsert 					
					@customerName VARCHAR(50),
					@address VARCHAR(250),
					@telephone VARCHAR(15),
					@gender CHAR,
					@dob DATE,
					@smoker CHAR,
					@hobbies VARCHAR(250)										
AS
BEGIN 
INSERT INTO Customers(CustomerName,Address,Telephone,Gender,Dob,Smoker,Hobbies) VALUES(@customerName,@address,@telephone,@gender,@dob,@smoker,@hobbies)
END

CREATE PROC prcCustomerDetails
AS
BEGIN 
SELECT * FROM Customers
END