open System.IO

let parseFilePath args =
    match args with
    | [] -> failwith "You must supply a file path"
    | _ :: path :: _ -> path
    | _ -> failwith "You must supply a file path"

/// Given: https://github.com/<username>/<reponame>/
/// Return (<username>, <reponame>)
let nameAndRepo (url: string) = 
    let e = lazy failwith "Invalid GitHub URL provided: {url}"
    in 
        let separateNameRepo (x: string) =
            match x.Trim('/').Split("/") |> Array.toList with
                | [] ->  e.Force()
                | username :: repo :: _ -> (username, repo)
                | _ -> e.Force()
        match url.Split(".com/") |> Array.toList with
            | [] -> e.Force()
            | _ :: x :: _ -> separateNameRepo x
            | _ -> e.Force()

let archiveUrl (userName, repoName): string =
    $"https://api.github.com/repos/{userName}/{repoName}/tarball"

let urls = 
    fsi.CommandLineArgs
    |> Array.toList
    |> parseFilePath
    |> File.ReadLines
    |> Seq.toList
    |> List.filter (fun s -> s.Length > 0)
    |> List.map nameAndRepo
    |> List.map archiveUrl
    /// TODO: find a way to bulk download (Promise.all equivalent in F#)