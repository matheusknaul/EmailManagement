CREATE DATABASE IF NOT EXISTS email_management;
USE email_management;

CREATE TABLE IF NOT EXISTS user(
	id INT auto_increment PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    avatar_url VARCHAR(255),
    created_at DATETIME NOT NULL    
);

CREATE TABLE IF NOT EXISTS folder(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    is_system BOOLEAN DEFAULT FALSE,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS recipient(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS email(
	id INT AUTO_INCREMENT PRIMARY KEY,
    subject VARCHAR(255) NOT NULL,
    body TEXT,
    date_sent DATETIME NOT NULL,
	recipient_id INT NOT NULL,
    FOREIGN KEY (recipient_id) REFERENCES recipient(id)
    
);

CREATE TABLE folder_email(
	folder_id INT,
    email_id INT,
    PRIMARY KEY (folder_id, email_id),
    FOREIGN KEY (folder_id) REFERENCES folder(id) ON DELETE CASCADE,
    FOREIGN KEY (email_id) REFERENCES email(id) ON DELETE CASCADE
);


