task Clean {
    dotnet clean

    Remove-Item '.\**\bin\' -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item '.\**\obj\' -Recurse -Force -ErrorAction SilentlyContinue
}

task Test-Error-Messages {
    $SavedErrorActionPreference = $ErrorActionPreference
    Push-Location ".\shc"
    try {
        $ErrorActionPreference="Continue"

        Get-ChildItem "..\test\error-messages" -Filter *.shl | Foreach-Object {
            $output = (& dotnet run $_.FullName 2>&1) | Out-String

            if (-not $output.Contains("Compilation failed"))
            {
                throw "Expected compilation to fail.";
            }

            $expectedError = (Get-Content $_.FullName -First 1).substring(2);

            if (-not $output.Contains($expectedError))
            {
                throw "Expected error condition to match. Got (" + $output + ") Expected (" + $expectedError + ")"
            }
        }
    }
    finally {
        Pop-Location
        $ErrorActionPreference = $SavedErrorActionPreference
    }
}

task Test-Assertions {
    Push-Location ".\shc"
    try {
        Get-ChildItem "..\test\assertions" -Filter *.shl | Foreach-Object {
            Exec { dotnet run $_.FullName }

            $executable = [IO.Path]::ChangeExtension($_.FullName, ".exe");

            Exec { & $executable }
        }
    }
    finally {
        Pop-Location
    }
}

task Test Build, Test-Error-Messages, Test-Assertions

task Build {
    Exec { dotnet build . }
}

task . Build
