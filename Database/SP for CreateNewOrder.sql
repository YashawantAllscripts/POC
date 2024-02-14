USE ProductOrder;
GO;
--Store procedure to create a new Order record
CREATE OR ALTER PROCEDURE CreateNewOrderInsPr
			@productID INT,
			@date DATE,
			@quantity INT
AS
BEGIN
	SET NOCOUNT ON;
	--Check Input parameter values is Null Or Empty
	IF(@productID IS NULL OR @date IS NULL OR @quantity IS NULL)
	BEGIN
		RAISERROR('Invalid Parameters', 16, 1);
	END;
	-- Convert the date to a valid date format 'YYYY-MM-DD'

	SET @date=TRY_CONVERT(DATE,@date,23);---- Style 23: 'YYYY-MM-DD'

	BEGIN TRY
		BEGIN TRANSACTION;
				--Insert records
				DECLARE @ID INT;
				SET @ID=(SELECT MAX(ID)+1 FROM Orders);print @id-- Increment Id value by 1 
				INSERT INTO Orders(ID,ProductID,Date,Quantity)
						VALUES (@ID,@productID,@date,@quantity)
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
				--Check Trasaction failure
				IF @@TRANCOUNT>0
				ROLLBACK TRANSACTION;
				PRINT('Exception Is:' +ERROR_MESSAGE());
	END CATCH
END

--EXEC CreateNewOrderInsPr 4,'07-02-2024',20

--SELECT * FROM Orders;

--DELETE FROM Orders where id=5;


