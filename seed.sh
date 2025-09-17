#!/bin/bash

DB="sample.db"

sqlite3 $DB <<EOF
-- Users
INSERT INTO Users (Name, Email, PasswordHash, Role, CreatedAt) VALUES
('Alice Johnson', 'alice@example.com', 'hashed_pw1', 'User', CURRENT_TIMESTAMP),
('Bob Smith', 'bob@example.com', 'hashed_pw2', 'Admin', CURRENT_TIMESTAMP);

-- Categories
INSERT INTO Categories (Name, Description) VALUES
('Electronics', 'Electronic gadgets and devices'),
('Books', 'Various kinds of books');

-- Products
INSERT INTO Products (Name, Description, Price, CategoryId) VALUES
('Smartphone', 'Latest Android smartphone', 599.99, 1),
('Laptop', 'Lightweight business laptop', 1099.50, 1),
('Novel', 'Best-selling fiction novel', 15.99, 2);

-- Orders
INSERT INTO Orders (UserId, OrderDate, TotalAmount, Status) VALUES
(1, CURRENT_TIMESTAMP, 615.98, 'Pending'),
(2, CURRENT_TIMESTAMP, 1099.50, 'Completed');

-- OrderItems
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES
(1, 1, 1, 599.99),
(1, 3, 1, 15.99),
(2, 2, 1, 1099.50);

-- Payments
INSERT INTO Payments (OrderId, PaymentDate, Amount, PaymentMethod, Status) VALUES
(1, CURRENT_TIMESTAMP, 615.98, 'Credit Card', 'Pending'),
(2, CURRENT_TIMESTAMP, 1099.50, 'PayPal', 'Completed');

-- ShippingAddresses
INSERT INTO ShippingAddresses (UserId, AddressLine1, AddressLine2, City, State, PostalCode, Country, CreatedAt) VALUES
(1, '123 Main St', '', 'New York', 'NY', '10001', 'USA', CURRENT_TIMESTAMP),
(2, '456 Oak Ave', 'Apt 5B', 'Los Angeles', 'CA', '90001', 'USA', CURRENT_TIMESTAMP);
EOF
