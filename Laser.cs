using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Spaceshooter
{
    class Laser : GameObject
    {
        public Laser(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.position = position;
            velocity = new Vector2(0, -1);
            color = Color.White;
            speed = 500;
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            if(position.Y < 0)
            {
                GameWorld.Destroy(this);
            }
        }

    }
}
