IF EXISTS (SELECT * FROM SYSOBJECTS WHERE Name='Customers')
DROP TABLE Customers
GO

CREATE SEQUENCE Customer_Seq AS
INT START WITH 1
INCREMENT BY 1 
GO

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

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE Name='Products')
DROP TABLE Products

CREATE SEQUENCE Products_Seq AS
INT START WITH 1
INCREMENT BY 1 ;

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
	PolicyID VARCHAR(8) UNIQUE CONSTRAINT FK_PolicyID FOREIGN KEY (PolicyID) REFERENCES Policy(PolicyID),
	ProductType VARCHAR(60),
	ProductName VARCHAR(50),
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


INSERT INTO  Login(LoginID,Password)
VALUES('Admin','Administrator')

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE Name='EndorsementStatus')
DROP TABLE EndorsementStatus

CREATE TABLE EndorsementStatus
(
	TransactionID INT CONSTRAINT FK_TransactionID FOREIGN KEY(TransactionID) REFERENCES Endorsement(TransactionID) ,
	StatusID INT IDENTITY(1,1) CONSTRAINT PK_StatusID PRIMARY KEY,
	CurrentStatus VARCHAR(30) DEFAULT 'Pending' CONSTRAINT CHK_CurrentStatus CHECK(CurrentStatus IN ('Pending','Accepted','Rejected'))  
)


CREATE TABLE Transactions
(
	TransactionID INT,
	PolicyID VARCHAR(8),
	InsuredName VARCHAR(50),
	InsuredAge INT,
	Dob DATE,
	Gender CHAR,
	Nominee VARCHAR(50),
	Relation VARCHAR(50),
	Smoker VARCHAR(5),
	Address VARCHAR(250),
	Telephone VARCHAR(15),
	PremiumFrequency VARCHAR(20),
	StatusID INT,
	CurrentStatus VARCHAR(50)
)

select * from Login


--PROCEDURES
--***********
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

CREATE PROC prcCustomerID
AS
BEGIN 
SELECT CustomerNumber FROM Customers
END

CREATE PROC prcProductInsert
						@customerNumber VARCHAR(8),
						@productName VARCHAR(50),
						@productType VARCHAR(60)
AS
BEGIN
INSERT INTO Products(CustomerNumber,ProductName,ProductType) VALUES (@customerNumber,@productName,@productType)
END


drop proc prcProductID
CREATE PROC prcProductID
					@customerNumber VARCHAR(10)
AS
BEGIN
SELECT Products.ProductID 
FROM Products
LEFT JOIN Customers
   on Products.CustomerNumber = Customers.CustomerNumber
   where Products.CustomerNumber=@customerNumber
END


CREATE PROC prcPolicyInsert
						@insuredName VARCHAR(50),
						@insuredAge INT,
						@nominee VARCHAR(50),
						@relation VARCHAR(50),
						@premiumFrequency VARCHAR(50),
						@customernumber VARCHAR(8),
						@productID VARCHAR(8)
AS
BEGIN
INSERT INTO Policy(CustomerNumber,ProductID,InsuredName,InsuredAge,Nominee,Relation,PremiumFrequency) VALUES (@customernumber,@productID,@insuredName,@insuredAge,@nominee,@relation,@premiumFrequency)
END

CREATE PROC prcAgeDob
						@customerNumber VARCHAR(8)
AS
BEGIN
SELECT DATEDIFF(hour,Dob,GETDATE())/8766 AS AgeYearsIntTrunc FROM Customers WHERE CustomerNumber=@customerNumber
END


CREATE PROC prcLoginGen
						@customerNumber VARCHAR(8),
						@loginID VARCHAR(30),
						@password VARCHAR(30)
AS
BEGIN
INSERT INTO Login(CustomerNumber,LoginID,Password) VALUES (@customerNumber,@loginID,@password)
END

drop proc prcPolicySearchID

CREATE PROC prcPolicySearchID
							@policyID VARCHAR(8)
AS
BEGIN
		SELECT Policy.PolicyID,Products.ProductID,Products.ProductType,Products.ProductName,Policy.InsuredName,Policy.InsuredAge,Customers.Dob,
		Customers.Gender,Policy.Nominee,Policy.Relation,Customers.Smoker,Customers.Address,Customers.Telephone,Policy.PremiumFrequency
		FROM Customers
		RIGHT JOIN Products ON (Products.CustomerNumber=Customers.CustomerNumber)
		RIGHT JOIN Policy ON (Policy.ProductID=Products.ProductID) WHERE Policy.PolicyID=@policyID ORDER BY Customers.CustomerNumber
END



CREATE PROC prcPolicySearchCust
							@customerNumber VARCHAR(8)
