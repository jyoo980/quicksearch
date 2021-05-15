namespace Parser

open FSharp.Data

type RepoResult = { UserName: string; FullUrl: string; RepoName: string }
type GitHub = JsonProvider<"https://api.github.com/search/repositories?q=scala">

[<AutoOpen>]
module ParserUtil =

    let toRepoResult (fullName: string, repoUrl) =
        match fullName.Split("/") |> Array.toList with
        | [] -> failwith $"Bad response from GitHub: {fullName}"
        | userName :: repoName :: _ -> { UserName = userName; FullUrl = repoUrl; RepoName = repoName }
        | _ -> failwith $"Bad response from GitHub: {fullName}"

    let parseReposWithTerm (url: string) =
        let response = GitHub.Load(url)
        response.Items
        |> Seq.map (fun repo -> (repo.FullName, repo.HtmlUrl))
        |> Seq.map toRepoResult
        |> Seq.toList
