USE ProductOrder;
GO;
--Store procedure to  update a Product 
CREATE OR ALTER PROCEDURE UpdateProductUpdPr
    @Id INT,
    @name VARCHAR(50),
    @price DECIMAL(10,2),
    @manufacturer VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
 
    -- Check ID exists in the table OR Not
    IF NOT EXISTS (SELECT 1 FROM Product WHERE ID = @Id)
    BEGIN
        RAISERROR('Product with ID %d not found', 16, 1, @Id);
        RETURN;
    END;
 
    -- Check name is not empty and is unique
    IF @Name = '' OR EXISTS (SELECT 1 FROM Product WHERE Name = @name AND ID != @Id)
    BEGIN
        RAISERROR('Invalid or duplicate product name', 16, 1);
        RETURN;
    END;
 
    -- Check price is not null and greater than 0
    IF @price IS NULL OR @price <= 0
    BEGIN
        RAISERROR('Price Must be greater than 0', 16, 1);
        RETURN;
    END;
 
    -- Check manufacturer is not empty
    IF @manufacturer = ''
    BEGIN
        RAISERROR('Manufacturer cannot be empty', 16, 1);
        RETURN;
    END;
 
    BEGIN TRY
		BEGIN TRANSACTION;
			--Perform update operation
			UPDATE Product
			SET Name = @name,
			    Price = @price,
                Manufacturer = @manufacturer
			WHERE ID = @Id;
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

-- EXEC UpdateProductUpdPr 5,'Mobile',10000,'Samsung'

--SELECT * FROM Product;



