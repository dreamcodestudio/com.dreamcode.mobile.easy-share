using System;

namespace DreamCode.EasyShare
{
    internal interface IShare
    {
        /// <summary>
        /// Shares binary content (files) with other applications.
        /// </summary>
        /// <param name="filePath">Path to the file to share</param>
        /// <param name="mimeType">MIME type of the file</param>
        /// <param name="message">Optional message to include with the share</param>
        /// <param name="sharedByActivity">Optional callback when sharing is completed</param>
        /// <exception cref="ArgumentNullException">Thrown when filePath is null or empty</exception>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the file doesn't exist</exception>
        void SendBinaryContent(
            string filePath,
            string mimeType,
            string message,
            Action<string>? sharedByActivity = null);

        /// <summary>
        /// Shares text content with other applications.
        /// </summary>
        /// <param name="message">Text to share</param>
        /// <param name="sharedByActivity">Optional callback when sharing is completed</param>
        /// <exception cref="ArgumentNullException">Thrown when message is null or empty</exception>
        void SendText(
            string message,
            Action<string>? sharedByActivity = null);
    }
}