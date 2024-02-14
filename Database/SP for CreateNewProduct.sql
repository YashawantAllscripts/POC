USE ProductOrder;
GO;
--Store procedure to create a new Product record
CREATE OR ALTER PROCEDURE CreateNewProductInsPr
			--@ID INT,
			@name VARCHAR(50),
			@price DECIMAL(10,2),
			@manufacturer VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	--Check Input parameter values is Null Or Empty
	IF(LEN(@name)=0 OR @price IS NULL OR LEN(@manufacturer)=0)
	BEGIN
		RAISERROR('Invalid Parameters', 16, 1);
		RETURN;
	END;
	BEGIN TRY
		BEGIN TRANSACTION;
				--Insert records
				DECLARE @ID INT;
				SET @ID=(SELECT MAX(ID)+1 FROM Product); -- Increment Id value by 1 
				INSERT INTO Product(ID,Name,Price,Manufacturer)
						VALUES (@ID,@name,@price,@manufacturer)
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
				--Check Trasaction failure
				IF @@TRANCOUNT>0
				ROLLBACK TRANSACTION;
					PRINT('Exception Is:' +ERROR_MESSAGE());
	END CATCH
END

-- EXEC CreateNewProductInsPr 'Charger',2000.23,'Nokia'

--SELECT * FROM Product;

--DELETE FROM Product WHERE ID in(6,7)


			