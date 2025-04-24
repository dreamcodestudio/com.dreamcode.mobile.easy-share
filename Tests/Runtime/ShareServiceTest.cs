using NUnit.Framework;
using UnityEngine;
using System;
using UnityEngine.TestTools;
using System.Collections;

namespace DreamCode.EasyShare
{
    internal class ShareServiceTest
    {
        private const string TestMessage = "Test message";

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
            const string expectedError =
                "Failed to share text: Message cannot be null or empty\r\nParameter name: message";
            LogAssert.Expect(LogType.Error, expectedError);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => ShareService.SendText(message));
            Assert.That(ex.ParamName, Is.EqualTo(nameof(message)));
        }
    }
}