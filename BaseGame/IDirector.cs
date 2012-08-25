using BaseGame.Screens;
using Microsoft.Xna.Framework;

namespace BaseGame
{
    public  interface IDirector
    {
        BaseScreen CurrentScreen { get; set; }
        void Initialise(GraphicsDeviceManager graphics);
        void Update(GameTime time);
        void Draw(GameTime time);
        void LoadContent();
    }
}