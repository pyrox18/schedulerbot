# SchedulerBot

A Discord bot for scheduling events.

This bot uses the [DSharpPlus](https://dsharpplus.emzi0767.com/) library.

This codebase represents SchedulerBot version 2 and above, which was rewritten to work on top of the .NET Core platform. The previous (1.x) SchedulerBot codebase, which was written for Node.js, can be found at [this repository](https://github.com/pyrox18/schedulerbot-1.0). The previous codebase is no longer maintained, and is only present for display purposes.

## Prerequisites

- [Microsoft .NET Core 2.1](https://www.microsoft.com/net/download)
- [PostgreSQL 10+](https://www.postgresql.org/download/)

## Migrating from v1.0.x to v2.0

A data migration tool is available for migrating data from a MongoDB database that stores data for SchedulerBot v1.0.x to a PostgreSQL database storing data for v2.0. See the readme in the `SchedulerBot.MigrationTool` project for further details.

## Installation and Usage

All command examples given assume the use of a Bash shell.

1. Clone the repository.

```bash
$ git clone https://github.com/pyrox18/SchedulerBot.git
```

2. Create a Discord Developer App with an app bot user at https://discordapp.com/developers/applications/me, and note the app bot user token to place in the configuration file.
3. Ensure that a local PostgreSQL instance is running, and create a database called `schedulerbot`.
4. The provided `appsettings.json` file in `SchedulerBot/SchedulerBot.Client` comes with default settings for the client to run. To override any of these settings, create an `appsettings.Development.json` file in `SchedulerBot/SchedulerBot.Client` with the key-value pairs that you want to override with the same hierarchy:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  },
  "ConnectionStrings": {
    "SchedulerBotContext": "Server=localhost;Database=schedulerbot;Username=someuser;Password=abc123"
  },
  "Bot": {
    "Token": "YOUR_BOT_TOKEN_HERE"
  }
}
```

Replace `YOUR_BOT_TOKEN_HERE` with the app bot user token generated earlier.

5. Restore the solution's dependencies.

```bash
$ cd SchedulerBot
$ dotnet restore SchedulerBot.sln
```

6. Migrate the data model schema to the database.

```bash
$ cd SchedulerBot/SchedulerBot.Data
$ ASPNETCORE_ENVIRONMENT=Development dotnet ef database update -s ../SchedulerBot.Client/SchedulerBot.Client.csproj
```

7. Build and run the `SchedulerBot.Client` project.

```bash
$ cd ../SchedulerBot.Client
$ dotnet build
$ cd bin/Debug/netcoreapp2.1
$ ASPNETCORE_ENVIRONMENT=Development dotnet SchedulerBot.Client.dll
```

Alternatively, open the solution in Visual Studio and debug the `SchedulerBot.Client` project from there.

8. Invite the bot to your server by generating an invite link at https://discordapi.com/permissions.html. At minimum, the bot requires permission to read, send and manage messages.

## Docker Support

This project has support for Docker.

To build and run:

```bash
$ docker build . -t schedulerbot:dev
$ docker run -e "ASPNETCORE_ENVIRONMENT=Development" -d schedulerbot:dev
```

## Production Configuration

Production application settings should be placed in a `appsettings.Production.json` file in the `SchedulerBot.Client` project directory. The contents are similar to the `appsettings.Development.json` file, with the addition of a section for providing the DSN to report errors to for the Sentry error reporting service.

```json
{
  "ConnectionStrings": {
    "SchedulerBotContext": "Server=localhost;Database=schedulerbot;Username=someuser;Password=abc123"
  },
  "Bot": {
    "Token": "YOUR_BOT_TOKEN_HERE"
  }
  "Raven": {
  	"DSN": "https://<key>@sentry.io/<project>"
  }
}
```

Alternatively, these settings can be set with environment variables when running the client. Separate hierarchical keys and array index keys with the `__` (double underscore) separator.

```bash
$ ASPNETCORE_ENVIRONMENT=Production Bot__Token=YOUR_BOT_TOKEN_HERE Prefixes__0=++ Raven__DSN=https://abc@sentry.io/bot dotnet SchedulerBot.Client.dll
```

## License

The SchedulerBot source code is distributed under the GNU General Public License v3.0.

## Contributing

Refer to the CONTRIBUTING.md file for more information on how to contribute to the development of SchedulerBot.

## Discussions

Discuss about the development of SchedulerBot on the #development channel of the [SchedulerBot](https://discord.gg/CRxRn5X) support server.