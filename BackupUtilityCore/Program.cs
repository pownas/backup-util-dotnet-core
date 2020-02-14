﻿using BackupUtilityCore.YAML;
using System;
using System.Linq;

namespace BackupUtilityCore
{
    sealed class Program
    {
        /// <summary>
        /// Entry point for program.
        /// </summary>
        static int Main(string[] args)
        {
            // Default to OK
            int returnCode = 0;

            try
            {
                Console.WriteLine("Backup Utility v{0}\n", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

                // Check if help args supplied
                if (args.Any(arg => arg == "-h" || arg.ToLower() == "--help"))
                {
                    HelpInfo.Display();
                }
                else if (TryGetSettingsFile(args, out string settingsFile))
                {
                    // Parse args for backup settings
                    BackupSettings backupSettings = ParseSettings(settingsFile);

                    // Check config parsed ok
                    if (backupSettings.Valid)
                    {
                        // Create backup object
                        BackupTask backup = new BackupTask(backupSettings);

                        // Add handler for output
                        backup.Log += Console.WriteLine;

                        try
                        {
                            // Execute backup
                            int backupCount = backup.Execute();

                            // Report total
                            Console.WriteLine($"Total files backed up: {backupCount}");
                        }
                        finally
                        {
                            // Remove handler
                            backup.Log -= Console.WriteLine;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Config file is not valid, target or source settings are missing.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Report error
                Console.WriteLine("\nError:\n{0}", ex.Message);

                // Check if more details available
                if (ex.StackTrace != null)
                {
                    Console.WriteLine("\nStack Trace:\n{0}", ex.StackTrace);
                }

                // Return error
                returnCode = 1;
            }

            return returnCode;
        }

        static bool TryGetSettingsFile(string[] args, out string settingsFile)
        {
            const string DefaultFile = "backup-config.yaml";

            // Get settings file name.
            settingsFile = args.ElementAtOrDefault(0) ?? DefaultFile;

            // Check whether full path or just file supplied.
            if (!System.IO.Path.IsPathRooted(settingsFile))
            {
                // Add current directory to path.
                settingsFile = System.IO.Path.Combine(Environment.CurrentDirectory, settingsFile);
            }

            // Check file exists.
            if (System.IO.File.Exists(settingsFile))
            {
                // File should be used.
                return true;
            }

            // Check for flag to create missing file.
            if (args.Contains("-c"))
            {
                // Create file using defaults.
                EmbeddedResource.CreateLocalCopy(DefaultFile);

                // Report that file created.
                Console.WriteLine($"Config file created: {DefaultFile}");
            }
            else
            {
                Console.WriteLine($"Config file does not exist: {settingsFile}");
            }

            // Return false if not specified or newly created.
            // (May be dangerous to use default settings)
            return false;
        }

        static BackupSettings ParseSettings(string settingsFile)
        {
            // Type from settings?
            ISettingsParser backupSettings = new YamlSettingsParser();

            return backupSettings.Parse(settingsFile);
        }
    }
}
