using System;

namespace SpaceInvaders
{
    class Vecteur2D
    {
        public double x, y;

        public Vecteur2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Vecteur2D()
        {
            this.x = 0.0;
            this.y = 0.0;
        }

        public double norme()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public static Vecteur2D addition(Vecteur2D v1, Vecteur2D v2)
        {

            return new Vecteur2D(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vecteur2D soustraction(Vecteur2D v1, Vecteur2D v2)
        {

            return new Vecteur2D(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vecteur2D inverse(Vecteur2D v)
        {
            v.x *= -1.0;
            v.y *= -1.0;
            return v;
        }

        public static Vecteur2D scalaireGauche(double n, Vecteur2D v)
        {
            n *= v.x;
            n *= v.y;
            return v;
        }

        public static Vecteur2D scalaireDroite(Vecteur2D v, double n)
        {
            v.x *= n;
            v.y *= n;
            return v;
        }

        public static Vecteur2D divScalaire(Vecteur2D v, double n)
        {
            if (n == 0)
            {
                throw new DivideByZeroException();
            }
            v.x /= n;
            v.y /= n;
            return v;
        }



    }
}
