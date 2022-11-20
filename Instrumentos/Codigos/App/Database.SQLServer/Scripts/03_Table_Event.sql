USE TakeIt;

CREATE TABLE Event (
    Code                    VARCHAR(36)     NOT NULL        PRIMARY KEY,
    StartDate               TIMESTAMP       NOT NULL,
    EndDate                 TIMESTAMP       NOT NULL,
    Location                VARCHAR(200)    NOT NULL,
    Title                   VARCHAR(200)    NOT NULL,
    Description             TEXT            NOT NULL,
    Ticker                  VARCHAR(20)     NOT NULL,
    TokenContractAddress    VARCHAR(100)    NOT NULL,
    ImageUrl                TEXT            NOT NULL,
    AlreadyIssuedTickets    BIGINT          NOT NULL,
    ResaleFeePercentage     DECIMAL(18,2)   NOT NULL
);

CREATE TABLE EventTicketType (
    Code                        VARCHAR(36)     NOT NULL        PRIMARY KEY,
    EventCode                   VARCHAR(36)     NOT NULL,
    TicketName                  VARCHAR(200)    NOT NULL,
    StartDate                   TIMESTAMP       NOT NULL,
    EndDate                     TIMESTAMP       NOT NULL,
    Qualification               INT             NOT NULL,
    PriceBrl                    DECIMAL(18,2)   NOT NULL,
    MetadataFileUrl             TEXT            NOT NULL,
    TotalAvailableTickets       BIGINT          NOT NULL,
    CurrentlyAvailableTickets   BIGINT          NOT NULL,
    CONSTRAINT fk_event_code FOREIGN KEY(EventCode) REFERENCES Event(Code)
);