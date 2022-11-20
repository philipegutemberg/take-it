USE TakeIt;

CREATE TABLE User_Backoffice (
    Id              INT             NOT NULL        GENERATED ALWAYS AS IDENTITY,
    Code            VARCHAR(36)     NOT NULL        PRIMARY KEY,
    Username        VARCHAR(100)    NOT NULL,
    Password        VARCHAR(100)    NOT NULL
);

CREATE TABLE User_Gatekeeper (
    Id              INT             NOT NULL        GENERATED ALWAYS AS IDENTITY,
    Code            VARCHAR(36)     NOT NULL        PRIMARY KEY,
    Username        VARCHAR(100)    NOT NULL,
    Password        VARCHAR(100)    NOT NULL
);

CREATE TABLE User_Customer (
    Id                  INT             NOT NULL        GENERATED ALWAYS AS IDENTITY,
    Code                VARCHAR(36)     NOT NULL        PRIMARY KEY,
    Username            VARCHAR(100)    NOT NULL,
    Password            VARCHAR(100)    NOT NULL,
    FullName            VARCHAR(150)    NOT NULL,
    Email               VARCHAR(100)    NOT NULL,
    Phone               VARCHAR(100)    NOT NULL,
    WalletAddress       VARCHAR(100)    NOT NULL,
    InternalAddress     VARCHAR(100)    NULL
);

INSERTO INTO User_Backoffice (Code, Username, Password) VALUES ('d51d1355-0da2-4cc2-abf1-d52924a4d2d5', 'philipe.bo', 'ea71c25a7a602246b4c39824b855678894a96f43bb9b71319c39700a1e045222')