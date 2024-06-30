using System;
using System.Runtime.InteropServices;

namespace DreamCode.EasyShare
{
    internal sealed class iOSShare : IShare
    {
        private readonly iOSShareListener? _listener =
            ListenerFactory.Create<iOSShareListener>();

        [DllImport("__Internal")]
        private static extern void _SS_SendBinaryContent(string message, string filePath);

        public void SendText(string message, Action<string>? callback)
        {
            throw new NotImplementedException();
        }

        public void SendBinaryContent(string filePath, string mimeType, string message, Action<string>? callback)
        {
            if (_listener == null)
                throw new InvalidOperationException("TODO");

            _listener.ShareCompleted = callback;
            _SS_SendBinaryContent(message, filePath);
        }
    }
}