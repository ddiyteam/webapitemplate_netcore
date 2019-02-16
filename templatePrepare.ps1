#=============================================================================
# Powershell script prepares multi-project zip template 
#
# Before runnig this script:
# - manually export all projects as project teplates in VisualStudio
# - copy all exported zip files to template folder (near ps script)
#
# After runnig this script
# - add prepared multi-project zip template to vsix project as asset for build or simply copy zip file to visual studio template folder (%USERPROFILE%\Documents\Visual Studio 17\Templates\ProjectTemplates)
#=============================================================================

$folderPath = "template";
$baseName = "Service";
$projects = @("API", "BLL", "Configuration", "DAL.MySql", "DbUp.MySql", "BLL.Tests");

if(test-Path "${folderPath}\output")
{
    Remove-Item -LiteralPath "${folderPath}\output" -Force -Recurse;
}

foreach ($project in $projects) {       
    $projectFullName = "${baseName}.${project}";
   
    if(test-Path "${folderPath}\${projectFullName}.zip")
    {
        Expand-Archive "${folderPath}\${projectFullName}.zip" -DestinationPath "${folderPath}\output\${projectFullName}";

        #replaces for others files
        $files = Get-ChildItem "${folderPath}\output\${projectFullName}" *.* -Recurse -Exclude "MyTemplate.vstemplate", *.csproj | Where-Object { ! $_.PSIsContainer };
        foreach ($file in $files)
        {
            (Get-Content $file.PSPath) |
            Foreach-Object { $_ -replace " ${baseName}\.", ' $ext_safeprojectname$.' } |
            Set-Content $file.PSPath;

            (Get-Content $file.PSPath) |
            Foreach-Object { $_  -replace '\$safeprojectname\$', "`$ext_safeprojectname`$.${project}" } |
            Set-Content $file.PSPath;
        }

        #replaces for vstemplate file
        $templateFile = Get-ChildItem "${folderPath}\output\${projectFullName}" "MyTemplate.vstemplate";
        (Get-Content $templateFile.PSPath) |
        Foreach-Object { $_ -replace "TargetFileName=`"${baseName}.", 'TargetFileName="$ext_safeprojectname$.' } |
        Set-Content $templateFile.PSPath;        

        #replaces for csproj file
        $csprojFile = Get-ChildItem "${folderPath}\output\${projectFullName}" *.csproj;
        (Get-Content $csprojFile.PSPath) |
        Foreach-Object { $_ -replace "${baseName}.", '$ext_safeprojectname$.' } |
        Set-Content $csprojFile.PSPath;

        #files renames       
        Rename-Item -Path "${folderPath}\output\${projectFullName}\MyTemplate.vstemplate" -NewName "${project}.vstemplate";

        Write-Host "${projectFullName}.zip was successfully converted" -ForegroundColor green;
    }else {
        Write-Host "${projectFullName}.zip does not exists" -ForegroundColor red;
    }
}

Copy-Item "assets\WebAPI.vstemplate" -Destination "${folderPath}\output"
Copy-Item "assets\Icon.png" -Destination "${folderPath}\output"
Write-Host "WebAPI.vstemplate was copied" -ForegroundColor green;

Compress-Archive -Path "${folderPath}\output\*" -CompressionLevel Optimal -DestinationPath "${folderPath}\output\WebAPI_Template"
Write-Host "Template was successfully archived" -ForegroundColor green;