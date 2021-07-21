@echo off

dotnet restore

dotnet build --no-restore -c Release

move /Y Panosen.Markdown.Parsers\bin\Release\Panosen.Markdown.Parsers.*.nupkg D:\LocalSavoryNuget\

pause