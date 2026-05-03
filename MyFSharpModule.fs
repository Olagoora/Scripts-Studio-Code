module MyFSharpModule

let analyzeCode code =
    if System.String.IsNullOrEmpty(code) then "Code vide"
    else sprintf "F# analysé : %d caractères" code.Length

let secureExecute script =
    sprintf "Script exécuté en sécurité : %s" script