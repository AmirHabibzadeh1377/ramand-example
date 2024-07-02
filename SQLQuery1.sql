Go
	IF Not EXISTS(SELECT * FROM master.sys.databases WHERE name='Ramand_Db')
		BEGIN
			  Create DATABASE  Ramand_Db
		END
	else
		Begin
			Use Ramand_Db
		End
Go
	IF NOt EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='User') 
		Begin
			CREATE TABLE [User]
			(
				Id  int primary key identity(1,1) not null,
				FirstName nvarchar(100),
				LastName nvarchar(150),
				Username nvarchar(200),
				Password nvarchar(200)
			)
		End
Go
	CREATE PROCEDURE GetUserByUsername
      @Username NVARCHAR(100)
	  AS
	  BEGIN
		 SET NOCOUNT ON;
		 
		 SELECT *
		 FROM [User]
		 WHERE Username = @Username;
	  END
Go
		CREATE PROCEDURE GetAllUser
		AS
		BEGIN
		    SET NOCOUNT ON;
		
		    SELECT *
		    FROM [User]
		END
Go

insert into [User](FirstName,LastName,Username,Password)values('amir','habibzadeh','amirhabibzadeh','123456789')
Go
