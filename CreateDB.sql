CREATE TABLE IF NOT EXISTS Persons (
    id   INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR
);

CREATE TABLE IF NOT EXISTS Info (
    InfoID      INTEGER PRIMARY KEY AUTOINCREMENT,
    Description VARCHAR,
    Link        VARCHAR,
    PersonID    INTEGER,
    FOREIGN KEY (
        PersonID
    )
    REFERENCES Person (id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Images (
    ImagesID INTEGER PRIMARY KEY AUTOINCREMENT,
    Image    BLOB,
    PersonID INTEGER,
    FOREIGN KEY (
        PersonID
    )
    REFERENCES Persons (id) ON DELETE CASCADE
);
