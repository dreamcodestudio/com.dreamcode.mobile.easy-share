using System;
using UnityEngine;

namespace DreamCode.EasyShare
{
    /// <summary>
    /// Base abstract class for platform-specific share implementations.
    /// Provides common functionality and error handling for all share implementations.
    /// </summary>
    public abstract class BaseShare : IShare
    {
        /// <summary>
        /// Platform-specific implementation for sharing binary content.
        /// </summary>
        protected abstract void PlatformSendBinaryContent(string filePath, string mimeType, string message,
            Action<string>? sharedByActivity);

        /// <summary>
        /// Platform-specific implementation for sharing text content.
        /// </summary>
        protected abstract void PlatformSendText(string message, Action<string>? sharedByActivity);

        /// <inheritdoc/>
        public void SendBinaryContent(string filePath, string mimeType, string message,
            Action<string>? sharedByActivity = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentNullException(nameof(filePath), ShareConstants.ErrorMessages.InvalidFilePath);
                }

                if (!System.IO.File.Exists(filePath))
                {
                    var errorMessage = string.Format(ShareConstants.ErrorMessages.FileNotFound, filePath);
                    throw new System.IO.FileNotFoundException(errorMessage);
                }

                PlatformSendBinaryContent(filePath, mimeType, message, sharedByActivity);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(
                    ShareConstants.ErrorMessages.FailedToShare,
                    "binary content",
                    ex.Message);
                Debug.LogError($"{ShareConstants.LogPrefix} {errorMessage}");
                throw;
            }
        }

        /// <inheritdoc/>
        public void SendText(string message, Action<string>? sharedByActivity = null)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(nameof(message), ShareConstants.ErrorMessages.InvalidMessage);
                }

                PlatformSendText(message, sharedByActivity);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(
                    ShareConstants.ErrorMessages.FailedToShare,
                    "text",
                    ex.Message);
                Debug.LogError($"{ShareConstants.LogPrefix} {errorMessage}");
                throw;
            }
        }
    }
}