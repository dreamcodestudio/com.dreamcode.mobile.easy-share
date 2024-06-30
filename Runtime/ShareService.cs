using System;

namespace DreamCode.EasyShare
{
    public static class ShareService
    {
        private static readonly IShare _share = ShareFactory.Create();

        public static void SendBinaryContent(
            string filePath,
            string mimeType,
            string message,
            Action<string>? sharedByActivity = null) =>
            _share.SendBinaryContent(filePath, mimeType, message, sharedByActivity);

        public static void SendText(string message, Action<string>? sharedByActivity = null) =>
            _share.SendText(message, sharedByActivity);
    }
}