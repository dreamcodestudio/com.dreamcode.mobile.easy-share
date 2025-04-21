using System;
using UnityEngine;

namespace DreamCode.EasyShare
{
    /// <summary>
    /// Static service for sharing content across different platforms.
    /// Provides a unified interface for sharing text and binary content.
    /// </summary>
    public static class ShareService
    {
        private static readonly IShare _share = ShareFactory.Create();

        /// <summary>
        /// Shares binary content (files) with other applications.
        /// </summary>
        /// <param name="filePath">Path to the file to share</param>
        /// <param name="mimeType">MIME type of the file</param>
        /// <param name="message">Optional message to include with the share</param>
        /// <param name="sharedByActivity">Optional callback when sharing is completed</param>
        /// <exception cref="ArgumentNullException">Thrown when filePath is null or empty</exception>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the file doesn't exist</exception>
        public static void SendBinaryContent(
            string filePath,
            string mimeType,
            string message,
            Action<string>? sharedByActivity = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentNullException(nameof(filePath), "File path cannot be null or empty");
                }

                if (!System.IO.File.Exists(filePath))
                {
                    throw new System.IO.FileNotFoundException($"File not found: {filePath}");
                }

                _share.SendBinaryContent(filePath, mimeType, message, sharedByActivity);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to share binary content: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Shares text content with other applications.
        /// </summary>
        /// <param name="message">Text to share</param>
        /// <param name="sharedByActivity">Optional callback when sharing is completed</param>
        /// <exception cref="ArgumentNullException">Thrown when message is null or empty</exception>
        public static void SendText(string message, Action<string>? sharedByActivity = null)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(nameof(message), "Message cannot be null or empty");
                }

                _share.SendText(message, sharedByActivity);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to share text: {ex.Message}");
                throw;
            }
        }
    }
}