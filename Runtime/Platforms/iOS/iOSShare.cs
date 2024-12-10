using System;
#if UNITY_IOS 
using System.Runtime.InteropServices;
#endif

namespace DreamCode.EasyShare
{
    internal sealed class iOSShare : IShare
    {
        private readonly iOSShareListener? _listener =
            ListenerFactory.Create<iOSShareListener>();
#if UNITY_IOS 
        [DllImport("__Internal")]
        private static extern void _ES_SendText(string message);
        [DllImport("__Internal")]
        private static extern void _ES_SendBinaryContent(string message, string filePath);
#endif

        public void SendText(string message, Action<string>? callback)
        {
            if (_listener == null)
                throw new InvalidOperationException(
                    $"{nameof(iOSShare)}-{nameof(SendText)}-{nameof(_listener)} not created");

            _listener.ShareCompleted = callback;
#if UNITY_IOS 
            _ES_SendText(message);
#endif
        }

        public void SendBinaryContent(string filePath, string mimeType, string message, Action<string>? callback)
        {
            if (_listener == null)
                throw new InvalidOperationException(
                    $"{nameof(iOSShare)}-{nameof(SendBinaryContent)}-{nameof(_listener)} not created");

            _listener.ShareCompleted = callback;
#if UNITY_IOS 
            _ES_SendBinaryContent(message, filePath);
#endif
        }
    }
}