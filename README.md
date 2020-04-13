# Backup Utility (.NET Core)

Backup Utility is an app that copies files from multiple source directories to a common target directory.

An example usage would be to configure the target directory within a cloud directory (such as Dropbox/OneDrive etc.) in order to selectively backup files to your cloud provider of choice.
(Multiple configurations can be used/run if you wanted to backup to multiple cloud drives.)

Certain file types or directories may be excluded in order to save space on the target directory. For example, in a developer environment, build type folders (such as bin and obj) can be excluded as these are easily recreated by rebuilding a project/solution.

Supported configuration files are in YAML format.  
<br />
  
## Requirements
Requires .NET Core framework v3.1 to be installed.  
<br />

## Command Line
The portable version of the app (**backuputil.dll**) can be executed on either **Windows** or **Mac**.  
To execute the portable version, open a terminal window and change to the directory containing the app.  
The portable app (**backuputil.dll**) can be executed using the .NET Core framework using the following command:
```sh
$ dotnet backuputil.dll
```  

Alternatively, if the project has been built to target the local platform, the app directory will also contain a native bootstrap file (with an exe/app extension) for executing the app.  
The specific command line with vary depending on the OS:

**Windows:**
```sh
$ backuputil
```

**Mac:**
```sh
$ ./backuputil
```
  
Note: .NET Core projects can also be configured to build a self-contained exe/app, but these are platform specific. The Backup Utility project is configured to be portable by default.
<br />

## Help
Help info can be displayed in the console using the **--help** argument:  
  
Example:
```sh
$ dotnet backuputil.dll --help
```
<br />
  
## Version
Version info can be displayed in the console using the **--version** argument:  
  
Example:
```sh
$ dotnet backuputil.dll --version
```
<br />
  
## Creating a Default Config
A default YAML configuration file can be created using either the **--create** or **-c** argument followed by a config name:  
  
Example:
```sh
$ dotnet backuputil.dll -c config1.yaml
```
<br />

## Running a Backup
The name of the backup configuration file should be passed as a command line argument after either the **--run** or **-r** argument.  
  
Example:  
```sh
$ dotnet backuputil.dll -r config1.yaml
```
