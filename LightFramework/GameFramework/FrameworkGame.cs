using GameFramework.Audio;
using GameFramework.Bubbling.Events;
using GameFramework.Composition;
using GameFramework.FSM;
using GameFramework.Input;
using GameFramework.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework
{
    public class FrameworkGame : Game, IGame
    {
        private const int SCREEN_WIDTH = 1280;
        private const int SCREEN_HEIGHT = 720;
        private readonly int _screenHeight;
        private GraphicsDeviceManager _graphics;
        private IMouseState _mouseState;
        private readonly int _screenWidth;
        private float _scale = 1f;

        public SpriteBatch SpriteBatch { get; private set; }

        public IStateMachine StateMachine { get; private set; }

        public IStateProvider StateProvider { get; private set; }

        public IMusicService Audio { get; }

        public Color BackgroundColor { get; set; }

        public IBubbleMessageQueue MessageQueue { get; }

        /// <summary>
        /// Set the scale of the game. Minimum scale is 1/4 (0.25).
        /// </summary>
        public float Scale
        {
            get => _scale;
            set
            {
                _scale = MathF.Max(0.25f, value);
            }
        }

        public MouseInputState GetMouseState()
        {
            return _mouseState.GetState();
        }

        public T GetComponent<T>()
        {
            return (T)Components.First(c => c is T);
        }

        public IList<T> GetComponents<T>()
        {
            return Components.Select(c => c is T ? (T)c : default(T))
                             .Where(c => c != null)
                             .ToList();
        }

        public FrameworkGame(int width = SCREEN_WIDTH, int height = SCREEN_HEIGHT)
        {
            _graphics = new GraphicsDeviceManager(this);
            _mouseState = new FrameworkMouseState(this);
            MessageQueue = new BubbleMessageQueue();
            _screenWidth = width;
            _screenHeight = height;
            Audio = new MusicService(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);
            SpriteBatch.Begin(samplerState: SamplerState.PointWrap, transformMatrix: Matrix.CreateScale(Scale));
            base.Draw(gameTime);
            SpriteBatch.End();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            StateMachine = new StateMachine(this);
            StateProvider = new StateProvider(this);

            // Set up services
            if (!this.HasAttribute<NoServicesAttribute>())
            {
                List<Type> serviceTypes = AssemblyHelper.GetImplementorsOf<IGameService>();
                foreach (Type serviceType in serviceTypes)
                {
                    IGameService service = (IGameService)Activator.CreateInstance(serviceType, this);
                    Services.AddService(service.GetType(), service);
                }
            }
            
            base.LoadContent();
        }
    }
}
