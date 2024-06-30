using System;
using UnityEngine;

namespace DreamCode.EasyShare
{
    internal sealed class EditorShare : IShare
    {
        private const string PackageName = "editor.package.name";

        public void SendText(string message, Action<string>? callback)
        {
            Debug.Log($"{nameof(EditorShare)}-{nameof(SendText)}");
            callback?.Invoke(PackageName);
        }

        public void SendBinaryContent(string filePath, string mimeType, string message, Action<string>? callback)
        {
            Debug.Log($"{nameof(EditorShare)}-{nameof(SendBinaryContent)}");
            callback?.Invoke(PackageName);
        }
    }
}