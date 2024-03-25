$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath

$repositories = @(
    @{  
        Option             = ("&A-Profile", "Profile")
        AppName            = "izprofile"
        ProjectPath        = "../TaskManager/Private/Profile/TaskManager.Profile.Infrastructure/TaskManager.Profile.Infrastructure.csproj"
        StartUpProjectPath = "../TaskManager/Private/Profile/TaskManager.Profile.API/TaskManager.Profile.API.csproj"
        MigrationPath = "./Infrastructure/Migration"
        MigrationScriptPath  = "../TaskManager/Private/Profile/TaskManager.Profile.Infrastructure/Migration/Migration-Script.sql"
    })


$choicedesc = New-Object System.Collections.ObjectModel.Collection[System.Management.Automation.Host.ChoiceDescription]
$choicedesc.Add((New-Object System.Management.Automation.Host.ChoiceDescription ("&Quit", "Quit")) ) 

for ($i = 0; $i -lt $repositories.length; $i++) {
    $choicedesc.Add((New-Object System.Management.Automation.Host.ChoiceDescription $repositories[$i].Option ) ) 
}

$result = $host.ui.PromptForChoice("Select project", "", $choicedesc, 0)
$index = -1;
$index= [int]::Parse($result)
if ( $index -lt 0 -or  $index -gt $repositories.Length + 1){
    Write-Host "Invalid Response"; return;
}
$index = $index  - 1;

$env:ConnectionStrings__default="Data Source=.;Integrated Security=True;Initial Catalog=MHAPortal"
$env:ASPNETCORE_ENVIRONMENT="Production"
if($index -ge 0){
    $migrationName = Read-Host -Prompt 'Input migration name: (leave blank to skip adding migration)'
    if($migrationName -ne ""){
        Write-Host "Generating EF Migration for $($repositories[$index].AppName)"
        &dotnet ef migrations add $migrationName --project $repositories[$index].ProjectPath --startup-project $repositories[$index].StartUpProjectPath -o $repositories[$index].MigrationPath -- --environment Production 
    }
    Write-Host "Generating EF Script for $($repositories[$index].AppName)"
    Write-Host "Project path = $($repositories[$index].ProjectPath)"
    Write-Host "Startup Project path = $($repositories[$index].StartUpProjectPath)"
    Write-Host "Migration Script path = $($repositories[$index].MigrationScriptPath)"
    &dotnet ef migrations script --idempotent --project $repositories[$index].ProjectPath --startup-project $repositories[$index].StartUpProjectPath --output $repositories[$index].MigrationScriptPath -- --environment Production 
}

[Environment]::SetEnvironmentVariable("ConnectionStrings__default",$null,"User")
[Environment]::SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT",$null,"User")
