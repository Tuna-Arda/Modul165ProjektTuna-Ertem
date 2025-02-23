#!/bin/bash

if [ "$1" == "backup" ]; then
    mongodump --uri="mongodb://localhost:27017/Jetstream" --out=./backup
    echo "Backup erstellt im Ordner ./backup."
elif [ "$1" == "restore" ]; then
    mongorestore --uri="mongodb://localhost:27017/Jetstream" ./backup/Jetstream
    echo "Datenbank aus Backup wiederhergestellt."
else
    echo "Usage: $0 [backup|restore]"
fi
