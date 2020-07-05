module ETLAssist.Extraction

type SchemaColumn =
    { FieldName: string
      TableName: string
      SampleDataValues: string [] }

type ConnectionString = string

type ProviderType = Postgres