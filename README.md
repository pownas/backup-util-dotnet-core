# backup-util-dotnet-core

Backup utility to copy directories from multiple source locations to a common target directory.

An example usage would be to configure the target directory within a cloud directory (such as Dropbox/OneDrive etc.) in order to selectively backup files to your cloud provider of choice.
(Multiple configurations can be used/run if you wanted to backup to multiple cloud drives.)

Certain file types or directories may be excluded in order to save space on the target directory. For example, in a developer environment, build type folders (such as bin and obj) can be excluded as these are easily recreated by rebuilding a project/solution.

Configuration files are YAML in format.

## Usage
The name of the configuration file should be passed as a command line argument.
i.e. BackupUtilityCore [config name]

Example:
**BackupUtilityCore config1.yaml**

### Default Config
A default configuration file can be created using the following command.
**BackupUtilityCore -c**
