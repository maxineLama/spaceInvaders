using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace SpaceInvaders
{
    class PlayerSpaceship : SpaceShip
    {
        public PlayerSpaceship(Vecteur2D pos, int lives, Bitmap img): base(pos, lives, img)
        {
            
        }

        public override void Update(Game gameInstance, double deltaT)
        {

            if (gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Right))
            {
                if (gameInstance.playerSpaceShip.Position.x <= gameInstance.gameSize.Width - gameInstance.playerSpaceShip.Image.Width)
                {
                    gameInstance.playerSpaceShip.Position.x += speedPixelPerSecond * deltaT;
                }

            }
            else if (gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Left))
            {
                if (gameInstance.playerSpaceShip.Position.x - 1 > 0)
                {
                    gameInstance.playerSpaceShip.Position.x -= speedPixelPerSecond * deltaT;
                }
            }
            else if (gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Space))
            {

                shoot(gameInstance, deltaT);



            }

        }
    }
}
