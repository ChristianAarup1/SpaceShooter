using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Content;


namespace Spaceshooter
{
    public class GameWorld : Game
    {
        //Dette er til github test, hvis denne er der, så virker det!
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private static Vector2 screenSize;
        private static List<GameObject> deleteObjects;
        private List<GameObject> gameObjects;
        private static List<GameObject> newGameObjects;
        private float timeElapsedEnemySpawn;
        private Texture2D collisionTexture;
        private Texture2D background;
        private Rectangle rectangleBackground;
        private Rectangle scroll;




        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameObjects = new List<GameObject>();
            newGameObjects = new List<GameObject>();
            deleteObjects = new List<GameObject>();
            gameObjects.Add(new Player());
            gameObjects.Add(new Enemy());
            gameObjects.Add(new Enemy());
            gameObjects.Add(new Enemy());




            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Loads the background into the sprite texture
            background = Content.Load<Texture2D>("background");
            rectangleBackground = new Rectangle(0, 0, background.Width, background.Height);
            // TODO: use this.Content to load your game content here

            collisionTexture = Content.Load<Texture2D>("CollisionTexture (1)");

            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(this.Content);

            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameObjects.AddRange(newGameObjects);
            newGameObjects.Clear();
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);

                foreach (GameObject other in gameObjects)
                {
                    go.CheckCollision(other);
                }
            }
            foreach (GameObject go in deleteObjects)
            {
                gameObjects.Remove(go);
            }
            deleteObjects.Clear();
            timeElapsedEnemySpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;


            //if (timeElapsedEnemySpawn > 3f)
            //{
            //    Enemy spawn = new Enemy();
            //    spawn.LoadContent(Content);
            //    gameObjects.Add(spawn);
            //    timeElapsedEnemySpawn = 0f;
            //}

            gameObjects.AddRange(newGameObjects);
            newGameObjects.Clear();




            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(background, rectangleBackground, new Rectangle(-scroll.X, -scroll.Y, rectangleBackground.Width, rectangleBackground.Height), Color.White);
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
#if DEBUG
                DrawCollisionBox(go);
#endif
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
        private void DrawCollisionBox(GameObject go)
        {
            Rectangle topLine = new Rectangle(go.Collision.X, go.Collision.Y, go.Collision.Width, 1);
            Rectangle bottomLine = new Rectangle(go.Collision.X, go.Collision.Y + go.Collision.Height, go.Collision.Width, 1);
            Rectangle rightLine = new Rectangle(go.Collision.X + go.Collision.Width, go.Collision.Y, 1, go.Collision.Height);
            Rectangle leftLine = new Rectangle(go.Collision.X, go.Collision.Y, 1, go.Collision.Height);

            spriteBatch.Draw(collisionTexture, topLine, Color.Red);
            spriteBatch.Draw(collisionTexture, bottomLine, Color.Red);
            spriteBatch.Draw(collisionTexture, rightLine, Color.Red);
            spriteBatch.Draw(collisionTexture, leftLine, Color.Red);
        }
        public static void Instantiate(GameObject go)
        {
            newGameObjects.Add(go);
        }

        public static void Destroy(GameObject go)
        {
            deleteObjects.Add(go);
        }


    }
}
