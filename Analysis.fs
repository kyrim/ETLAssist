module ETLAssist.Analysis

open ETLAssist.Definitions
open ETLAssist.Extraction

type AnalysisResult =
    { TargetFieldName: string
      PotentialSourceFields: List<SchemaColumn> }

let analyse (definitions: List<Definition>) (schema: List<SchemaColumn>) =
    Ok List.empty<AnalysisResult>
