module ETLAssist.Extraction.Postgres

let postgresExtractor (connectionString: ConnectionString) (sampleSize: int): Result<List<SchemaField>, string> = Error ""
