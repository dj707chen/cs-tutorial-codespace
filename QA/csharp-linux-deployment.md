# Can a C# program be deployed to Linux?

Yes. A C# program can be deployed to Linux as long as it is built for a .NET runtime supported on that Linux environment.

Typical workflow:

- Write the application in C#.
- Build it with the .NET SDK.
- Publish it for the target platform and runtime.
- Copy the output to the Linux machine.
- Run it with the appropriate .NET runtime installed.

Example:

```bash
dotnet publish -c Release -r linux-x64 --self-contained false
```

This creates a deployable output for Linux. If you use a framework-dependent app, the Linux machine must have the matching .NET runtime installed.

For a self-contained deployment, the runtime is included in the published output, which can make distribution easier without requiring the target machine to install .NET first.

In short: yes, C# applications are commonly deployed to Linux, and .NET supports this very well.
