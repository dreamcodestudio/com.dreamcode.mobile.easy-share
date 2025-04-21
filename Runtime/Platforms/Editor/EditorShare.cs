using System;
using UnityEngine;

namespace DreamCode.EasyShare
{
    /// <summary>
    /// Editor-specific implementation of the share functionality.
    /// Provides mock sharing capabilities for testing in the Unity Editor.
    /// </summary>
    internal sealed class EditorShare : BaseShare
    {
        protected override void PlatformSendText(string message, Action<string>? sharedByActivity)
        {
            Debug.Log($"{ShareConstants.LogPrefix} [Editor] Sharing text: {message}");
            sharedByActivity?.Invoke(ShareConstants.EditorPackageName);
        }

        protected override void PlatformSendBinaryContent(
            string filePath,
            string mimeType,
            string message,
            Action<string>? sharedByActivity)
        {
            Debug.Log(
                $"{ShareConstants.LogPrefix} [Editor] Sharing file: {filePath} (MIME: {mimeType}) with message: {message}");
            sharedByActivity?.Invoke(ShareConstants.EditorPackageName);
        }
    }
}