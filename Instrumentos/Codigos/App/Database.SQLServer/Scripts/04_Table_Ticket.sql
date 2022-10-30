USE TakeIt;

CREATE TABLE Ticket (
    Code                        VARCHAR(36)     NOT NULL        PRIMARY KEY,
    EventCode                   VARCHAR(36)     NOT NULL        FOREIGN KEY REFERENCES [Event](Code),
    EventTicketTypeCode         VARCHAR(36)     NOT NULL        FOREIGN KEY REFERENCES EventTicketType(Code),
    PurchaseDate                DATETIME        NOT NULL,
    OwnerCustomerCode           VARCHAR(36)     NOT NULL        FOREIGN KEY REFERENCES User_Customer(Code),
    TokenId                     BIGINT          NOT NULL
);