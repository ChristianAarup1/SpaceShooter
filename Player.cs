using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Spaceshooter
{
    class Player : GameObject
    {


        private Vector2 spawnOffset;
        private Texture2D laser;
        private bool canFire;
        private int fireTrigger;


        public Player()
        {
            speed = 250;
            velocity = Vector2.Zero;
            fps = 10;
            color = Color.White;
            canFire = true;
            fireTrigger = 0;
            spawnOffset = new Vector2(-25, -105);

        }
        /// <summary>
        /// Calls the methods that are used, and updates them in each gameloop
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            HandleInput();
            Move(gameTime);
            Animate(gameTime);
            ScreenWarp();
            ScreenLimits();


        }
        /// <summary>
        /// Handles the input of the player
        /// </summary>
        private void HandleInput()
        {
            //Reset velocity
            //Makes sure that we will stop moving when no keys are pressed
            velocity = Vector2.Zero;

            //Gets the current keyboard state
            KeyboardState keyState = Keyboard.GetState();

            //If we press W

            if (keyState.IsKeyDown(Keys.W))
            {
                //Move up
                velocity += new Vector2(0, -1);
            }
            //If we press S
            if (keyState.IsKeyDown(Keys.S))
            {
                //Move down
                velocity += new Vector2(0, 1);
            }
            //If we press A
            if (keyState.IsKeyDown(Keys.A))
            {
                //Move Left
                velocity += new Vector2(-1, 0);
            }
            //If we press D
            if (keyState.IsKeyDown(Keys.D))
            {
                //Move Right
                velocity += new Vector2(1, 0);
            }
            //If we press space
            if (keyState.IsKeyDown(Keys.Space) & canFire)
            {
                //Shoot
                canFire = false;
                GameWorld.Instantiate(new Laser(laser, new Vector2(position.X + spawnOffset.X, position.Y + spawnOffset.Y)));
            }

            if (!canFire && fireTrigger < 50)
            {
                fireTrigger++;
            }
            else
            {
                canFire = true;
                fireTrigger = 0;
            }
            //Makes sure that the vector is normalized so we dont move faster if we press two keys at once
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[4];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>((i + 1) + "fwd");

            }
            sprite = sprites[0];

            this.position = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y - sprite.Height / 2);
            this.origin = new Vector2(sprite.Height / 2, sprite.Width / 2);
            this.offset.X = (-sprite.Width / 2) - 20;
            this.offset.Y = -sprite.Height / 2;


        }

        private void ScreenWarp()
        {
            if (position.X > GameWorld.ScreenSize.X + sprite.Width)
            {
                position.X = -sprite.Width;
            }
            else if (position.X < -sprite.Width)
            {
                position.X = GameWorld.ScreenSize.X + sprite.Width;
            }
        }

        private void ScreenLimits()
        {
            if (position.Y - sprite.Height / 2 < 0)
            {
                position.Y = sprite.Height / 2;
            }
            else if (position.Y > GameWorld.ScreenSize.Y)
            {
                position.Y = GameWorld.ScreenSize.Y;
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy enemy)
            {
                color = Color.Green;
                enemy.Respawn();
            }
        }
 
       
    }
}
