USE ProductOrder;
GO;
--Store procedure to Delete a Order
CREATE OR ALTER PROCEDURE DeleteOrderDelPr
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
	 -- Check ID exists in the table OR Not
    IF @id<=0 OR NOT EXISTS (SELECT 1 FROM Orders WHERE ID = @Id)
    BEGIN
        RAISERROR('Order with ID %d not found', 16, 1, @Id);
        RETURN;
    END;
	BEGIN TRY
			BEGIN TRANSACTION;
				DELETE FROM Orders
					WHERE ID=@Id;
			COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		--Rollback if errror occurs
		IF @@TRANCOUNT > 0
			BEGIN
            ROLLBACK TRANSACTION;
			END;
        PRINT('Exception occurred: ' + ERROR_MESSAGE());
	END CATCH
END;

--EXEC DeleteOrderDelPr 4
--Delete from Orders where Id=4
--SELECT * FROM Orders;