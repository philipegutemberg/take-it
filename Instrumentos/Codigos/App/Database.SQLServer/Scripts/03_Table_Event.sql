USE TakeIt;

CREATE TABLE [Event] (
    Code                    VARCHAR(36)     NOT NULL        PRIMARY KEY,
    StartDate               DATETIME        NOT NULL,
    EndDate                 DATETIME        NOT NULL,
    [Location]              VARCHAR(200)    NOT NULL,
    Title                   VARCHAR(200)    NOT NULL,
    [Description]           VARCHAR(MAX)    NOT NULL,
    Ticker                  VARCHAR(20)     NOT NULL,
    TokenContractAddress    VARCHAR(100)    NOT NULL,
    ImageUrl                VARCHAR(MAX)    NOT NULL,
    AlreadyIssuedTickets    BIGINT          NOT NULL
);

CREATE TABLE [EventTicketType] (
    Code                        VARCHAR(36)     NOT NULL        PRIMARY KEY,
    EventCode                   VARCHAR(36)     NOT NULL        FOREIGN KEY REFERENCES [Event](Code),
    TicketName                  VARCHAR(200)    NOT NULL,
    StartDate                   DATETIME        NOT NULL,
    EndDate                     DATETIME        NOT NULL,
    Qualification               INT             NOT NULL,
    PriceBrl                    DECIMAL(18,2)   NOT NULL,
    MetadataFileUrl             VARCHAR(MAX)    NOT NULL,
    TotalAvailableTickets       BIGINT          NOT NULL,
    CurrentlyAvailableTickets   BIGINT          NOT NULL
);