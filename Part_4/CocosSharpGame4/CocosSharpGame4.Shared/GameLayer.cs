using System.Collections.Generic;
using CocosSharp;

namespace CocosSharpGame4.Shared
{
    public class GameLayer : CCLayer
    {

        // Define a label variable
        CCLabel label;
        CCLabel gameOver;
        CCSprite paddle;
        CCSprite ballSprite;

        float ballXVelocity;
        float ballYVelocity = 300;
        const float gravity = 140;
        int score;
        int lives = 3;

        public GameLayer()
        {
            // create and initialize a Label
            label = new CCLabel(string.Format("Lives: {0} Score: {1}", lives, score), "fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            AddChild(label);
            gameOver = new CCLabel("Game over!", "fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            gameOver.Visible = false;
            AddChild(gameOver);

            // paddle
            paddle = new CCSprite("paddle");
            paddle.PositionX = 100;
            paddle.PositionY = 100;
            AddChild(paddle);

            // ball
            ballSprite = new CCSprite("ball");
            ballSprite.PositionX = 320;
            ballSprite.PositionY = 800;
            AddChild(ballSprite);

            Schedule(RunGameLogic);
        }

        void RunGameLogic(float frameTimeInSeconds)
        {
            if (lives == 0)
            {
                return;
            }

            // Move ball
            //ballYVelocity += frameTimeInSeconds * -gravity;
            ballSprite.PositionX += ballXVelocity * frameTimeInSeconds;
            ballSprite.PositionY += ballYVelocity * frameTimeInSeconds;

            // Check for ball falling off the bottom
            if (ballSprite.PositionY < 0)
            {
                lives--;
                label.Text = string.Format("Lives: {0} Score: {1}", lives, score);
                if (lives == 0)
                {
                    // Game over
                    gameOver.Visible = true;
                    return;
                }
                else
                {
                    ballSprite.PositionX = 320;
                    ballSprite.PositionY = 800;
                }
            }

            // Check for paddle hit
            bool doesBallOverlapPaddle = ballSprite.BoundingBoxTransformedToParent.IntersectsRect(paddle.BoundingBoxTransformedToParent);
            bool isMovingDownward = ballYVelocity < 0;
            if (doesBallOverlapPaddle && isMovingDownward)
            {
                ballYVelocity *= -1;
                const float minXVelocity = -300;
                const float maxXVelocity = 300;
                ballXVelocity = CCRandom.GetRandomFloat(minXVelocity, maxXVelocity);

                score++;
                label.Text = string.Format("Lives: {0} Score: {1}", lives, score);
            }

            // Check left and right bounds
            float ballRight = ballSprite.BoundingBoxTransformedToParent.MaxX;
            float ballLeft = ballSprite.BoundingBoxTransformedToParent.MinX;
            float screenRight = VisibleBoundsWorldspace.MaxX;
            float screenLeft = VisibleBoundsWorldspace.MinX;

            bool shouldReflectXVelocity =
                (ballRight > screenRight && ballXVelocity > 0) ||
                (ballLeft < screenLeft && ballXVelocity < 0);

            if (shouldReflectXVelocity)
            {
                ballXVelocity *= -1;
            }

            // Check for top bounds (bottom handled above)
            float ballTop = ballSprite.BoundingBoxTransformedToParent.MaxY;
            float screenTop = VisibleBoundsWorldspace.MaxY;

            if ( ballTop > screenTop && ballYVelocity > 0 )
            {
                ballYVelocity *= -1;
            }

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;

            // position the label on the center of the screen
            label.PositionX = label.ScaledContentSize.Width / 2;
            label.PositionY = label.ScaledContentSize.Height / 2;
            gameOver.Position = bounds.Center;

            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            touchListener.OnTouchesMoved = OnTouchesEnded;
            AddEventListener(touchListener, this);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                var locationOnScreen = touches[0].Location;
                paddle.PositionX = locationOnScreen.X;
            }
        }
    }
}

