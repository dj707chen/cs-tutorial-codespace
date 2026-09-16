using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebAPIClient;

using HttpClient client = new();
/*
using declaration (C# 8). This is not a using directive like the ones on lines 1–3 that import namespaces.
Placed in front of a variable declaration, using means "dispose this object when it goes out of scope."
Since the variable is at the top level of the file, its scope is the whole program, so client.Dispose() 
runs automatically when Main finishes — even if an exception is thrown. It's equivalent to the older block form:
    using (HttpClient client = new())
    {
        // ... rest of the program ...
    }
How does the type HttpClient come into visibility at line 5?
    HttpClient lives in the System.Net.Http namespace, and nothing in Program.cs imports it explicitly. It's visible because of <ImplicitUsings>enable</ImplicitUsings> in WebAPIClient.csproj.
    That property makes the SDK generate a file at build time containing global using directives (which apply to every file in the project). Above is the actual generated file, obj/.../WebAPIClient.GlobalUsings.g.cs, and global using System.Net.Http; is in it.
    The set depends on the SDK: Microsoft.NET.Sdk gives the seven above; Microsoft.NET.Sdk.Web adds ASP.NET namespaces like Microsoft.AspNetCore.Builder.
*/

client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

var repositories = await ProcessRepositoriesAsync(client);
foreach (var repo in repositories ?? Enumerable.Empty<Repository>())
{
    Console.WriteLine($"Name: {repo.Name}");
    Console.WriteLine($"Homepage: {repo.Homepage}");
    Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
    Console.WriteLine($"Description: {repo.Description}");
    Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
    Console.WriteLine($"Last push(UTC):   {repo.LastPushUtc}");
    Console.WriteLine($"Last push(Local): {repo.LastPush}");
    Console.WriteLine();
}

static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
{
    // var json = await client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

    var repositories = await client.GetFromJsonAsync<List<Repository>>("https://api.github.com/orgs/dotnet/repos");
    // The GetFromJsonAsync method is generic, which means you supply type arguments for what kind of objects should be created from the fetched JSON text.
    // Note: There is this sentence in the section "Deserialize the JSON Result" of the tutorial(https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient#deserialize-the-json-result):
    //      The first argument to GetFromJsonAsync method is an await expression.
    // This is not correct. The first argument to GetFromJsonAsync method is the request URI string

    return repositories ?? new(); // The compiler generates the Task<T> object for the return value because you've marked this method as async.
}

// Test the program with the following command:
// dotnet run | jq | more

// Deserialize the JSON Result
//   https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient#deserialize-the-json-result