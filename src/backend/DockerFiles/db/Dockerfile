FROM mysql:5.7.22
EXPOSE 3306
COPY ../Database/migrations /home/database/
COPY ../Database/dataset /home/database/
COPY ../Ci/init_database.sh /docker-entrypoint-initdb.d/init_database.sh
