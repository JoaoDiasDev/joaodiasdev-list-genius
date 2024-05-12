INSERT INTO `shared_products`
(`name`, `value`, `description`, `qrcode`, `image`, `link`, `enabled`, `created_date`, `updated_date`, `unit`, `id_groups`, `id_sub_groups`)
VALUES
('Product 1', 10.00, 'Description for Product 1', 'qrcode1', 0x0123456789ABCDEF, 'https://example.com/product1', TRUE, NOW(), NOW(), 'Meter', 1, 1),
('Product 2', 20.00, 'Description for Product 2', 'qrcode2', 0x0123456789ABCDEF, 'https://example.com/product2', TRUE, NOW(), NOW(), 'SquareMeter', 1, 2),
('Product 3', 30.00, 'Description for Product 3', 'qrcode3', 0x0123456789ABCDEF, 'https://example.com/product3', TRUE, NOW(), NOW(), 'CubicMeter', 2, 3),
('Product 4', 40.00, 'Description for Product 4', 'qrcode4', 0x0123456789ABCDEF, 'https://example.com/product4', TRUE, NOW(), NOW(), 'Unit', 2, 4),
('Product 5', 50.00, 'Description for Product 5', 'qrcode5', 0x0123456789ABCDEF, 'https://example.com/product5', TRUE, NOW(), NOW(), 'Meter', 3, 5),
('Product 6', 60.00, 'Description for Product 6', 'qrcode6', 0x0123456789ABCDEF, 'https://example.com/product6', TRUE, NOW(), NOW(), 'SquareMeter', 3, 1),
('Product 7', 70.00, 'Description for Product 7', 'qrcode7', 0x0123456789ABCDEF, 'https://example.com/product7', TRUE, NOW(), NOW(), 'CubicMeter', 4, 2),
('Product 8', 80.00, 'Description for Product 8', 'qrcode8', 0x0123456789ABCDEF, 'https://example.com/product8', TRUE, NOW(), NOW(), 'Unit', 4, 3),
('Product 9', 90.00, 'Description for Product 9', 'qrcode9', 0x0123456789ABCDEF, 'https://example.com/product9', TRUE, NOW(), NOW(), 'Meter', 5, 4),
('Product 10', 100.00, 'Description for Product 10', 'qrcode10', 0x0123456789ABCDEF, 'https://example.com/product10', TRUE, NOW(), NOW(), 'SquareMeter', 5, 5);
