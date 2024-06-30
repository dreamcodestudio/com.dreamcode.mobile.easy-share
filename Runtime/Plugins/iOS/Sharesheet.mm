#import "UnityFramework/UnityFramework-Swift.h"

extern "C" {

void _SS_SendBinaryContent(char* cMessage, char* cFilePath) {
    NSString *message = [[NSString alloc] initWithUTF8String:cMessage];
    NSString *filePath = [[NSString alloc] initWithUTF8String:cFilePath];
    
    UIViewController *viewController = UnityGetGLViewController();
    [[Sharesheet shared] SendBinaryContentWithMessage:message filePath:filePath viewController:viewController];
}
}