AS
BEGIN
SELECT Policy.PolicyID,Products.ProductID,Products.ProductType,Products.ProductName,Policy.InsuredName,Policy.InsuredAge,Customers.Dob,
			Customers.Gender,Policy.Nominee,Policy.Relation,Customers.Smoker,Customers.Address,Customers.Telephone,Policy.PremiumFrequency
			FROM Customers
			RIGHT JOIN Products ON (Products.CustomerNumber=Customers.CustomerNumber)
			RIGHT JOIN Policy ON (Policy.ProductID=Products.ProductID) WHERE Policy.CustomerNumber=@customerNumber
END

CREATE PROC prcPolicySearchName
							@customerName VARCHAR(50),
							@dob Date
AS
BEGIN
SELECT Policy.PolicyID,Products.ProductID,Products.ProductType,Products.ProductName,Policy.InsuredName,Policy.InsuredAge,Customers.Dob,
			Customers.Gender,Policy.Nominee,Policy.Relation,Customers.Smoker,Customers.Address,Customers.Telephone,Policy.PremiumFrequency
			FROM Customers
			RIGHT JOIN Products ON (Products.CustomerNumber=Customers.CustomerNumber)
			RIGHT JOIN Policy ON (Policy.ProductID=Products.ProductID) WHERE Customers.CustomerName=@customerName AND Customers.Dob=@dob
END


CREATE PROC prcLoginCheck
							@userName VARCHAR(30),@password VARCHAR(30),@RES INT OUTPUT
AS
BEGIN
 IF EXISTS(SELECT LoginID,Password FROM Login WHERE LoginID=@userName AND Password=@password)
BEGIN 
	SET @RES=1	
END
ELSE 
	BEGIN
	SET @RES=0 
	END
END

CREATE PROC prcLoginDetails
				@username VARCHAR(30),@password VARCHAR(30)
AS
BEGIN
	SELECT LoginID,Password,CustomerNumber FROM Login WHERE LoginID=@username AND Password=@password
END




CREATE PROC prcEndorsementInsert
						@policyID VARCHAR(8),
						@productType VARCHAR(60),
						@productName VARCHAR(50),
						@insuredName VARCHAR(50),
						@insuredAge INT,
						@dob DATE,
						@gender CHAR,
						@nominee VARCHAR(50),
						@relation VARCHAR(50),
						@smoker CHAR,
						@address VARCHAR(250),
						@telephone VARCHAR(15),
						@premiumFrequency VARCHAR(50)					
						
AS
BEGIN
INSERT INTO Endorsement(PolicyID,ProductType,ProductName,InsuredName,InsuredAge,Dob,Gender,Nominee,Relation,Smoker,Address,Telephone,PremiumFrequency) VALUES (@policyID,@productType,@productName,@insuredName,@insuredAge,@dob,@gender,@nominee,@relation,@smoker,@address,@telephone,@premiumFrequency)
END

CREATE PROC prctransactionID
						@policyID VARCHAR(8)
AS
BEGIN 
SELECT TransactionID FROM Endorsement WHERE PolicyID=@policyID
END

CREATE PROC prcStatusupdate
						@transactionID INT
AS
BEGIN
INSERT INTO EndorsementStatus(TransactionID) VALUES(@transactionID)
END


CREATE PROC prcEndorsementDetailsCust
							@customernumber VARCHAR(8)							
AS
BEGIN
		SELECT Endorsement.TransactionID,Endorsement.PolicyID,Endorsement.InsuredName,Endorsement.InsuredAge,Endorsement.Dob,Endorsement.Gender,Endorsement.Nominee,Endorsement.Relation,
		Endorsement.Smoker,Endorsement.Address,Endorsement.Telephone,Endorsement.PremiumFrequency,EndorsementStatus.StatusID,EndorsementStatus.CurrentStatus
		FROM Endorsement
		RIGHT JOIN EndorsementStatus ON (Endorsement.TransactionID=EndorsementStatus.TransactionID)
		LEFT JOIN Policy ON (Endorsement.PolicyID=Policy.PolicyID) WHERE CustomerNumber=@customernumber
END

CREATE PROC prcEndorsementDetails						
AS
BEGIN
		SELECT Endorsement.TransactionID,Endorsement.PolicyID,Endorsement.InsuredName,Endorsement.InsuredAge,Endorsement.Dob,Endorsement.Gender,Endorsement.Nominee,Endorsement.Relation,
		Endorsement.Smoker,Endorsement.Address,Endorsement.Telephone,Endorsement.PremiumFrequency,EndorsementStatus.StatusID,EndorsementStatus.CurrentStatus
		FROM Endorsement
		RIGHT JOIN EndorsementStatus ON (Endorsement.TransactionID=EndorsementStatus.TransactionID)
