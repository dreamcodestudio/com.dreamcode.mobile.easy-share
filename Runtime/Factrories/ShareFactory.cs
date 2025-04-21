using System;
using UnityEngine;

namespace DreamCode.EasyShare
{
    internal static class ShareFactory
    {
        /// <summary>
        /// Creates a platform-specific share implementation based on the current runtime platform.
        /// </summary>
        /// <returns>An instance of IShare implementation for the current platform</returns>
        /// <exception cref="NotImplementedException">Thrown when the current platform is not supported</exception>
        internal static IShare Create()
        {
            try
            {
#if UNITY_EDITOR
                return new EditorShare();
#elif UNITY_ANDROID
                return new AndroidShare();
#elif UNITY_IOS
                return new iOSShare();
#else
                var errorMessage =
                string.Format(ShareConstants.ErrorMessages.PlatformNotSupported, Application.platform);
                throw new NotImplementedException(errorMessage);
#endif
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to create share implementation: {ex.Message}");
                throw;
            }
        }
    }
}