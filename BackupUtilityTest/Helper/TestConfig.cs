﻿using BackupUtilityCore;
using System;
using System.IO;

namespace BackupUtilityTest.Helper
{
    /// <summary>
    /// Class to help with config resource for testing.
    /// </summary>
    internal static class TestConfig
    {
        /// <summary>
        /// Internal resource path
        /// </summary>
        public const string ResourcePath = "BackupUtilityTest.Resources.test-config.yaml";

        /// <summary>
        /// Index to ensure config names unique.
        /// </summary>
        private static int testIndex = 0;

        /// <summary>
        /// Creates a config name for test purposes.
        /// </summary>
        /// <returns>Config path</returns>
        public static string CreateNewOutputPath(string testRoot)
        {
            string now = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Thread-safe increment
            int newIndex = System.Threading.Interlocked.Increment(ref testIndex);

            // Output path for testing
            return Path.Combine(testRoot, $"{now}-{newIndex}.yaml");
        }

        /// <summary>
        /// Creates a new config file for testing.
        /// </summary>
        /// <returns>Path to new file</returns>
        public static string CreateNewConfig(string testRoot)
        {
            string targetPath = TestConfig.CreateNewOutputPath(testRoot);

            // Create file using embedded resource
            EmbeddedResource.CreateCopyFromPath(TestConfig.ResourcePath, targetPath);

            return targetPath;
        }
    }
}
