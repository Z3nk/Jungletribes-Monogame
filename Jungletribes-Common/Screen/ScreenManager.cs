using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jungletribes_Common
{
    public static class ScreenManager
    {
        public static Dictionary<String, Screen> screenList = new Dictionary<string, Screen>();
        public static Screen currentScreen;
        public static void addScreen(String name, Screen screen)
        {
            screenList.Add(name, screen);
        }
        public static Screen getScreen(String name)
        {
            return screenList[name];
        }

        public static void moveTo(string screenName)
        {
            currentScreen.UnloadContent();
            currentScreen = getScreen(screenName);
            currentScreen.LoadContent();
        }
    }
}