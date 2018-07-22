using CocosSharp;
using CocosSharpGame4.Shared;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocosSharpGame4.Shared
{
    public class GameScene : CCScene
    {
        public GameScene(CCWindow window) : base(window)
        {
            var backgroundLayer = new CCLayer();
            CreateBackground(window, backgroundLayer);
            AddChild(backgroundLayer);

            var introLayer = new GameLayer();
            AddChild(introLayer);
        }

        private void CreateBackground(CCWindow window, CCLayer backgroundLayer)
        {
            var texture = new CCTexture2D("tilesmall");
            texture.SamplerState = SamplerState.LinearWrap;
            var background = new CCSprite(texture);
            background.ContentSize = new CCSize(window.WindowSizeInPixels.Width, window.WindowSizeInPixels.Height);
            background.TextureRectInPixels = new CCRect(0, 0, window.WindowSizeInPixels.Width, window.WindowSizeInPixels.Height);
            background.AnchorPoint = new CCPoint(0, 0);
            backgroundLayer.AddChild(background);

            //var background = new CCSprite("tilebig_p.png");
            //background.AnchorPoint = new CCPoint(0, 0);
            //background.IsAntialiased = false;
            //backgroundLayer.AddChild(background);
        }
    }
}
