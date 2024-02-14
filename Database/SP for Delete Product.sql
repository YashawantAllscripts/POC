USE ProductOrder;
GO;
--Store procedure to Delete a Product 
CREATE OR ALTER PROCEDURE DeleteProductDelPr
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
 
    -- Check ID exists in the table OR Not
    IF @id<=0 OR NOT EXISTS (SELECT 1 FROM Product WHERE ID = @Id)
    BEGIN
        RAISERROR('Product with ID %d not found', 16, 1, @Id);
        RETURN;
    END;
	BEGIN TRY
			BEGIN TRANSACTION;
				-- Update orders to remove association with the product    
			    --UPDATE Orders SET ProductID=NULL WHERE ID=@Id;

				--Delete orders associated with the product
				DELETE FROM Orders 
				WHERE ProductID = @Id;

				--Delete Product
				DELETE FROM Product
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

--EXEC DeleteProductDelPr 3

-- SELECT * FROM Product;