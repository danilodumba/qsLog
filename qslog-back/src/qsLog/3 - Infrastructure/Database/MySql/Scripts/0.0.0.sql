create database qslog;

CREATE TABLE `__database_version` (
  `creation_date` DATETIME NOT NULL,
  `version` CHAR(8) NOT NULL,
  PRIMARY KEY (`version`) 
);

insert into __database_version (`creation_date`, `version`) value (now(), '0.0.0');

CREATE TABLE `users` (
  `id` CHAR(36) NOT NULL,
  `name` VARCHAR(100) NOT NULL,
  `user_name` VARCHAR(30) NOT NULL,
  `email` VARCHAR(250) NOT NULL,
  `password` VARCHAR(100) NOT NULL,
  `administrator` BIT NOT NULL,
  PRIMARY KEY (`id`) 
);

CREATE TABLE `projects` (
  `id` CHAR(36) NOT NULL,
  `name` VARCHAR(50) NOT NULL,
  `api_key` CHAR(36) NOT NULL,
  PRIMARY KEY (`id`) 
);

CREATE TABLE `logs` (
  `id` CHAR(36) NOT NULL,
  `project_id` CHAR(36) NOT NULL,
  `description` VARCHAR(500) NOT NULL,
  `source` TEXT,
  `creation` DATETIME NOT NULL,
  `log_type` SMALLINT NOT NULL,
  PRIMARY KEY (`id`) 
);