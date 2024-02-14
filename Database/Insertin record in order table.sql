USE ProductOrder;
GO;
--Insert records into Order Table

INSERT INTO Orders(ID,ProductId,Date,Quantity) 
		VALUES
		(11,1,'2023-12-5',3),
		(22,2,'2024-01-15',5),
		(33,3,'2024-01-01',10),
		(44,4,'2023-12-11',7);

		
--SELECT * FROM Orders;