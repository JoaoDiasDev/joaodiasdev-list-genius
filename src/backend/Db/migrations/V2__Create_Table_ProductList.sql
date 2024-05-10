CREATE TABLE IF NOT EXISTS `product_list` (
  `id` INT(11) AUTO_INCREMENT PRIMARY KEY,
  `name` longtext,
  `description` longtext,
  `enabled` BOOLEAN DEFAULT TRUE NOT NULL
  `total_products_count` INT NOT NULL,
  `total_products_value` DECIMAL(65,2) NOT NULL,
  `created_date` DATETIME(6) NOT NULL,
  `updated_date` DATETIME(6) NOT NULL,
)
ENGINE=InnoDB DEFAULT CHARSET=utf8;
