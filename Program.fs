open Argu
open ETLAssist.Definitions
open ETLAssist.Extraction
open ETLAssist.Extraction.Extractors
open ETLAssist.Analysis

let SampleSizeDefault = 1000

// The arguments that need to be passed into the program to run.
type CliArguments =
    | [<Mandatory>]Path of path:string
    | [<Mandatory>]Conn of connectionString:string
    | [<Mandatory>]Type of providerType:ProviderType
    | SampleSize of int

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            // Descriptions of each argument
            | Path _ -> "The file path to the JSON definitions file."
            | Conn _ -> "The connection string to connect to the database."
            | Type _ -> "The type of database to be used (MSSQLSERVER, MYSQL, etc)."
            | SampleSize _ -> sprintf "The amount of sample data values per field to retrieve for analysis (Default: %i)" SampleSizeDefault

// So that we can display this in the "help" prompt
let executableName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName 
                        |> System.IO.Path.GetFileName

let parser = ArgumentParser.Create<CliArguments>(programName = executableName)

// We can just ensure we are using Result<a', string> everywhere, and at the end
// of the program, just get the error code based on if there is an error.
let getErrorCode (result: Result<_, string>) = 
    match result with 
        | Error error -> printf "%s" error; 1
        | Ok -> 0

[<EntryPoint>]
let main argv =
    try 
      let parsed = parser.Parse argv

      let path = parsed.GetResult Path
      let connectionString = parsed.GetResult Conn
      let providerType = parsed.GetResult Type
      let sampleSize = parsed.GetResult (SampleSize, defaultValue = SampleSizeDefault)

      let json = DeserialiseJsonFromFile path 
      
      getExtractor providerType connectionString sampleSize
      |> Result.map (analyse json.Definitions)
      |> getErrorCode
    with
    // Ideally we would not need this exception handling,
    // but the Argument parser, deserialisation and file handling
    // all throw.
    | ex ->
        printfn "%s" ex.Message
        1

