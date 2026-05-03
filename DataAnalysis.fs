module DataAnalysis

// Analyse professionnelle : statistiques sur une liste
let calculateStats (data: float list) =
    match data with
    | [] -> None
    | _ ->
        let sum = List.sum data
        let count = float (List.length data)
        let mean = sum / count
        let variance = List.map (fun x -> (x - mean) ** 2.0) data |> List.sum |> (fun v -> v / count)
        let stddev = sqrt variance
        let min = List.min data
        let max = List.max data
        Some { Mean = mean; StdDev = stddev; Min = min; Max = max; Count = int count }

type Statistics = {
    Mean: float
    StdDev: float
    Min: float
    Max: float
    Count: int
}

// Analyse professionnelle : détection d'anomalies (écart-type)
let detectAnomalies (data: float list) (threshold: float) =
    match calculateStats data with
    | None -> []
    | Some stats ->
        data
        |> List.mapi (fun i x ->
            let zScore = abs (x - stats.Mean) / (stats.StdDev + 0.001)
            if zScore > threshold then (i, x, zScore) else (-1, x, 0.0))
        |> List.filter (fun (i, _, _) -> i >= 0)

// Analyse professionnelle : filtrage et transformation
let filterAndTransform (data: float list) (minVal: float) (maxVal: float) =
    data
    |> List.filter (fun x -> x >= minVal && x <= maxVal)
    |> List.map (fun x -> x * x)  // Transformation : carré

// Analyse professionnelle : regroupement par catégories
let categorizeData (data: float list) (bins: int) =
    match data with
    | [] -> Map.empty
    | _ ->
        let min = List.min data
        let max = List.max data
        let binSize = (max - min) / float bins

        data
        |> List.fold (fun acc x ->
            let binIndex = int ((x - min) / binSize)
            let binIndex = System.Math.Min(binIndex, bins - 1)
            Map.tryFind binIndex acc
            |> function
            | None -> Map.add binIndex [x] acc
            | Some values -> Map.add binIndex (x :: values) acc
        ) Map.empty
