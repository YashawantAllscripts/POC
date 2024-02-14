USE ProductOrder;
GO
--Store Procedure to Get the Orders by ProductId

CREATE OR ALTER PROCEDURE GetOrdersByProductIDSelPr ( 
						@productID int)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT o.ID,o.ProductID,o.Date,o.Quantity 
	FROM Orders o 
	WITH (NOLOCK)
	JOIN Product p 
	ON p.ID=o.ProductID 
	WHERE o.ProductID=@productID; 
END;


--EXEC GetOrdersByProductIDSelPr 3;
--SELECT * FROM ORDERS;
--DELETE FROM ORDERS WHERE ID=55;

