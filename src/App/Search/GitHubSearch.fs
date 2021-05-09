namespace GitHubSearch

open Parser

[<AutoOpen>]
module GitHubSearch =

    let BaseSearchUrl = "https://api.github.com"
    let BaseIssueUrl = "https://github.com"

    let searchUrl term =
        $"{BaseSearchUrl}/search/repositories?q={term}"

    let openIssueUrl (res: RepoResult) =
        $"{BaseIssueUrl}/{res.UserName}/{res.RepoName}/issues"
