﻿using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace BackupUtilityCore
{
    /// <summary>
    /// Enum definition for backup result.
    /// </summary>
    public enum BackupResult
    {
        [Description("OK")]
        OK,
        
        [Description("Ineligible due to config")]
        Ineligible,
        
        [Description("Already backed-up")]
        AlreadyBackedUp,
        
        [Description("Unable to access file")]
        Error,
        
        [Description("File busy, write in progress")]
        WriteInProgress,

        [Description("Target path is too long")]
        PathTooLong
    }

    /// <summary>
    /// Class to extend BackupResult enum.
    /// </summary>
    public static class BackupResultEnumExtension
    {
        /// <summary>
        /// Determines whether the result is suitable for retry.
        /// </summary>
        /// <param name="result">BackupResult value</param>
        /// <returns>true if retry possible</returns>
        public static bool CanBeRetried(this BackupResult result)
        {
            return result == BackupResult.WriteInProgress 
                || result == BackupResult.Error;
        }

        /// <summary>
        /// Gets the description associated with the enum value.
        /// </summary>
        /// <param name="result">BackupResult value</param>
        /// <returns>enum description as string</returns>
        public static string GetDescription(this BackupResult result)
        {
            // Query enum for info
            FieldInfo fi = result.GetType().GetField(result.ToString());

            if (fi != null)
            {
                // Get first description attribute
                DescriptionAttribute attr = ((DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false)).FirstOrDefault();

                // Get description value (default to enum string if missing)
                return attr?.Description ?? result.ToString();
            }

            return result.ToString();
        }
    }
}
