using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework.Components
{
    public class NullComponent : BaseComponent
    {
        private readonly List<BaseComponent> _children = new List<BaseComponent>();

        public new IGame Game => base.Game;

        public int ChildCount => _children.Count;

        public IEnumerable<BaseComponent> Children => _children;

        /// <summary>
        /// Constructor.
        /// Note: This automatically adds itself to the renderable components
        /// </summary>
        /// <param name="game">Game Instance</param>
        /// <param name="parent">Parent component</param>
        public NullComponent(IGame game, BaseComponent parent = null) : base(game, parent)
        {
            game.Components.Add(this);
        }

        /// <summary>
        /// Finds the child of type T in the collection of Children. Returns null if 
        /// a child of that type cannot be found.
        /// </summary>
        /// <typeparam name="T">The type. Must extend from BaseComponent</typeparam>
        /// <returns>A reference to the child or null</returns>
        public T FindChild<T>() where T : BaseComponent
        {
            return (T)_children.FirstOrDefault(x => x is T);
        }

        /// <summary>
        /// Returns the child at the given index or null.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>The child at the given index or null of the index is out of range</returns>
        public BaseComponent GetChildAt(int index)
        {
            return _children.ElementAtOrDefault(index);
        }

        /// <summary>
        /// Add a child to the parent.
        /// </summary>
        /// <param name="component">The component child to add</param>
        public void AddChild(BaseComponent component)
        {
            if (_children.Contains(component)) return; 
            if (component.Parent != null && component.Parent is NullComponent)
            {
                ((NullComponent)component.Parent).RemoveChild(component);
            }

            _children.Add(component);
            if (component is not NullComponent && !Game.Components.Contains(component))
                Game.Components.Add(component);

            OnChildAdded(component);
        }

        /// <summary>
        /// Add a child of type T to the component.
        /// </summary>
        /// <typeparam name="T">The child T. Must extend from BaseComponent</typeparam>
        /// <param name="parameters">The constructor parameters for the child component. Do not include Game or Parent parameters</param>
        /// <returns>The created instance of the child component</returns>
        public T AddChild<T>(params object[] parameters) where T: BaseComponent
        {
            var paramLength = parameters.Length + 2; // 2 for game and this
            var array = new object[paramLength];
            Array.Copy(new object[] { Game, this }, 0, array, 0, 2);
            if (parameters.Length > 0)
            {
                Array.Copy(parameters, 0, array, 2, parameters.Length);
            }

            var child = (T)Activator.CreateInstance(typeof(T), array);
            _children.Add(child);

            if (child is not NullComponent)
                Game.Components.Add(child);
            OnChildAdded(child);
            return child;
        }

        /// <summary>
        /// Remove the child from the list. Also removes it from the running game components.
        /// </summary>
        /// <param name="component">The component</param>
        public void RemoveChild(BaseComponent component)
        {
            _children.Remove(component);
            Game.Components.Remove(component);
            component.Dispose();
        }

        public void RemoveChildAt(int index)
        {
            if (index < 0 || index >= _children.Count) return;
            var child = _children[index];
            RemoveChild(child);
        }

        public void RemoveAll()
        {
            while (_children.Count > 0)
            {
                RemoveChild(_children[0]);
            }
        }

        protected override void Dispose(bool disposing)
        {
            RemoveAll();
            Game.Components.Remove(this);
            base.Dispose(disposing);
        }

        protected virtual void OnChildAdded(BaseComponent component)
        {

        }
    }
}
