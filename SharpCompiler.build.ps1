task Clean {
    dotnet clean

    Remove-Item '.\**\bin\' -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item '.\**\obj\' -Recurse -Force -ErrorAction SilentlyContinue
}

task Test-Incorrect {
    Push-Location ".\shc"
    try {    
        Get-ChildItem "..\test\incorrect" -Filter *.shl | Foreach-Object {
            $output = (& dotnet run $_ 2>&1) | Out-String

            if (-not $output.Contains("Compilation failed"))
            {
                throw "Expected compilation to fail.";
            }

            $expectedError = (Get-Content $_ -First 1).substring(2);

            if (-not $output.Contains($expectedError))
            {
                throw "Expected error condition to match."
            }
        }
    } 
    finally {
        Pop-Location
    }
}

task Test-Correct {
    Push-Location ".\shc"
    try {    
        Get-ChildItem "..\test\correct" -Filter *.shl | Foreach-Object {
            Exec { dotnet run $_.FullName }

            $executable = [IO.Path]::ChangeExtension($_.FullName, ".exe");

            Exec { & $executable }
        }
    } 
    finally {
        Pop-Location
    }
}

task Test Build, Test-Incorrect, Test-Correct

task Build {
    Exec { dotnet build . }
}

task . Build
