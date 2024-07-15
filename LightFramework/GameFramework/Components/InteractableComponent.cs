using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Components
{
    public class InteractableComponent : NullComponent
    {
        public InteractableComponent(IGame game, BaseComponent parent = null) : base(game, parent)
        {
        }
    }
}
