# 🎯 What is EasyShare package?
Lets users share content quickly and easily using their favorite apps.

# 💻 Usage
```csharp
using DreamCode.EasyShare;

ShareService.SendText(
    message: "Content with text",
    sharedByActivity: packageName => { Debug.Log("SendBinaryContent callback:" + packageName); });

//
ScreenCapture.CaptureScreenshot("Test.png");
var imagePath = Path.Combine(Application.persistentDataPath, "Test.png");

ShareService.SendBinaryContent(
                filePath: imagePath,
                mimeType: MimeTypeNames.Image,
                message: "Content with image",
                sharedByActivity: packageName => { Debug.Log($"SendBinaryContent:{packageName}"); });

```

https://github.com/user-attachments/assets/873a488f-ebed-4362-aa14-2d3794880172
