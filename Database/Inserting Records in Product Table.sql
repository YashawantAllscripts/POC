USE ProductOrder;
GO;

--Insert some records in Product Table


INSERT INTO Product(ID,Name,Price,Manufacturer) 
	VALUES
	(1,'Laptop',100000.20,'Lenovo'),
	(2,'Headphone',4000,'Sennheiser'),
	(3,'IPhone',124000,'Apple'),
	(4,'Desktop',18000,'HP');


--SELECT * FROM Product;