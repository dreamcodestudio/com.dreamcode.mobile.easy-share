using NUnit.Framework;
using UnityEngine;
using System;
using UnityEngine.TestTools;
using System.Collections;
using System.IO;

namespace DreamCode.EasyShare
{
    internal class ShareServiceTest
    {
        private const string TestMessage = "Test message";
        private const string TestImageName = "test_image.png";
        private readonly byte[] _pngSignature = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        [UnityTest]
        public IEnumerator SendText_WithValidMessage_DoesNotThrow()
        {
            // Arrange
            string callbackPackageName = null;
            var callbackInvoked = false;
            Action<string> callback = packageName =>
            {
                callbackPackageName = packageName;
                callbackInvoked = true;
            };

            // Act
            ShareService.SendText(TestMessage, callback);

            while (!callbackInvoked)
            {
                yield return null;
            }

            // Assert
            Assert.That(callbackPackageName, Is.Not.Null, "Callback package name should not be null");
            Assert.That(callbackPackageName, Is.Not.Empty, "Callback package name should not be empty");
        }

        [Test]
        public void SendText_WithNullMessage_ThrowsArgumentNullException()
        {
            // Arrange
            string message = null;
            LogAssert.ignoreFailingMessages = true;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => ShareService.SendText(message));
            Assert.That(ex.ParamName, Is.EqualTo(nameof(message)));
        }

        [UnityTest]
        public IEnumerator SendBinaryContent_WithValidImage_DoesNotThrow()
        {
            // Arrange
            var testImagePath = Path.Combine(Application.persistentDataPath, TestImageName);
            File.WriteAllBytes(testImagePath, _pngSignature);

            try
            {
                string callbackPackageName = null;
                var callbackInvoked = false;
                Action<string> callback = packageName =>
                {
                    callbackPackageName = packageName;
                    callbackInvoked = true;
                };

                // Act
                ShareService.SendBinaryContent(
                    testImagePath,
                    "image/png",
                    TestMessage,
                    callback);
                while (!callbackInvoked)
                {
                    yield return null;
                }

                // Assert
                Assert.That(callbackPackageName, Is.Not.Null, "Callback package name should not be null");
                Assert.That(callbackPackageName, Is.Not.Empty, "Callback package name should not be empty");
            }
            finally
            {
                // Cleanup
                if (File.Exists(testImagePath))
                {
                    File.Delete(testImagePath);
                }
            }
        }

        [Test]
        public void SendBinaryContent_WithNullFilePath_ThrowsArgumentNullException()
        {
            // Arrange
            string filePath = null;
            LogAssert.ignoreFailingMessages = true;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => ShareService.SendBinaryContent(
                filePath,
                "image/png",
                TestMessage));
            Assert.That(ex.ParamName, Is.EqualTo(nameof(filePath)));
        }

        [Test]
        public void SendBinaryContent_WithNonExistentFile_ThrowsFileNotFoundException()
        {
            // Arrange
            var nonExistentPath = Path.Combine(Application.persistentDataPath, "non_existent.png");
            LogAssert.ignoreFailingMessages = true;

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => ShareService.SendBinaryContent(
                nonExistentPath,
                "image/png",
                TestMessage));
        }
    }
}