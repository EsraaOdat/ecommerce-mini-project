CREATE DATABASE EcommerceDB;
USE EcommerceDB;

-- Create Users Table with Default Image
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    image VARCHAR(MAX) DEFAULT 'https://i.pinimg.com/564x/39/94/fb/3994fb52d1f983d003ed6f084366bdab.jpg'
);


-- Create Categories Table
CREATE TABLE Categories (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(100) NOT NULL,
    image VARCHAR(MAX)
);

-- Create Products Table
CREATE TABLE Products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    product_name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    category_id INT,
    image VARCHAR(MAX),
    FOREIGN KEY (category_id) REFERENCES Categories(category_id) ON DELETE CASCADE
);

-- Create Cart Table
CREATE TABLE Cart (
    cart_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    product_id INT,
    quantity INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES Products(product_id) ON DELETE CASCADE
);

-- Create Orders Table
CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    order_date DATETIME NOT NULL,
    status VARCHAR(50) NOT NULL,
    total_amount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE
);

-- Create OrderItems Table
CREATE TABLE OrderItems (
    order_item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT,
    product_id INT,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES Orders(order_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES Products(product_id) ON DELETE CASCADE
);

-- Insert Sample Data
INSERT INTO Categories (category_name, image) VALUES 
('Gifts', 'https://www.brides.com/thmb/4XSayGNRHjhx7zhMyTAEOweZ4Vo=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/gif-gettyimages-re-215ebb8f3199469184954bdbf85a5d81.jpg'),
('Jewelry', 'https://assets.ynap-content.com/story-metadata-image-1682681758488.jpeg'),
('Home and Living', 'https://i.etsystatic.com/13148455/r/il/61cb9f/4862579425/il_794xN.4862579425_p5s5.jpg');

INSERT INTO Products (product_name, description, price, category_id, image) 
VALUES 
('Silver Necklace', 'A beautiful silver necklace', 49.99, 2, 'https://www.silverbling.ie/cdn/shop/products/Heart_20Centre_20Stone_20Cubic_20Zirconia_20Silver_20Pendant.jpg?v=1665087637&width=493'),  
('Handmade Vase', 'A unique handmade vase', 79.99, 3, 'https://i.etsystatic.com/34878446/r/il/7ca782/6105761712/il_794xN.6105761712_dzb7.jpg'),          
('Gift Card', 'A gift card with customizable amount', 25.00, 1, 'https://i.etsystatic.com/6841042/r/il/bf8320/5353525062/il_794xN.5353525062_l9qg.jpg');

INSERT INTO Users (username, email, password, phone, image) 
VALUES 
('esraa_odat', 'esraa.odat@gmail.com', '123', '0797109414', 'https://i.pinimg.com/564x/39/94/fb/3994fb52d1f983d003ed6f084366bdab.jpg'),
('saja', 'saja@gmail.com', '2003', '0771234567', 'images/users/saja.png'),
('dana', 'dana@gmail.com', '2009', '0789876543', 'images/users/dana.png');

INSERT INTO Orders (user_id, order_date, status, total_amount) 
VALUES 
(1, GETDATE(), 'Processing', 99.98),  
(2, GETDATE(), 'Completed', 79.99);   

INSERT INTO OrderItems (order_id, product_id, quantity, price) 
VALUES 
(1, 1, 2, 49.99),  
(2, 2, 1, 79.99);


USE EcommerceDB;


-- Insert records into the Cart table
INSERT INTO Cart (user_id, product_id, quantity) 
VALUES 
(1, 1, 2),  -- user_id = 1, product_id = 1 (e.g., Silver Necklace), quantity = 2
(2, 2, 1),  -- user_id = 2, product_id = 2 (e.g., Handmade Vase), quantity = 1
(1, 3, 1);  -- user_id = 1, product_id = 3 (e.g., Gift Card), quantity = 1

-- Verify insertion
SELECT * FROM Cart;



-- ????? ?????? ?? ???? Products ?? ?????? Gifts, Jewelry, Home and Living
INSERT INTO Products (product_name, description, price, category_id, image)
VALUES 
('Rose Bouquet', 'A beautiful bouquet of red roses, perfect for special occasions.', 29.99, 1, 'https://i.pinimg.com/736x/e5/48/cb/e548cbbd7b1882814a6655d70b40fc18.jpg'),
('Gold Necklace', 'Elegant 14k gold necklace with a heart-shaped pendant.', 199.99, 2, 'https://i.pinimg.com/564x/95/d2/cd/95d2cd4201d6c1725be4fc7fa0ac271c.jpg'),
('Decorative Vase', 'Handcrafted ceramic vase to add a touch of elegance to your living room.', 45.50, 3, 'https://i.pinimg.com/564x/44/25/4e/44254edc4d8a14591f6cdd108a2e1515.jpg');



-- ????? ?????? ?? ???????? ?? ???? Products
INSERT INTO Products (product_name, description, price, category_id, image)
VALUES 
-- Products for Gifts category (category_id = 1)
('Chocolate Gift Box', 'Assorted gourmet chocolates in a beautifully wrapped gift box.', 34.99, 1, 'https://i.pinimg.com/564x/28/1e/c8/281ec87911f7318d6f531c4a07d215f8.jpg'),
('Personalized Mug', 'A ceramic mug with customizable text and images.', 15.99, 1, 'https://i.pinimg.com/564x/f8/ff/63/f8ff63086818dd1774a3960c6eeaceab.jpg'),

-- Products for Jewelry category (category_id = 2)
('Silver Bracelet', 'Sterling silver bracelet with intricate design.', 89.99, 2, 'https://i.pinimg.com/564x/c3/00/c5/c300c53e0d4b77f9f1f47f81b9057d10.jpg'),
('Diamond Earrings', 'Exquisite diamond stud earrings, perfect for any occasion.', 499.99, 2, 'https://i.pinimg.com/564x/c2/3d/6d/c23d6d95005e6a2b82f24c53ee544d0f.jpg'),

-- Products for Home and Living category (category_id = 3)
('Wall Art', 'Abstract wall art to enhance your living room decor.', 75.00, 3, 'https://i.pinimg.com/564x/56/7d/14/567d1464cb09b33271596eb556e2e5dd.jpg'),
('Throw Pillow', 'Cozy and stylish throw pillow to add comfort and style to your home.', 25.99, 3, 'https://i.pinimg.com/564x/3f/1c/cd/3f1ccd68d24da4b34839ffef8e08960c.jpg');



-- ????? ?????? ?? ???????? ?? ???? Products
INSERT INTO Products (product_name, description, price, category_id, image)
VALUES 
-- Products for Gifts category (category_id = 1)
('Spa Gift Basket', 'A luxurious spa gift basket including bath salts, body lotion, and more.', 59.99, 1, 'https://i.pinimg.com/564x/a3/6a/44/a36a44b655100c77e455d6937b296968.jpg'),

-- Products for Jewelry category (category_id = 2)
('Pearl Necklace', 'Classic pearl necklace with high-quality freshwater pearls.', 149.99, 2, 'https://i.pinimg.com/236x/d8/cd/63/d8cd6335ed6a0d468aa1a3241131267e.jpg'),
('Gold Hoop Earrings', 'Stylish 14k gold hoop earrings with a smooth finish.', 129.99, 2, 'https://i.pinimg.com/564x/a6/6e/f4/a66ef4f645e9f40291d70117e19622cf.jpg'),

-- Products for Home and Living category (category_id = 3)
('Candlestick Holder', 'Elegant brass candlestick holder for a vintage touch.', 39.99, 3, 'https://i.pinimg.com/564x/cb/f7/bd/cbf7bd8dacd42e89c10fe6f7e6498b47.jpg'),
('Area Rug', 'Soft area rug to enhance the comfort and style of your living space.', 120.00, 3, 'https://i.pinimg.com/564x/81/39/7f/81397fa0e493748f9ec0e1bd4fb8d13f.jpg');




INSERT INTO Users (username, email, password, phone) 
VALUES 
('majd', 'majdodat@gmail.com', '123', '0771234567');
