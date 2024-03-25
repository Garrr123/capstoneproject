FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["TaskManager/Private/UserManagement/TaskManager.Platform.Intranet.API/*.csproj", "TaskManager/Private/UserManagement/TaskManager.Platform.Intranet.API/"]
COPY ["TaskManager/Private/UserManagement/TaskManager.Platform.Intranet.Application/*.csproj", "TaskManager/Private/UserManagement/TaskManager.Platform.Intranet.Application/"]

WORKDIR /src/TaskManager/Private/UserManagement/TaskManager.Platform.Intranet.API/
RUN dotnet restore
RUN dotnet build "TaskManager.Platform.Intranet.API.csproj" -c Release -o /app/build

WORKDIR /src
