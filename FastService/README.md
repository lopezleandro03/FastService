# FastService WebApp

This article includes things to remember...

## Build and Deploy is handled using Github actions

We build on a windows job because it is a .NET app. Eventually, we will migrate to .NET core.
We deploy on an ubuntu job because we use tasks that only run on Linux jobs (string replace task).

## Database credentials

The database credentials are tokenized and injected as part of the build process. The following Github secrets must exists for the build process to work:

- DB_USER
- DB_PWD
- AZURE_CREDENTIALS

## Changelog

| When | What | 
| ----------- | ----------- |
| 6/5/2017 | Somewhere in 2017 we went live |
| 6/5/2019 | Migrate from onprem machine to Azure App Service and Azure SQL database |
| 6/5/2021 | Replace .rdlc reports with pure HTML reports, downscale AppService after removing .rdlc dependencies |
| 6/6/2021 | Introduce github actions CI/CD |
