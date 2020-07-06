module ETLAssist.Extraction

type SchemaField =
    { FieldName: string
      TableName: string
      SampleDataValues: List<string> }

type ConnectionString = string

type ProviderType = Postgres