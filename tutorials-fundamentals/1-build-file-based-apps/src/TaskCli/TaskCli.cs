using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
/*
https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/system-command-line

dotnet TaskCli.cs -- add "Write documentation" --priority High --due 2026-04-01
dotnet TaskCli.cs -- list --all
dotnet TaskCli.cs -- complete 3
dotnet TaskCli.cs -- remove 3
dotnet TaskCli.cs -- --verbose list
*/

Console.WriteLine("Hello, TaskCli!");

////////////////////////////////
// Define options and arguments
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/system-command-line#define-options-and-arguments
var verboseOption = new Option<bool>("--verbose") {
    Description = "Show detailed output",
    Recursive = true
};
var priorityOption = new Option<Priority>("--priority") {
    Description = "Task priority level",
    DefaultValueFactory = _ => Priority.Medium
};
var dueOption = new Option<DateOnly?>("--due") {
    Description = "Due date (uses current culture date format)"
};
var allOption = new Option<bool>("--all") {
    Description = "Include completed tasks"
};

var descriptionArgument = new Argument<string>("description") {
    Description = "Task description"
};
var taskIdArgument = new Argument<int>("id") {
    Description = "Task ID"
};

/////////////////////////////////////
// Build commands and subcommands
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/system-command-line#build-commands-and-subcommands
var addCommand = new Command("add", "Add a new task") {
    Arguments = { descriptionArgument },
    Options = { priorityOption, dueOption }
};
var listCommand = new Command("list", "List all tasks") {
    Options = { allOption }
};
var completeCommand = new Command("complete", "Mark a task as complete") {
    Arguments = { taskIdArgument }
};
var removeCommand = new Command("remove", "Remove a task") {
    Arguments = { taskIdArgument }
};

var rootCommand = new RootCommand("A simple task tracker CLI") {
    Options = { verboseOption },
    Subcommands = { addCommand, listCommand, completeCommand, removeCommand }
};

addCommand.SetAction(parseResult =>
{
    var description = parseResult.GetValue(descriptionArgument)!;
    var priority = parseResult.GetValue(priorityOption);
    var due = parseResult.GetValue(dueOption);
    var verbose = parseResult.GetValue(verboseOption);

    var tasks = LoadTasks();
    var id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
    var task = new TaskItem(id, description, priority, due, false);
    tasks.Add(task);
    SaveTasks(tasks);

    Console.WriteLine($"Added task {id}: {description}");
    if (verbose)
    {
        Console.WriteLine($"  Priority: {priority}");
        if (due is DateOnly dueDate)
        {
            Console.WriteLine($"  Due: {dueDate}");
        }
    }
});

listCommand.SetAction(parseResult =>
{
    var showAll = parseResult.GetValue(allOption);
    var verbose = parseResult.GetValue(verboseOption);

    var tasks = LoadTasks();
    var filtered = showAll ? tasks : tasks.Where(t => !t.IsComplete).ToList();

    if (filtered.Count == 0)
    {
        Console.WriteLine("No tasks found.");
        return;
    }

    foreach (var task in filtered)
    {
        var status = task.IsComplete ? "✓" : " ";
        Console.WriteLine($"  [{status}] {task.Id}: {task.Description}");
        if (verbose)
        {
            Console.WriteLine($"       Priority: {task.Priority}");
            if (task.Due is DateOnly dueDate)
            {
                Console.WriteLine($"       Due: {dueDate}");
            }
        }
    }
});

completeCommand.SetAction(parseResult =>
{
    var id = parseResult.GetValue(taskIdArgument);
    var verbose = parseResult.GetValue(verboseOption);

    var tasks = LoadTasks();
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task is null)
    {
        Console.Error.WriteLine($"Task {id} not found.");
        return -1;
    }

    tasks[tasks.IndexOf(task)] = task with { IsComplete = true };
    SaveTasks(tasks);

    Console.WriteLine($"Completed task {id}: {task.Description}");
    if (verbose)
    {
        Console.WriteLine($"  Priority: {task.Priority}");
    }
    return 0;
});

removeCommand.SetAction(parseResult =>
{
    var id = parseResult.GetValue(taskIdArgument);
    var verbose = parseResult.GetValue(verboseOption);

    var tasks = LoadTasks();
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task is null)
    {
        Console.Error.WriteLine($"Task {id} not found.");
        return -1;
    }

    tasks.Remove(task);
    SaveTasks(tasks);

    Console.WriteLine($"Removed task {id}: {task.Description}");
    if (verbose)
    {
        Console.WriteLine($"  Priority: {task.Priority}");
    }
    return 0;
});

return rootCommand.Parse(args).Invoke();

static string GetTaskFilePath() => Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "taskcli-sample",
    "tasks.json");

static List<TaskItem> LoadTasks()
{
    var path = GetTaskFilePath();
    if (!File.Exists(path))
    {
        return [];
    }

    var json = File.ReadAllText(path);
    return JsonSerializer.Deserialize(json, TaskJsonContext.Default.ListTaskItem) ?? [];
}

static void SaveTasks(List<TaskItem> tasks)
{
    var path = GetTaskFilePath();
    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
    var json = JsonSerializer.Serialize(tasks, TaskJsonContext.Default.ListTaskItem);
    File.WriteAllText(path, json);
}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CA1050 // Declare types in namespaces
public enum Priority { Low, Medium, High }
public record TaskItem(int Id, string Description, Priority Priority, DateOnly? Due, bool IsComplete);
#pragma warning restore CA1050 // Declare types in namespaces
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

[System.Text.Json.Serialization.JsonSourceGenerationOptions(WriteIndented = true)]
[System.Text.Json.Serialization.JsonSerializable(typeof(List<TaskItem>))]
internal sealed partial class TaskJsonContext : System.Text.Json.Serialization.JsonSerializerContext;
