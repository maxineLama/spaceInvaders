using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class Bunker : SimpleObject
    {
		
        public Bunker(Vecteur2D pos, int lives, Bitmap img ):base(Side.Ally)
        {
            Position = pos;
            Lives = lives;
            Image = img;
			this.side = Side.Ally;
			
            
        }
       
        public override void Update(Game gameInstance, double deltaT)
        {
            
			

        }
        public override bool Collision(Missile m)
        {
			

			return base.Collision(m);
		}

		public void CollisionParPixel(Missile m)
		{
			if (Collision(m))
			{
				int x = 0, y = 0;
				int etat = 0;
				for (int i = 0; i < m.Image.Width; i++)
				{
					for (int j = 0; j < m.Image.Height; j++)
					{

						
						x = i + (int)m.Position.x - (int)Position.x;
						y = j + (int)m.Position.y - (int)Position.y;
						if (x >= 0 && x < Image.Width && y >= 0 && y < Image.Height)
						{
							
							Color pixel = Image.GetPixel(x, y);
							// si pixel noir tu le met blanc et tu tues le missile
							if (pixel.A == 255 && pixel.R == 0 && pixel.G == 0 && pixel.B == 0)
							{
								
								Image.SetPixel(x, y, Color.White);
								m.Lives = 0;
								Lives--;
							}

						}

					}

				}

			}	

		}
		protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
			Lives -= numberOfPixelsInCollision;
        }




	}
}
