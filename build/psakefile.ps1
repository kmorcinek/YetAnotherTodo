Framework "4.6"

Properties {
    $solution_name = "KMorcinek.YetAnotherTodo"
    $build_dir = Split-Path $psake.build_script_file
    $build_artifacts_dir = "$build_dir\BuildArtifacts"
    $code_dir = "$build_dir\..\src"
    $xunit_output_dir = "$build_artifacts_dir\TestOutput"

    # nuget.exe that always need to be in repository to allow downloading other dependencies (and newest nuget version),
    # no need to update this file when new minor nuget versions are available
    $nuget_exe_path = "tools\NuGet.exe"
}

FormatTaskName (("-"*25) + "[{0}]" + ("-"*25))

Task Default -Depends Build

Task Build -Depends Clean {
    Write-Host "Restoring NuGet packages for $solution_name" -ForegroundColor Green
    & $nuget_exe_path restore "$code_dir\$solution_name.sln"

    Write-Host "Building $solution_name.sln" -ForegroundColor Green
    RunMsBuild { msbuild "$code_dir\$solution_name.sln" /t:Build /p:Configuration=$build_configuration /v:quiet /p:OutDir=$build_artifacts_dir }
}

Task Clean {
    Write-Host "Creating BuildArtifacts directory" -ForegroundColor Green

    Ensure-Directory-Is-Clean $build_artifacts_dir

    Write-Host "Cleaning $solution_name.sln" -ForegroundColor Green
    RunMsBuild { msbuild "$code_dir\$solution_name.sln" /t:Clean /p:Configuration=$build_configuration /v:quiet }
}

Task Test {
    try {
        Run-Tests-By-Filter "*Tests.dll"
    } catch {
        ReportExceptionForVsoAgent($_)
    }
}

function Run-Tests-By-Filter
{
    Param(
        [Parameter(Mandatory=$true)]
        [string]$filter
    )

    $test_runner_version = "2.3.1"

    # Install test runner via NuGet
    .\tools\NuGet.exe install xunit.runner.console -version $test_runner_version

    $xunit = "xunit.runner.console.$test_runner_version\tools\net452\xunit.console.exe"
    assert(Test-Path($xunit)) "xUnit must be available (check the path again!)."

    Ensure-Directory-Is-Clean $xunit_output_dir

    $testFolders = gci $build_artifacts_dir/$filter

    foreach($testFolder in $testFolders) { 
        write-host $testFolder

        $testFolderPath = $testFolder.FullName

        $test_name = $testFolder.Name
        $test_output_path = "$xunit_output_dir\$test_name.xml"
        write-host "****** found tests: $testFolderPath, output: $test_output_path"

        Exec {
            & $xunit $testFolderPath -xml $test_output_path
        }

        write-host "****** finished testing: $testFolderPath"
    }
}

function RunMsBuild
{
    Param(
      [scriptblock]$script
    )

    $errorLine = ""
    $outputArray = (&$script 2>&1)
    
    Foreach ($outputLine in $outputArray)
    {
        $warningPosition = $outputLine.IndexOf(": warning ")
        if ($warningPosition -eq -1)
        {
            $errorPosition = $outputLine.IndexOf(": error ")
            if ($errorPosition -eq -1)
            {
                Write-Host $outputLine
            } else {
                $errorLine = $outputLine
                Write-Host $outputLine -ForegroundColor Red
            }
        } else {
            Write-Host "##vso[task.logissue type=warning]"$outputLine -ForegroundColor Yellow
        }
    }
    
    if ($lastexitcode -ne 0) {
        throw ($errorLine)
    }
}

function Ensure-Directory-Is-Clean
{
    Param(
        [Parameter(Mandatory=$true)]
		[string]$directoryPath
    )

    if (Test-Path $directoryPath)
    {
        Remove-Item $directoryPath -rec -force | out-null
    }

    mkdir $directoryPath | out-null
}

function ReportExceptionForVsoAgent
{
    #Write result of current sub-task
    if ($RunningTaskGuid -ne "")
    {
        Write-Host "##vso[task.logdetail id=$RunningTaskGuid;state=Completed;result=Failed;finishtime=$(Get-Date)]Failed"
    }

    #Write result of whole script
    # This helped me get all details of the exception: http://stackoverflow.com/questions/38419325/catching-full-exception-message
    $scope = $args[0]

    $formatString = "{0} : {1}`n{2}`n" +
                    "    + CategoryInfo          : {3}`n" +
                    "    + FullyQualifiedErrorId : {4}`n" +
                    "    + Stack Trace : {5}`n" +
                    "    + Script Stack Trace : {6}`n"

    $fields = $scope.InvocationInfo.MyCommand.Name,
              $scope.Exception.Message,
              $scope.InvocationInfo.PositionMessage,
              $scope.CategoryInfo.ToString(),
              $scope.FullyQualifiedErrorId,
              $scope.Exception.StackTrace,
              $scope.ScriptStackTrace

    $formatString = $formatString -f $fields

    Write-Host "`n`n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!`n" -ForegroundColor Cyan

    Write-Host "##vso[task.logissue type=error]"$formatString -ForegroundColor Red
    Write-Host "##vso[task.complete result=Failed;]Error" -ForegroundColor Red

    exit(0)
}