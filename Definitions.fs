module ETLAssist.Definitions

open FSharp.Json

type Definition =
    { TargetFieldName: string
      PossibleFieldNames: List<string>
      PossibleDataValues: List<string> }

type Definitions = { Definitions: List<Definition> }

let config =
    JsonConfig.create (jsonFieldNaming = Json.snakeCase)

let DeserialiseJsonFromFile filePath =
    System.IO.File.ReadAllText filePath // TODO: Exceptions
    |> Json.deserializeEx<Definitions> config // TODO: Catch deserialisation issues
