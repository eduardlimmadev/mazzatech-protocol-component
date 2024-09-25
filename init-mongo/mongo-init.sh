#!/bin/bash
# Inicializa o MongoDB com o arquivo JSON de seed

mongoimport --host localhost --db FileServiceDb --collection files --type json --file /docker-entrypoint-initdb.d/insert_file.json --jsonArray