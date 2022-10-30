USE TakeIt;

CREATE TABLE User_Backoffice (
    Id              INT             NOT NULL        IDENTITY,
    Code            VARCHAR(36)     NOT NULL        PRIMARY KEY,
    Username        VARCHAR(100)    NOT NULL,
    [Password]      VARCHAR(100)    NOT NULL
);

CREATE TABLE User_Gatekeeper (
    Id              INT             NOT NULL        IDENTITY,
    Code            VARCHAR(36)     NOT NULL        PRIMARY KEY,
    Username        VARCHAR(100)    NOT NULL,
    [Password]      VARCHAR(100)    NOT NULL
);

CREATE TABLE User_Customer (
    Id              INT             NOT NULL        IDENTITY,
    Code            VARCHAR(36)     NOT NULL        PRIMARY KEY,
    Username        VARCHAR(100)    NOT NULL,
    [Password]      VARCHAR(100)    NOT NULL,
    FullName        VARCHAR(150)    NOT NULL,
    Email           VARCHAR(100)    NOT NULL,
    Phone           VARCHAR(100)    NOT NULL,
    WalletAddress   VARCHAR(100)    NOT NULL
);