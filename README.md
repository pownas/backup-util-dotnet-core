# Backup Utility (.NET Core)

Backup Utility is a console app that copies files from multiple source directories to a common target directory.  

The app is built as a .NET Core console app rather than a service in order to remain portable between Windows and Mac.  
  
Typical cloud backup services only allow you to sync a single directory. Backup Utility can be used to selectively copy multiple source directories to the target directory of your chosen cloud provider. It may also be used to simply backup files to your own network or USB drive.  

Multiple configurations can be setup and run if you wanted to backup to multiple cloud/target drives.

Certain file types or directories may be excluded in order to save space on the target directory. For example, in a developer environment, build type folders (such as bin and obj) can be excluded as these may take up unnecessary space and are easily recreated by rebuilding a project/solution.

Supported configuration files are in YAML format.  
  
You are welcome to use/update this software unter the terms of the **MIT license**.  
<br />
  
## Requirements
Executing the app requires [.NET Core](https://dotnet.microsoft.com/download/dotnet-core) framework v3.1 to be installed.  
  
To build the project yourself you will need the [.NET Core SDK](https://dotnet.microsoft.com/download).  

[Visual Studio](https://visualstudio.microsoft.com) is an IDE with built-in support for C# and comes pre-packaged with the .NET Core SDK. Visual Studio is available for [Windows](https://visualstudio.microsoft.com/vs/) or [Mac](https://visualstudio.microsoft.com/vs/mac/).  

Other options include using a code editor such as [Visual Studio Code](https://code.visualstudio.com) with a C# extension installed.  
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
Help info can be displayed in the console using the **--help**, **-h**, or **-?** argument:  
  
*Examples:*
```sh
$ dotnet backuputil.dll --help
```
```sh
$ dotnet backuputil.dll -h
```
```sh
$ dotnet backuputil.dll -?
```
<br />
  
## Version
Version info can be displayed in the console using the **--version** or **-v** argument:  
  
*Examples:*
```sh
$ dotnet backuputil.dll --version
```
```sh
$ dotnet backuputil.dll -v
```
<br />
  
## Creating a Default Config
A default YAML configuration file can be created using either the **--create** or **-c** argument followed by a config name:  
  
*Examples:*
```sh
$ dotnet backuputil.dll --create config1.yaml
```
```sh
$ dotnet backuputil.dll -c config1.yaml
```
<br />

## Running a Backup
The name of the backup configuration file to be run should be provided as a command line argument after either the **--run** or **-r** argument.  
  
*Examples:*  
```sh
$ dotnet backuputil.dll --run config1.yaml
```
```sh
$ dotnet backuputil.dll -r config1.yaml
```
<br />

## Configuration Settings
The following settings can be configured within the YAML configuration file. Most settings are required to be defined within the configuration file. If any critical setting is missing, or the value is inappropriate, then the settings will not validate and the backup will not be run. (App will exit with an error.)

<br /> 

### ***backup_type***
Determines the type of backup to execute *(see table below)*.  
Settings is required. 


|Types|Description|
|:---:|-----|
|**copy**|Copies the contents of the source directory to the target directory. Any files later deleted from the source directory, will remain in the target diectory.|
|**sync**|Keeps the target directory in-sync with the source directory. Files deleted from the source directory will also be deleted from the target directory.|
|**isolated**|Creates isolated backups within the target directory. I.e. Each time a backup is run, a new/separate backup copy is created.|

*Example config entries:*
```yaml
backup_type: copy
```
```yaml
backup_type: sync
```

<br />

### ***target_dir***
Defines the path of the root target backup directory, where the backup will take place.  
Setting is required: Must have a target directory in order to back-up.

*Example config entries:*
```yaml
target_dir: C:\Backups
```
```yaml
target_dir: /Users/freedom35/Backups
```

<br />

### ***source_dirs***
Determines the list of source directories that will be backed up.  
Setting is required: Must have at least one source directory to back-up.

*Example config entries:*
```yaml
source_dirs:
 - C:\Users\freedom35\Projects
 - C:\Users\freedom35\Documents\Specifications
```
```yaml
source_dirs:
 - /Users/freedom35/Projects
 - /Users/freedom35/Documents/Specifications
```

<br />  

### ***max_isolation_days***
Integer value determining the max number of days to keep existing backups. This setting is only used when ***isolated*** is configured as the backup type.  

Set to zero for no max limit (default).  

*Example config entries:*
```yaml
max_isolation_days: 0
```
```yaml
max_isolation_days: 30
```

<br />

### ***ignore_hidden_files***
Determines whether hidden files and folders are ignored during a backup run.  

Default Value: *true*

*Example config entries:*
```yaml
ignore_hidden_files: true
```
```yaml
ignore_hidden_files: false
```

<br />

### ***excluded_dirs***
Determines the list of directories (or sub-directories) that will be ***excluded*** from the backup.  
These directories will not be copied or synced. This can be useful when saving on target storage space.  

Default Value: *None*

*Example config entries:*
```yaml
excluded_dirs:
 - obj
 - bin
 - _sgbak
 - .vs
```
```yaml
excluded_dirs: []
```

<br />

### ***excluded_types***
Determines the list of file types/extensions that will be ***excluded*** from the backup.  
Files with these extensions will not be copied or synced. This can be useful when saving on target storage space.  

Default Value: *None*

*Example config entries:*
```yaml
excluded_types:
 - dll
 - pdb
 - zip
```
```yaml
excluded_types: []
```