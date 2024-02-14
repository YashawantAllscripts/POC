USE ProductOrder;
GO;
--Create a product table

CREATE TABLE Product(
ID INT PRIMARY KEY,
Name VARCHAR(50) NOT NULL UNIQUE,
Price DECIMAL(10,2) NOT NULL, 
Manufacturer VARCHAR(100) NOT NULL);

--SELECT * FROM Product;


