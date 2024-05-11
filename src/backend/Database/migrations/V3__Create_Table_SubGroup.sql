CREATE TABLE IF NOT EXISTS `sub_groups` (
	`id` BIGINT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`name` VARCHAR(100) NOT NULL,
	`image` BLOB NOT NULL,
	`description` VARCHAR(255) NOT NULL,
	`enabled` BOOLEAN DEFAULT TRUE NOT NULL,
	`id_groups` BIGINT(11) NOT NULL,
	FOREIGN KEY (`id_groups`) REFERENCES `groups`(`id`),
	UNIQUE `name` (`name`),
) ENGINE=InnoDB DEFAULT CHARSET=utf8;