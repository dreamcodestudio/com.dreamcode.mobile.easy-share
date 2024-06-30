using System;

namespace DreamCode.EasyShare
{
    internal interface IShare
    {
        void SendText(string message, Action<string>? callback);
        void SendBinaryContent(string filePath, string mimeType, string message, Action<string>? callback);
    }
}