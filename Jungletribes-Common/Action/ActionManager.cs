using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jungletribes_Common
{
    public static class ActionManager
    {
        private static readonly List<GameAction[]> actions = new List<GameAction[]>();
        public static void addAction(GameAction[] actionsTab)
        {
            actions.Add(actionsTab);
        }
        public static GameAction[] getRandAction()
        {
            return actions.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}