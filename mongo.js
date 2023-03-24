docker run --name test-mongo -dit -p 27017:27017 mongo:latest

db.clients.insertMany(
    Array.from({ length: 100000 }).map((_, index) => ({
            _id: index,
        name: ["Ivan", "Fedor", "Anatoly", "Maksim", "Nickolay", "Sergey", "Yuri", "Kirill", "Alexander", "Dmitry",][index % 10],
        surname: ["Zimin", "Gref", "Kondrashov", "Alekseev", "Borisov", "Lazarev", "Sokolov", "Borodin", "Morozov", "Medvedev",][index % 10],
        birthday_date: [ISODate("1980-12-19T06:01:17.171Z"), ISODate("2000-12-19T06:01:17.171Z"),ISODate("1988-05-02T06:01:17.171Z"), ISODate("2002-01-10T06:01:17.171Z"),ISODate("1976-12-22T06:01:17.171Z"),ISODate("1980-01-25T06:01:17.171Z"),ISODate("1973-08-25T06:01:17.171Z"),ISODate("2001-08-30T06:01:17.171Z"),ISODate("1969-05-08T06:01:17.171Z"),ISODate("1983-09-22T06:01:17.171Z"),][index % 10],
        email:["portele@gmail.com", "draper@gmail.com", "rjones@gmail.com", "mcsporran@gmail.com", "skajan@gmail.com", "aibrahim@gmail.com", "zeitlin@gmail.com", "sequin@gmail.com", "peoplesr@gmail.com", "bebing@gmail.com",][index % 10],
        phone_number:["79096946504", "79093956752", "79097726492", "79096915554", "79092836409", "79090538066", "79093947964", "79096511151", "79094418515", "79090869515",][index % 10],
    }))
);

db.accounts.insertMany(
    Array.from({ length: 100000 }).map((_, index) => ({
        account_number: (100000 + index),
        balance: [253, 6236356, 85311, 6134636, 8223114,243671,2462461,64316161,24561616, 81981851][index % 10],
        opening_date: [ISODate("2011-04-08T06:01:17.171Z"), ISODate("2018-11-25T06:01:17.171Z"), ISODate("2010-12-19T06:01:17.171Z"), ISODate("2012-12-19T06:01:17.171Z"),ISODate("2018-12-19T06:01:17.171Z"),ISODate("2011-06-15T06:01:17.171Z"),ISODate("2005-12-24T06:01:17.171Z"),ISODate("2004-01-21T06:01:17.171Z"),ISODate("2016-09-20T06:01:17.171Z"),ISODate("2008-03-30T06:01:17.171Z"),][index % 10],
        closing_date: [ISODate("2021-04-08T06:01:17.171Z"), ISODate("2023-11-25T06:01:17.171Z"), null,][index % 3],
        owner: index,
        transactions: []
    }))
);

db.transactions.insertMany(
    Array.from({ length: 100000 }).map((_, index) => ({
        date: [ISODate("2020-01-30T06:01:17.171Z"), ISODate("2020-11-24T06:01:17.171Z"), ISODate("2020-02-07T06:01:17.171Z"), ISODate("2020-08-31T06:01:17.171Z"),ISODate("2020-05-11T06:01:17.171Z"),ISODate("2020-11-27T06:01:17.171Z"), ISODate("2020-01-01T06:01:17.171Z"), ISODate("2020-02-03T06:01:17.171Z"), ISODate("2020-10-02T06:01:17.171Z"), ISODate("2020-03-28T06:01:17.171Z"), ISODate("2020-09-10T06:01:17.171Z"),][index % 10],
        amount: [34523623, 2444, 626262, 12356,19424, 145, 13501,123423,1023,10201427][index % 10],
        sender:
        recipient:
        index: index,
}))
);