FROM boxfuse/flyway
WORKDIR /migration
COPY read/ .
ENTRYPOINT flyway migrate -user=$SA_USER -password=$SA_PASSWORD -url="jdbc:sqlserver://${MSSQL_READ_HOST}:${MSSQL_READ_PORT}" -locations="filesystem:."