CREATE TABLE IF NOT EXISTS `products`(
	`id` BIGINT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`name` VARCHAR(255) NOT NULL,
	`value` DECIMAL(65,2) NOT NULL,
	`description` VARCHAR(255) NOT NULL,
	`qrcode` VARCHAR(255) NOT NULL,
	`image`  VARCHAR(255) NOT NULL,
	`link` VARCHAR(255) NOT NULL,
	`enabled` BOOLEAN DEFAULT TRUE NOT NULL,
	`unit` ENUM('Unspecified', 'M', 'M²', 'M³', 'UN') NOT NULL,
	`id_products_list` BIGINT(11) NOT NULL,
	UNIQUE `name` (`name`),
	FOREIGN KEY (`id_products_list`) REFERENCES `products_list`(`id`),
)
ENGINE=InnoDB DEFAULT CHARSET=utf8;
