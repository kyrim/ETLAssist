// Learn more about F# at http://fsharp.org

open System
open Argu

type CliArguments =
    | [<Mandatory>]Path of path:string
    | [<Mandatory>]Conn of connectionString:string
    | [<Mandatory>]Type of providerType:string

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Path _ -> "The file path to the JSON definitions file."
            | Conn _ -> "The connection string to connect to the database."
            | Type _ -> "The type of database to be used (MSSQLSERVER, MYSQL, etc)."

let parser = ArgumentParser.Create<CliArguments>(programName = "etlassist.exe")
let usage = parser.PrintUsage()

[<EntryPoint>]
let main argv =
    try 
      let parsed =  parser.Parse argv
      let path = parsed.GetResult Path
      let connectionString = parsed.GetResult Conn
      let providerType = parsed.GetResult Type

      let json = Definitions.DeserialiseJsonFromFile path
 
      0
    with
    | :? ArguParseException as ex ->
        printfn "%s" ex.Message
        1
    | ex ->
        printfn "Internal Error:"
        printfn "%s" ex.Message
        2

