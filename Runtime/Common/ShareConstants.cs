namespace DreamCode.EasyShare
{
    /// <summary>
    /// Contains constants used across the EasyShare package.
    /// </summary>
    internal static class ShareConstants
    {
        /// <summary>
        /// Logging prefix used for all share-related messages.
        /// </summary>
        public const string LogPrefix = "[EasyShare]";

        /// <summary>
        /// Package name used for editor sharing.
        /// </summary>
        public const string EditorPackageName = "editor.package.name";

        /// <summary>
        /// Error messages used across the package.
        /// </summary>
        public static class ErrorMessages
        {
            public const string ListenerNotCreated = "Share listener not created";
            public const string FileNotFound = "File not found: {0}";
            public const string InvalidFilePath = "File path cannot be null or empty";
            public const string InvalidMessage = "Message cannot be null or empty";
            public const string PlatformNotSupported = "Platform not supported: {0}";
            public const string FailedToInitialize = "Failed to initialize: {0}";
            public const string FailedToShare = "Failed to share {0}: {1}";
        }
    }
}