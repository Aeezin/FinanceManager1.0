CREATE TABLE Users (
    UserID SERIAL PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(50) NOT NULL,
    Salt VARCHAR(100) NOT NULL
);

CREATE TABLE Account (
    AccountID SERIAL PRIMARY KEY,
    UserID INT REFERENCES Users(UserID),
    AccountName VARCHAR(50) NOT NULL,
    Balance DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Transactions (
    TransactionID SERIAL PRIMARY KEY,
    AccountID INT REFERENCES Accounts(AccountID),
    Amount DECIMAL (10, 2) NOT NULL,
    Description TEXT,
    TransactionDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);