using System;
using UnityEngine;
#if UNITY_IOS 
using System.Runtime.InteropServices;
#endif

namespace DreamCode.EasyShare
{
    /// <summary>
    /// iOS-specific implementation of the share functionality.
    /// Provides native iOS sharing capabilities using native plugins.
    /// </summary>
    internal sealed class iOSShare : BaseShare
    {
        private readonly iOSShareListener? _listener = ListenerFactory.Create<iOSShareListener>();

#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _ES_SendText(string message);

        [DllImport("__Internal")]
        private static extern void _ES_SendBinaryContent(string message, string filePath);
#endif

        protected override void PlatformSendText(string message, Action<string>? sharedByActivity)
        {
            try
            {
                if (_listener == null)
                {
                    throw new InvalidOperationException(ShareConstants.ErrorMessages.ListenerNotCreated);
                }

                _listener.ShareCompleted = sharedByActivity;

#if UNITY_IOS
                _ES_SendText(message);
#endif
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

#if UNITY_IOS
                _ES_SendBinaryContent(message, filePath);
#endif
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(ShareConstants.ErrorMessages.FailedToShare, "binary content",
                    ex.Message);
                Debug.LogError($"{ShareConstants.LogPrefix} {errorMessage}");
                throw new InvalidOperationException(errorMessage, ex);
            }
        }
    }
}