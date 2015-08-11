#include "main.h"
#include "AppDelegate.h"
#include "cocos2d.h"
#include <shellapi.h>
//#include "vld.h"
USING_NS_CC;

#if _MSC_VER > 1700
#pragma comment(lib,"libcocos2d_2013.lib")
#pragma comment(lib,"libluacocos2d_2013.lib")
#pragma comment(lib,"libbox2d_2013.lib")
#pragma comment(lib,"libSpine_2013.lib")
#pragma comment(lib,"libsimulator_2013.lib")
#else
#pragma comment(lib,"libcocos2d_2012.lib")
#pragma comment(lib,"libluacocos2d_2012.lib")
#pragma comment(lib,"libbox2d_2012.lib")
#pragma comment(lib,"libSpine_2012.lib")
#pragma comment(lib,"libsimulator_2012.lib")
#endif
int APIENTRY _tWinMain(HINSTANCE hInstance,
                       HINSTANCE hPrevInstance,
                       LPTSTR    lpCmdLine,
                       int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // create the application instance
    AppDelegate app;
    return Application::getInstance()->run();
}
