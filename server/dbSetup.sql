CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'id for the recipe',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title CHAR(255) NOT NULL,
        instructions CHAR(255) NOT NULL,
        img VARCHAR(1000) NOT NULL,
        category CHAR(255) NOT NULL,
        creatorId CHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id)
    ) default charset utf8 COMMENT '';

INSERT INTO
    recipes(
        title,
        instructions,
        img,
        category,
        creatorId
    )
VALUES (
        "Lasagna",
        "Take Lasagna to a whole new level.",
        "https://images.unsplash.com/photo-1633436374961-09b92742047b?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fExhc2FnbmF8ZW58MHx8MHx8fDA%3D",
        "entree",
        "652ef49aff9881f8395eb350"
    );

SELECT rec.*, acc.*
FROM recipes rec
    JOIN accounts acc ON rec.creatorID = acc.id
WHERE rec.id = 1;

DROP TABLE recipes;