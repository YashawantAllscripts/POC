USE ProductOrder;
--Creating a Order Table

CREATE TABLE Orders(
ID INT  PRIMARY KEY,
ProductID INT NOT NULL,
Date DATE NOT NULL,
Quantity INT NOT NULL,
FOREIGN KEY (ProductID) REFERENCES Product(ID));


