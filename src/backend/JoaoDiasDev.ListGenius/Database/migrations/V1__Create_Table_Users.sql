CREATE TABLE IF NOT EXISTS `users` (
	`id` BIGINT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`user_name` VARCHAR(50) NOT NULL DEFAULT '0',
	`password` VARCHAR(130) NOT NULL DEFAULT '0',
	`image` BLOB NOT NULL,
	`email` VARCHAR(100) NOT NULL DEFAULT 'email@email.com',
	`full_name` VARCHAR(120) NOT NULL,
	`enabled` BOOLEAN DEFAULT TRUE NOT NULL,
	`refresh_token` VARCHAR(500) NULL DEFAULT '0',
	`refresh_token_expiry_time` DATETIME NULL DEFAULT NULL,
	`role` ENUM('Unspecified', 'SysAdmin', 'NormalUser') NOT NULL,
	UNIQUE `user_name` (`user_name`),
	UNIQUE `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;