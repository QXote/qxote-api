# API Qxote
## Disclaimer
These are the results of the Technova 2023 excursion. This was first and foremost to be used as a demo on what an API used by different apps of Qxote could look like.
This project has since changed to not only to be used for demo purposes, but for future development projects as well.

## Getting started

### Git and contributions

- Run `git clone` to clone the project to a local directory.
- Any new features/fixes should be added through a pull request.

### Prerequisites

- [DotNet 9](https://dotnet.microsoft.com/en-us/download)
- [SQL Server 2022](https://info.microsoft.com/ww-landing-sql-server-2022.html?culture=en-us&country=us)
- [Entity Framework Core tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Database

- Create a database called `QxoteDb` in SQL Server
- Run `dotnet ef database update` in the powershell terminal in Visual Studio. Make sure to set the directory to `apiqxote` or explicitly set the directory.
- Run the `DbQxoteInsert.sql` in the database script to get the data.

### API

- Download the neccessary Nuget packages if you haven't already.

## API Documentation
The API uses standard Odata syntax to make restful API calls. For more information please see: [Odata documentation](https://www.odata.org/getting-started/)
