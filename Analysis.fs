module Analysis

open Definitions
open Extraction

type AnalysisResult =
    { TargetFieldName: string
      PotentialSourceFields: SchemaColumn [] }

let analyse (definitions: List<Definition>) (schema: List<SchemaColumn>): List<AnalysisResult> =
    List.empty<AnalysisResult>
