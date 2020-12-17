FROM boxfuse/flyway
WORKDIR /migration
COPY write/ .
ENTRYPOINT flyway migrate -user=$SA_USER -password=$SA_PASSWORD -url="jdbc:sqlserver://${MSSQL_WRITE_HOST}:${MSSQL_WRITE_PORT}" -locations="filesystem:."