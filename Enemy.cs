using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spaceshooter
{
    class Enemy : GameObject
    {
        
        
        private Random rnd;
        
        public Enemy()
        {
            rnd = new Random();
            offset = Vector2.Zero;
            color = Color.White;

        }
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[4];

            sprites[0] = content.Load<Texture2D>("enemyBlue1");
            sprites[1] = content.Load<Texture2D>("enemyBlack1");
            sprites[2] = content.Load<Texture2D>("enemyGreen1");
            sprites[3] = content.Load<Texture2D>("enemyRed1");
            
            
            Respawn();




        }

        public override void Update(GameTime gameTime)
        {
            if (position.Y > GameWorld.ScreenSize.Y)
            {
                Respawn();
            }
            Move(gameTime);
            
            

            
        }
        
        public void Respawn()
        {
            int index = rnd.Next(0, 4);
            sprite = sprites[index];

            velocity = new Vector2(0, 1);
            speed = rnd.Next(50, 100);
            position.X = rnd.Next(0, (int)GameWorld.ScreenSize.X - sprite.Width);
            position.Y = 0;
            
        }
        public override void OnCollision(GameObject other)
        {
            if(other is Laser)
            {
                GameWorld.Destroy(other);
                Respawn();
            }
        }
        


    }
}
