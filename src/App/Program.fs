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
        |> printfn "%A\n"
        0
