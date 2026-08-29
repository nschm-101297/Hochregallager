USE WarehouseManagement;

CREATE TABLE prod.StoredItems
(
    Id int IDENTITY PRIMARY KEY,
    SerialNumber int NOT NULL,
    PlaceNumber int NULL,
    InputTime datetime2 NULL,
    OutputTime datetime2 NULL
);