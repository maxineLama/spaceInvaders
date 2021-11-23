using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represents the entire game, it implements the singleton pattern
    /// </summary>
    class Game
    {

        #region GameObjects management
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }

        public EnemyBlock enemies;
        public PlayerSpaceship playerSpaceShip;
        
        public enum GameState
        {
            Play,
            Pause
        }
        public GameState state = GameState.Play;
        public Bunker b1;
        public Bunker b2;
        public Bunker b3;


        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            Vecteur2D pos = new Vecteur2D(265, 550);
            Bitmap img = SpaceInvaders.Properties.Resources.ship3;
            this.playerSpaceShip = new PlayerSpaceship(pos, 3, img);

            this.gameSize = gameSize;
            gameObjects.Add(playerSpaceShip);

            Bitmap imgB = SpaceInvaders.Properties.Resources.bunker;
            Bitmap imgB2 = SpaceInvaders.Properties.Resources.bunker;
            Bitmap imgB3 = SpaceInvaders.Properties.Resources.bunker;
            Vecteur2D posB = new Vecteur2D(50, 480);
            Vecteur2D posB2 = new Vecteur2D(250, 480);
            Vecteur2D posB3 = new Vecteur2D(450, 480);

            this.b1 = new Bunker(posB, 300, imgB);
            this.b2 = new Bunker(posB2, 300, imgB2);
            this.b3 = new Bunker(posB3, 300, imgB3);
            gameObjects.Add(b1);
            gameObjects.Add(b2);
            gameObjects.Add(b3);

            Vecteur2D posEnemies = new Vecteur2D(15,0);
            enemies = new EnemyBlock(posEnemies, gameSize.Width);

            Bitmap imgEnemies = SpaceInvaders.Properties.Resources.ship1;
            enemies.AddLine(5, 1, imgEnemies);
           

            Bitmap imgEnemies2 = SpaceInvaders.Properties.Resources.ship5;
            enemies.AddLine(9, 1, imgEnemies2);

            Bitmap imgEnemies3 = SpaceInvaders.Properties.Resources.ship7;
            enemies.AddLine(5, 1, imgEnemies3);


            Bitmap imgEnemies4 = SpaceInvaders.Properties.Resources.ship8;
            enemies.AddLine(9, 1, imgEnemies4);
            Bitmap imgEnemies5 = SpaceInvaders.Properties.Resources.ship4;
            enemies.AddLine(5, 1, imgEnemies5);




            gameObjects.Add(enemies);

        }

        #endregion

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }


        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            float x = 255f;
            float y = 255f;
            if (state == GameState.Pause)
            {
               
                g.DrawString("PAUSE", defaultFont,drawBrush,x,y);
            }
            else
            {
               
                foreach (GameObject gameObject in gameObjects)
                    gameObject.Draw(this, g);
            }
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();


            // if space is pressed
            if (keyPressed.Contains(Keys.P))
            {
                if (state == GameState.Play)
                {

                    state = GameState.Pause;
                   

                }
                else
                {
                    state = GameState.Play;
                   
                }
                ReleaseKey(Keys.P);
                // create new BalleQuiTombe
                //GameObject newObject = new BalleQuiTombe(gameSize.Width / 2, 0);
                // add it to the game
                // AddNewGameObject(newObject);
                // release key space (no autofire)
                //ReleaseKey(Keys.Space);
            }

            // update each game object
            foreach (GameObject gameObject in gameObjects.ToList())
            {
                gameObject.Update(this, deltaT);
            }

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }
        #endregion
    }
}