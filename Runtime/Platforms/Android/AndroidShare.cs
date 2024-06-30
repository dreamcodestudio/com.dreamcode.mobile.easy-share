using System;
using UnityEngine;

namespace DreamCode.EasyShare
{
    internal sealed class AndroidShare : IShare
    {
        private static readonly AndroidJavaClass _unityPlayer = new("com.unity3d.player.UnityPlayer");

        private readonly AndroidShareListener? _listener =
            ListenerFactory.Create<AndroidShareListener>();

        private readonly AndroidJavaObject _fragment = new("dreamcode.unity.easyshare.ShareFragment");

        public AndroidShare() => _fragment.Call("Initialize");

        public void SendText(string message, Action<string>? callback)
        {
            if (_listener == null)
                throw new InvalidOperationException(
                    $"{nameof(AndroidShare)}-{nameof(SendText)}-{nameof(_listener)} not created");

            _listener.ShareCompleted = callback;

            using var activity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            _fragment.Call("ShareText", activity, message);
        }

        public void SendBinaryContent(string filePath, string mimeType, string message, Action<string>? callback)
        {
            if (_listener == null)
                throw new InvalidOperationException(
                    $"{nameof(AndroidShare)}-{nameof(SendBinaryContent)}-{nameof(_listener)} not created");

            _listener.ShareCompleted = callback;

            using var activity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            _fragment.Call("ShareBinaryContent", activity, mimeType, message, filePath);
        }
    }
}