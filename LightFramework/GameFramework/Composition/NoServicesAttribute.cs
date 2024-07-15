using System;

namespace GameFramework.Composition
{
    /// <summary>
    /// Add this to your game class if you don't have services. Otherwise by default
    /// the game class will try and add services that implement IGameService to the Game.Services property
    /// </summary>
    public class NoServicesAttribute : Attribute
    {
    }
}
