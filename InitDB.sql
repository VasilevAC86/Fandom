INSERT INTO Person (name) VALUES ('Doctor David Livesey'), ('John Silver'), ('William "Billy" Bones');
INSERT INTO Info (Description, PersonID) VALUES ('Доктор Ливси', (SELECT id FROM Person WHERE name LIKE '%Livesey%'));
INSERT INTO Info (Description, PersonID) VALUES ('Джон Силвер', (SELECT id FROM Person WHERE name LIKE '%Silver%'));
INSERT INTO Info (Description, PersonID) VALUES ('Билли Бонс', (SELECT id FROM Person WHERE name LIKE '%Bones%'));