END

CREATE PROC prcEndorsementPolicyIDDetails
									@policyID VARCHAR(8)						
AS
BEGIN
		SELECT Endorsement.TransactionID,Endorsement.PolicyID,Endorsement.InsuredName,Endorsement.InsuredAge,Endorsement.Dob,Endorsement.Gender,Endorsement.Nominee,Endorsement.Relation,
		Endorsement.Smoker,Endorsement.Address,Endorsement.Telephone,Endorsement.PremiumFrequency,EndorsementStatus.StatusID,EndorsementStatus.CurrentStatus
		FROM Endorsement
		RIGHT JOIN EndorsementStatus ON (Endorsement.TransactionID=EndorsementStatus.TransactionID) WHERE Endorsement.PolicyID=@policyID
END

SELECT * FROM Endorsement
SELECT * FROM Policy



CREATE PROC prcEndorsementUpdateDetails
						@policyID VARCHAR(8),						
						@insuredName VARCHAR(50),
						@insuredAge INT,
						@dob DATE,
						@gender CHAR,
						@nominee VARCHAR(50),
						@relation VARCHAR(50),
						@smoker CHAR,
						@address VARCHAR(250),
						@telephone VARCHAR(15),
						@premiumFrequency VARCHAR(50)

AS
BEGIN TRY
    BEGIN TRANSACTION

        UPDATE Endorsement SET InsuredName=@insuredName,InsuredAge=@insuredAge,Dob=@dob,Gender=@gender,Nominee=@nominee,Relation=@relation,
		Smoker=@smoker,Address=@address,Telephone=@telephone,PremiumFrequency=@premiumFrequency
		WHERE PolicyID=@policyID
    COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN --RollBack in case of Error
END CATCH



CREATE PROC prcStatusUpdateddd
                          @transactionID INT,
						  @currentstatus VARCHAR(30)
AS
BEGIN
 UPDATE EndorsementStatus SET CurrentStatus=@currentstatus WHERE TransactionID=@transactionID
END

CREATE PROC prcPolicyUpdate
					  @policyID VARCHAR(8),
				      @insuredName VARCHAR(50),
					  @insuredAge INT,					  
					  @nominee VARCHAR(50),
					  @relation VARCHAR(50),					 
					  @premiumFrequency VARCHAR(50)
AS
BEGIN
	UPDATE Policy SET InsuredName=@insuredName,InsuredAge=@insuredAge,Nominee=@nominee,Relation=@relation,PremiumFrequency=@premiumFrequency WHERE PolicyID=@policyID
END

CREATE PROC prcCustomerUpdate
							@customernumber VARCHAR(8),
							@dob DATE ,
							@gender CHAR,
							 @smoker VARCHAR(5),
							 @address VARCHAR(250),
							@telephone VARCHAR(15)
AS
BEGIN
    UPDATE Customers SET Dob=@dob,Gender=@gender,Smoker=@smoker,Address=@address,Telephone=@telephone WHERE CustomerNumber=@customernumber
END


CREATE PROC prccustIDGen
						@policyID VARCHAR(8)
AS
BEGIN
	SELECT CustomerNumber FROM Policy WHERE PolicyID=@policyID
END

drop  table Transactions



CREATE PROC prcTransactionDetails
								@transactionID INT,
								@policyID VARCHAR(8),
								@insuredName VARCHAR(50),
								@insuredAge INT,
								@dob DATE,
								@gender CHAR,
								@nominee VARCHAR(50),
								@relation VARCHAR(50),
								@smoker VARCHAR(5),
								@address VARCHAR(250),
								@telephone VARCHAR(15),
								@premiumFrequency VARCHAR(20),
								@statusID INT,
								@currentStatus VARCHAR(50)
AS
BEGIN TRANSACTION
	INSERT INTO Transactions VALUES(@transactionID,@policyID,@insuredName,@insuredAge,@dob,@gender,@nominee,@relation,@smoker,@address,@telephone,@premiumFrequency,@statusID,@currentStatus);
	DELETE FROM EndorsementStatus WHERE TransactionID=@transactionID;
	DELETE FROM Endorsement WHERE PolicyID=@policyID;
COMMIT;



CREATE PROC prcTransDetails
AS
BEGIN
	SELECT * FROM Transactions
END


CREATE PROC prcTransactionDetailsID
					@customerNumber VARCHAR(10)
AS
BEGIN
SELECT Transactions.*
FROM Transactions
LEFT JOIN Policy on Transactions.PolicyID=Policy.PolicyID
LEFT JOIN Endorsement
   on Endorsement.TransactionID=Transactions.TransactionID
   where Policy.CustomerNumber=@customerNumber
END






