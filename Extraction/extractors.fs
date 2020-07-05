module ETLAssist.Extraction.Extractors
open ETLAssist.Extraction.Postgres

let getExtractor providerType = 
    match providerType with 
        | Postgres -> postgresExtractor