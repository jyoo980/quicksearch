open Parser
open GitHubSearch

[<EntryPoint>]
let main argv =
    match argv |> Array.toList with
    | [] -> failwith "Search term required"
    | term :: _ ->  
        searchUrl term
        |> parseReposWithTerm
        |> List.map openIssueUrl
        |> List.iter (fun url -> url |> printfn "%A")
        0
