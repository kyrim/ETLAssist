module Definitions

open FSharp.Json

type Definition =
    { TargetFieldName: string
      PossibleColumnNames: string []
      PossibleDataValues: string [] }

type Definitions = { Definitions: Definition [] }

let config =
    JsonConfig.create (jsonFieldNaming = Json.snakeCase)

let DeserialiseJsonFromFile filePath =
    System.IO.File.ReadAllText filePath // TODO: Exceptions
    |> Json.deserializeEx<Definitions> config // TODO: Catch deserialisation issues
