using System;
using System.Drawing;


/// <summary>
/// Summary description for Class1
/// </summary>
namespace SpaceInvaders
{
    abstract class SimpleObject : GameObject
    {
        public SimpleObject(Side s) : base(s)
        {
            
        }
        public Vecteur2D Position
        {
            get;
            set;
        }
        public int Lives
        {
            get;
            set;

        }

        public Bitmap Image
        {
            get;
            set;
        }

        public override bool Collision(Missile m)
        {
           
            int nbCol = 0; ;
            if (this.IsAlive() && m.IsAlive() && this != m )
            {
                double obj_posX = this.Position.x;
                double obj_posY = this.Position.y;

                double ojb_img_W = this.Image.Width;
                double ojb_img_H = this.Image.Height;

                double m_posX = m.Position.x;
                double m_posY = m.Position.y;

                double m_img_W = m.Image.Width;
                double m_img_H = m.Image.Width;


                if (!((m_posX > obj_posX + ojb_img_W) || (m_posY > ojb_img_H + obj_posY) || (m_posX + m_img_W < obj_posX) || (obj_posY > m_posY + m_img_H)))

                    if (m.Position.x >= Position.x && m.Position.x <= Position.x + Image.Width)
                    {
                        nbCol++;
                    }

            }
           
           
            if (nbCol > 0 )
            {
                if (this.side == (m.side))
                {
                    Console.WriteLine("missile " + m.side + " obj " + this.side);
                    m.OnCollision(m, nbCol);
                    if (m.side == Side.Ally)
                    {
                        return true;
                    }

                    return false;
                }
                OnCollision(m, nbCol);
                Console.WriteLine("missile " + m.side + " obj " + this.side);
                return true;
            }

            return false;
        }


        public override void Draw(Game gameInstance, Graphics graphics)
        {

            float PositionX = (float)Position.x;
            float PositionY = (float)Position.y;

            graphics.DrawImage(Image, PositionX, PositionY, Image.Width, Image.Height);

        }

        public override bool IsAlive()
        {
            if (Lives > 0 && this != null)
            {
                return true;
            }
            return false;
        }



        protected abstract void OnCollision(Missile m, int numberOfPixelsInCollision);






    }
}
