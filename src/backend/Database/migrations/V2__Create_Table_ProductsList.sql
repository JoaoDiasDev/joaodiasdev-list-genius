CREATE TABLE IF NOT EXISTS `products_list` (
  `id` BIGINT(11) AUTO_INCREMENT PRIMARY KEY,
  `name` longtext,
  `description` longtext,
  `enabled` BOOLEAN DEFAULT TRUE NOT NULL,
  `public` BOOLEAN DEFAULT FALSE NOT NULL,
  `total_products_count` INT NOT NULL,
  `total_products_value` DECIMAL(65,2) NOT NULL,
  `created_date` DATETIME(6) NOT NULL,
  `updated_date` DATETIME(6) NOT NULL,
  `id_users` BIGINT(11) NOT NULL,
  UNIQUE `name` (`name`),
  FOREIGN KEY (`id_users`) REFERENCES `users`(`id`),
)
ENGINE=InnoDB DEFAULT CHARSET=utf8;
