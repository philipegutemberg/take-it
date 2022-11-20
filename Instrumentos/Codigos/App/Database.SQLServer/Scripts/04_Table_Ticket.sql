USE TakeIt;

CREATE TABLE Ticket (
                        Code                        VARCHAR(36)     NOT NULL        PRIMARY KEY,
                        EventCode                   VARCHAR(36)     NOT NULL,
                        EventTicketTypeCode         VARCHAR(36)     NOT NULL,
                        PurchaseDate                TIMESTAMP       NOT NULL,
                        OwnerCustomerCode           VARCHAR(36)     NULL,
                        TokenId                     BIGINT          NOT NULL,
                        UsedOnEvent                 BIT             NOT NULL,
                        CONSTRAINT fk_event_code FOREIGN KEY(EventCode) REFERENCES Event(Code),
                        CONSTRAINT fk_event_ticket_type_code FOREIGN KEY(EventTicketTypeCode) REFERENCES EventTicketType(Code),
                        CONSTRAINT fk_customer_owner FOREIGN KEY(OwnerCustomerCode) REFERENCES User_Customer(Code)
);