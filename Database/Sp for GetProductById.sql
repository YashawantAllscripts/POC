USE ProductOrder;
GO
--Create a Store Procedure to Get Products By Id

CREATE OR ALTER PROCEDURE GetProductsByIdSelPr (
		@ID INT )
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ID,Name,Price,Manufacturer 
	FROM Product 
	WITH (NOLOCK)--Dirty Read
	WHERE ID=@ID;
END;

--Executing GetProductsByIdSelPr By passing Id value

--exec GetProductsByIdSelPr 1;