module ETLAssist.Analysis

open ETLAssist.Definitions
open ETLAssist.Extraction

type AnalysisResult =
    { TargetFieldName: string
      PotentialTargetFields: List<Definition> }

let stringContains (string1: string) (string2: string) = string1.Contains(string2)

let stringsContains (strings: List<string>) (string: string) = 
    strings |> List.exists (stringContains string)

let similarTo (schemafield: SchemaField) (definition: Definition) = 
    stringContains definition.TargetFieldName schemafield.FieldName
    || definition.PossibleFieldNames |> List.exists (stringContains schemafield.FieldName)
    || definition.PossibleDataValues |> List.exists (stringsContains schemafield.SampleDataValues)

let analyseField (definitions: List<Definition>) (schemafield: SchemaField) = 
    {
        TargetFieldName = schemafield.FieldName
        PotentialTargetFields = definitions |> List.where (similarTo schemafield)
    }
 
let analyse (definitions: List<Definition>) (schemafields: List<SchemaField>) =
    schemafields |> 
        List.map (analyseField definitions)
