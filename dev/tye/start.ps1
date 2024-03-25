param ()

$choices = @(
  ("&Quit", "Quit"),
  ("&A-UserManagement", "User Management")
)
function show-menu {
   
    $choicedesc = New-Object System.Collections.ObjectModel.Collection[System.Management.Automation.Host.ChoiceDescription]
    for ($i = 0; $i -lt $choices.length; $i++) {
        $choicedesc.Add((New-Object System.Management.Automation.Host.ChoiceDescription $choices[$i]) ) 
    }

    $result = $host.ui.PromptForChoice("Select project", "", $choicedesc, 0)
    $tyeFile = "";
    switch ($result) {
        0 { return; } # Quit
        1 { $tyeFile = "usermanagement"; break }
        Default { Write-Host "Invalid Response!"; return; }
    }

    &tye run --watch --port 8001 "./tye-$tyeFile.yaml"
}

show-menu
