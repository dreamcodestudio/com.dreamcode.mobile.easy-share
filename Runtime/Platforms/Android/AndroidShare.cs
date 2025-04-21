using System;
using UnityEngine;

namespace DreamCode.EasyShare
{
    /// <summary>
    /// Android-specific implementation of the share functionality.
    /// Provides native Android sharing capabilities using Android Java classes.
    /// </summary>
    internal sealed class AndroidShare : BaseShare
    {
        private static readonly AndroidJavaClass _unityPlayer = new("com.unity3d.player.UnityPlayer");
        private readonly AndroidShareListener? _listener = ListenerFactory.Create<AndroidShareListener>();
        private readonly AndroidJavaObject _fragment = new("dreamcode.unity.easyshare.ShareFragment");

        public AndroidShare()
        {
            try
            {
                _fragment.Call("Initialize");
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(ShareConstants.ErrorMessages.FailedToInitialize, ex.Message);
                Debug.LogError($"{ShareConstants.LogPrefix} {errorMessage}");
                throw new InvalidOperationException(errorMessage, ex);
            }
        }

        protected override void PlatformSendText(string message, Action<string>? sharedByActivity)
        {
            try
            {
                if (_listener == null)
                {
                    throw new InvalidOperationException(ShareConstants.ErrorMessages.ListenerNotCreated);
                }

                _listener.ShareCompleted = sharedByActivity;

                using var activity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                _fragment.Call("ShareText", activity, message);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(ShareConstants.ErrorMessages.FailedToShare, "text", ex.Message);
                Debug.LogError($"{ShareConstants.LogPrefix} {errorMessage}");
                throw new InvalidOperationException(errorMessage, ex);
            }
        }

        protected override void PlatformSendBinaryContent(string filePath, string mimeType, string message,
            Action<string>? sharedByActivity)
        {
            try
            {
                if (_listener == null)
                {
                    throw new InvalidOperationException(ShareConstants.ErrorMessages.ListenerNotCreated);
                }

                _listener.ShareCompleted = sharedByActivity;

                using var activity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                _fragment.Call("ShareBinaryContent", activity, mimeType, message, filePath);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(ShareConstants.ErrorMessages.FailedToShare, "binary content",
                    ex.Message);
                throw new InvalidOperationException(errorMessage, ex);
            }
        }
    }
}