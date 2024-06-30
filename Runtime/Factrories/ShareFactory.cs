namespace DreamCode.EasyShare
{
    internal static class ShareFactory
    {
        internal static IShare Create()
        {
#if UNITY_EDITOR
            return new EditorShare();
#elif UNITY_ANDROID
            return new AndroidShare();
#elif UNITY_IOS
            return new iOSShare();
#endif
            throw new System.NotImplementedException($"{nameof(ShareFactory)}");
        }
    }
}