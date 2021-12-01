using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class Bunker : SimpleObject
    {
		
        public Bunker(Vecteur2D pos, int lives, Bitmap img ):base(Side.Neutral)
        {
            Position = pos;
            Lives = lives;
            Image = img;
			this.side = Side.Neutral;
			
            
        }
       
        public override void Update(Game gameInstance, double deltaT)
        {
            
			

        }
        public override bool Collision(Missile m)
        {
			int nbCol = 0; ;
			if (this.IsAlive() && m.IsAlive())
			{
				double obj_posX = this.Position.x;
				double obj_posY = this.Position.y;

				double ojb_img_W = this.Image.Width;
				double ojb_img_H = this.Image.Height;

				double m_posX = m.Position.x;
				double m_posY = m.Position.y;

				double m_img_W = m.Image.Width;
				double m_img_H = m.Image.Width;


				if (!((m_posX > obj_posX + ojb_img_W) || (m_posY > ojb_img_H + obj_posY) || (m_posX + m_img_W < obj_posX) || (obj_posY > m_posY + m_img_H))) { 

					if (m.Position.x >= Position.x && m.Position.x <= Position.x + Image.Width)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void CollisionParPixel(Missile m)
		{
			if (base.Collision(m))
			{
				int x = 0, y = 0;
		
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
		public void CollisionParPixelPlayer(Missile m)
		{
			if (this.Collision(m))
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

								
								m.Lives = 0;


								
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
