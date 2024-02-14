USE ProductOrder;
GO;
--Store procedure to  update a Order 
CREATE OR ALTER PROCEDURE UpdateOrderUpdPr
	@Id INT,
    @productID INT,
    @date DATE,
    @quantity INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Check Order ID exists in the table or Not
    IF NOT EXISTS (SELECT 1 FROM Orders WHERE ID = @Id)
    BEGIN
        RAISERROR('Order with ID %d not found', 16, 1, @Id);
        RETURN;
    END;
 
    -- Check Product ID exists in the Product table or Not
    IF NOT EXISTS (SELECT 1 FROM Product WHERE ID = @productID)
    BEGIN
        RAISERROR('Product with ID %d not found in Product Table', 16, 1, @productID);
        RETURN;
    END;
 
    -- Check if the provided Date is not null
    IF @date IS NULL
    BEGIN
        RAISERROR('Date cannot be null', 16, 1);
        RETURN;
    END;

	-- Convert the date to a valid date format 'YYYY-MM-DD'

	SET @date=TRY_CONVERT(DATE,@date,23);---- Style 23: 'YYYY-MM-DD'
 
    -- Check Quantity is not null and greater than 0
    IF @quantity IS NULL OR @quantity <= 0
    BEGIN
        RAISERROR('Quantity Must be greater than zero', 16, 1);
        RETURN;
    END;
 --Updating the order
    BEGIN TRY
        BEGIN TRANSACTION;
        -- Update the order
			UPDATE Orders
				SET ProductID = @productID,
					Date = @date,
					Quantity = @quantity
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
    END CATCH;
END;

--EXEC UpdateOrderUpdpr 4,4,'2023-12-05',05

--SELECT * FROM Orders