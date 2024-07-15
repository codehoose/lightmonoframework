using GameFramework.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Services.GUI
{
    public class GUIEventsService : BaseGameService
    {
        private GUIEventsServiceComponent _component;

        public GUIEventsService(IGame game) : base(game)
        {
            _component = new GUIEventsServiceComponent(game);
            game.Components.Add(_component);
        }
    }
}
