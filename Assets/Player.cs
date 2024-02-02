using Biking_Badass.Engine;
using CSdoom.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using static CSdoom.Engine.Utilities;

namespace CSdoom.Assets
{
    public class Player
    {
        public Vector3 position;
        public Vector2 direction;
        public Vector3 velocity;
        public float rotation;
        public float height;

        public float MouseSensitivity = 10, maxSpeed = 1, friction = 0.1f;

        public float size = 0.4f;
        public Camera camera;
        public Map.Room CurrentRoom;

        public Weapon weapon;

        public Player(Vector2 spawnPos, float height, Game1 game)
        {
            position = new Vector3(spawnPos.X, 0, spawnPos.Y);
            this.height = height;

            camera = new Camera(spawnPos, height, rotation, game);
            weapon = new Weapon("gun","bullet", game);
        }

        private Vector2 mouse;

        public void CheckColision(Map map)
        {
            foreach (Map.Room r in map.rooms)
            {
                if (r.bounds.Contains(new Vector2(position.X, position.Z)))
                {
                    CurrentRoom = r;
                    float xMin = CurrentRoom.enabledWalls.Contains(2) ? CurrentRoom.bounds.Left + size : float.NegativeInfinity;
                    float xMax = CurrentRoom.enabledWalls.Contains(3) ? CurrentRoom.bounds.Right - size : float.PositiveInfinity;

                    float zMin = CurrentRoom.enabledWalls.Contains(0) ? CurrentRoom.bounds.Top + size : float.NegativeInfinity;
                    float zMax = CurrentRoom.enabledWalls.Contains(1) ? CurrentRoom.bounds.Bottom - size : float.PositiveInfinity;
                    position.X = MathHelper.Clamp(position.X, xMin, xMax);
                    position.Z = MathHelper.Clamp(position.Z, zMin, zMax);

                    break;
                }
            }
        }

        public void Update(Game1 game)
        {
            camera.Update(game);
            weapon.Update(game);
            direction = PlayerDirection(rotation);

            rotation -= Input.MouseDelta(mouse, true).X * MouseSensitivity * game.deltaTime;
            mouse = Input.MousePos();

            velocity.X += direction.X * game.deltaTime;
            velocity.Z += direction.Y * game.deltaTime;

            position.X += velocity.X;
            position.Z += velocity.Z;

            velocity.X = MathHelper.Lerp(velocity.X, 0, friction);
            velocity.Z = MathHelper.Lerp(velocity.Z, 0, friction);

            camera.position = new Vector3(position.X, height, position.Z);
            camera.rotation = rotation;
            CheckColision(game.map);
            if(Input.MousePressed(0))
            {
                weapon.Shoot(game);
            }
        }

        public void DrawUI(Game1 game)
        {
            weapon.Draw(game);
        }
    }
}