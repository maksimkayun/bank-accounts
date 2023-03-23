-- docker run --name test-pg -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres
-- dotnet-ef migrations add init --context BankAccount.DataStorage.BankAccountPgContext --output-dir DataStorage/Migrations

CREATE DATABASE bankaccounts
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

CREATE TABLE clients (
                         id SERIAL PRIMARY KEY,
                         name VARCHAR(50) NOT NULL,
                         surname VARCHAR(50) NOT NULL,
                         birthday_date DATE NOT NULL,
                         email VARCHAR(255) NOT NULL,
                         phone_number VARCHAR(20) NOT NULL,
                         accounts INT NOT NULL
);

INSERT INTO clients (name, surname, birthday_date, email, phone_number)
SELECT
    CASE mod(generate_series, 10)
        WHEN 0 THEN 'Ivan'
        WHEN 1 THEN 'Fedor'
        WHEN 2 THEN 'Anatoly'
        WHEN 3 THEN 'Maksim'
        WHEN 4 THEN 'Nickolay'
        WHEN 5 THEN 'Sergey'
        WHEN 6 THEN 'Yuri'
        WHEN 7 THEN 'Kirill'
        WHEN 8 THEN 'Alexander'
        WHEN 9 THEN 'Dmitry'
        END,
    CASE mod(generate_series, 10)
        WHEN 0 THEN 'Zimin'
        WHEN 1 THEN 'Gref'
        WHEN 2 THEN 'Kondrashov'
        WHEN 3 THEN 'Alekseev'
        WHEN 4 THEN 'Borisov'
        WHEN 5 THEN 'Lazarev'
        WHEN 6 THEN 'Sokolov'
        WHEN 7 THEN 'Borodin'
        WHEN 8 THEN 'Morozov'
        WHEN 9 THEN 'Medvedev'
        END,
    CASE mod(generate_series, 10)
        WHEN 0 THEN '1980-12-19'
        WHEN 1 THEN '2000-12-19'
        WHEN 2 THEN '1988-05-02'
        WHEN 3 THEN '2002-01-10'
        WHEN 4 THEN '1976-12-22'
        WHEN 5 THEN '1980-01-25'
        WHEN 6 THEN '1973-08-25'
        WHEN 7 THEN '2001-08-30'
        WHEN 8 THEN '1969-05-08'
        WHEN 9 THEN '1983-09-22'
        END::DATE,
        CASE mod(generate_series, 10)
            WHEN 0 THEN 'portele@gmail.com'
            WHEN 1 THEN 'draper@gmail.com'
            WHEN 2 THEN 'rjones@gmail.com'
            WHEN 3 THEN 'mcsporran@gmail.com'
            WHEN 4 THEN 'skajan@gmail.com'
            WHEN 5 THEN 'aibrahim@gmail.com'
            WHEN 6 THEN 'zeitlin@gmail.com'
            WHEN 7 THEN 'sequin@gmail.com'
            WHEN 8 THEN 'peoplesr@gmail.com'
            WHEN 9 THEN 'bebing@gmail.com'
            END,
    CASE mod(generate_series, 10)
        WHEN 0 THEN '79096946504'
        WHEN 1 THEN '79093956752'
        WHEN 2 THEN '79097726492'
        WHEN 3 THEN '79096915554'
        WHEN 4 THEN '79092836409'
        WHEN 5 THEN '79090538066'
        WHEN 6 THEN '79093947964'
        WHEN 7 THEN '79096511151'
        WHEN 8 THEN '79094418515'
        WHEN 9 THEN '79090869515'
        END
FROM generate_series(1, 100000);


===================================================================================




CREATE TABLE accounts (
                          id SERIAL PRIMARY KEY,
                          account_number INT NOT NULL,
                          balance INT NOT NULL,
                          opening_date DATE NOT NULL,
                          closing_date DATE,
                          owner INT NOT NULL,
                          transactions INT NOT NULL
);

INSERT INTO accounts (account_number, balance, opening_date, closing_date, owner)
SELECT
    CASE mod(generate_series, 10)

        END,
    CASE mod(generate_series, 10)
        WHEN 0 THEN 253
        WHEN 1 THEN 6236356
        WHEN 2 THEN 85311
        WHEN 3 THEN 6134636
        WHEN 4 THEN 8223114
        WHEN 5 THEN 243671
        WHEN 6 THEN 2462461
        WHEN 7 THEN 64316161
        WHEN 8 THEN 24561616
        WHEN 9 THEN 81981851
        END,
    CASE mod(generate_series, 10)
        WHEN 0 THEN '2011-04-08'
        WHEN 1 THEN '2018-11-25'
        WHEN 2 THEN '2010-12-19'
        WHEN 3 THEN '2012-12-19'
        WHEN 4 THEN '2018-12-19'
        WHEN 5 THEN '2011-06-15'
        WHEN 6 THEN '2005-12-24'
        WHEN 7 THEN '2004-01-21'
        WHEN 8 THEN '2016-09-20'
        WHEN 9 THEN '2008-03-30'
        END::DATE,
        CASE mod(generate_series, 3)
            WHEN 0 THEN '2021-04-08'
            WHEN 1 THEN '2023-11-25'
            WHEN 2 THEN NULL
            END::DATE,
        CASE mod(generate_series, 1)
            WHEN 0 THEN NULL
            END
FROM generate_series(1, 100000);


========================================================================================


CREATE TABLE transactions (
                              id SERIAL PRIMARY KEY,
                              date DATE NOT NULL,
                              type VARCHAR(50) NOT NULL,
                              amount INT NOT NULL,
                              sender INT NOT NULL,
                              recipient INT NOT NULL
);

INSERT INTO transactions (date, type, amount, sender, recipient)
SELECT
    CASE mod(generate_series, 10)
        WHEN 0 THEN '2020-01-30'
        WHEN 1 THEN '2020-11-24'
        WHEN 2 THEN '2020-02-07'
        WHEN 3 THEN '2020-08-31'
        WHEN 4 THEN '2020-05-11'
        WHEN 5 THEN '2020-11-27'
        WHEN 6 THEN '2020-01-01'
        WHEN 7 THEN '2020-02-03'
        WHEN 8 THEN '2020-10-02'
        WHEN 9 THEN '2020-03-28'
        END::DATE,
        CASE mod(generate_series, 3)
            WHEN 0 THEN 'remittance'
            WHEN 1 THEN 'replenishment'
            WHEN 2 THEN 'withdrawal'
            END,
    CASE mod(generate_series, 10)
        WHEN 0 THEN 34523623
        WHEN 1 THEN 2444
        WHEN 2 THEN 626262
        WHEN 3 THEN 12356
        WHEN 4 THEN 19424
        WHEN 5 THEN 145
        WHEN 6 THEN 13501
        WHEN 7 THEN 123423
        WHEN 8 THEN 1023
        WHEN 9 THEN 10201427
        END,
    CASE mod(generate_series, 1)
        WHEN 0 THEN NULL
        END,
    CASE mod(generate_series, 1)
        WHEN 0 THEN NULL
        END
FROM generate_series(1, 100000);