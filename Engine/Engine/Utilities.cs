using Biking_Badass.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSdoom.Engine
{
    public static class Utilities
    {

        public static List<VertexPositionTexture> HorizontalTextureQuad(Rectangle bounds, float height)
        {
            return new List<VertexPositionTexture>
            {
                new VertexPositionTexture(new Vector3(bounds.Left, height, bounds.Top), TopLeft(bounds)),
                new VertexPositionTexture(new Vector3(bounds.Right, height, bounds.Top), TopRight(bounds)),
                new VertexPositionTexture(new Vector3(bounds.Left, height, bounds.Bottom), BottomLeft(bounds)),

                new VertexPositionTexture(new Vector3(bounds.Left, height, bounds.Bottom), BottomLeft(bounds)),
                new VertexPositionTexture(new Vector3(bounds.Right, height, bounds.Top), TopRight(bounds)),
                new VertexPositionTexture(new Vector3(bounds.Right, height, bounds.Bottom), BottomRight(bounds)),
            };
        }

        public static List<VertexPositionTexture> VerticalTextureQuad(Vector2 start, Vector2 end, float height)
        {
            return new List<VertexPositionTexture>
            {
                new VertexPositionTexture(new Vector3(start.X, height, start.Y), new Vector2(Magnitude(start), height)),
                new VertexPositionTexture(new Vector3(start.X, 0, start.Y), new Vector2(Magnitude(start), 0)),
                new VertexPositionTexture(new Vector3(end.X, height, end.Y), new Vector2(Magnitude(end), height)),

                new VertexPositionTexture(new Vector3(start.X, 0, start.Y), new Vector2(Magnitude(start), 0)),
                new VertexPositionTexture(new Vector3(end.X, 0, end.Y), new Vector2(Magnitude(end), 0)),
                new VertexPositionTexture(new Vector3(end.X, height, end.Y), new Vector2(Magnitude(end), height)),
            };
        }

        public static Vector2 TopRight(Rectangle bounds)
        {
            return new Vector2(bounds.Right, bounds.Top);
        }

        public static Vector2 TopLeft(Rectangle bounds)
        {
            return new Vector2(bounds.Left, bounds.Top);
        }

        public static Vector2 BottomRight(Rectangle bounds)
        {
            return new Vector2(bounds.Right, bounds.Bottom);
        }

        public static Vector2 BottomLeft(Rectangle bounds)
        {
            return new Vector2(bounds.Left, bounds.Bottom);
        }

        public static Vector2 PlayerDirection(float rotation)
        {
            Matrix r = Matrix.CreateRotationZ(MathHelper.ToRadians(-rotation));

            //????????? fucking finally. still specialized and not generalizable
            float x = Input.Axis(Keys.A, Keys.D);
            float y = Input.Axis(Keys.S, Keys.W);
            return Vector2.Transform(new Vector2(-x, y), r);
        }

        public static float Magnitude(Vector2 V)
        {
            float x = V.X * V.X;
            float y = V.Y * V.Y;
            return MathF.Sqrt(x + y);
        }

        public struct RoomData
        {
            public Rectangle bounds;
            public int fHeight, cHeight, fIndex, wIndex, cIndex;
            public int[] enabledWalls;

            public RoomData(Rectangle bounds, int fHeight, int cHeight, int fIndex, int wIndex, int cIndex, List<int> enabledWalls)
            {
                this.bounds = bounds;
                this.fHeight = fHeight;
                this.cHeight = cHeight;
                this.fIndex = fIndex;
                this.wIndex = wIndex;
                this.cIndex = cIndex;
                this.enabledWalls = enabledWalls.ToArray();
            }
        }
        public static void DrawPoint(Vector3 position, float size, Color color, Game1 game)
        {
            VertexBuffer buffer = new VertexBuffer(game.GraphicsDevice, VertexPositionColor.VertexDeclaration, 6, BufferUsage.None);
            VertexPositionColor[] vert = new VertexPositionColor[]
            {
                new VertexPositionColor(position + (Vector3.UnitX*size), color),
                new VertexPositionColor(position + (Vector3.UnitY*size), color),
                new VertexPositionColor(position - (Vector3.UnitY*size), color),

                new VertexPositionColor(position + (Vector3.UnitY*size), color),
                new VertexPositionColor(position - (Vector3.UnitX*size), color),
                new VertexPositionColor(position - (Vector3.UnitY*size), color),
            };

            buffer.SetData<VertexPositionColor>(vert);
            game.GraphicsDevice.SetVertexBuffer(buffer);

            var effect = game.effect;
            effect.View = game.map.effect.View;
            effect.Projection = game.map.effect.Projection;

            foreach (EffectPass pass in game.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                game.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            }
            buffer.Dispose();
        }
        public static void DrawLine(Vector3 start, Vector3 end, Color color, Game1 game)
        {
            VertexBuffer buffer = new VertexBuffer(game.GraphicsDevice, VertexPositionColor.VertexDeclaration, 2, BufferUsage.None);
            buffer.SetData<VertexPositionColor>(new VertexPositionColor[] { new VertexPositionColor(start, color), new VertexPositionColor(end, color) });
            game.GraphicsDevice.SetVertexBuffer(buffer);

            var effect = game.effect;
            effect.View = game.map.effect.View;
            effect.Projection = game.map.effect.Projection;
            
            foreach (EffectPass pass in game.effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                
                game.GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, 0, 2);
            }
            buffer.Dispose();
        }

        public static void DrawRay(Ray ray, float distance, Color color, Game1 game)
        {
            DrawLine(ray.Position, ray.Position + (ray.Direction * distance), color, game);
        }

        public static float Hash(float value, int seed)
        {
            return (seed * MathF.Sin(seed * value)) % 1;
        }
        public static Vector3 HashVec3(float value, int seed)
        {
            return new Vector3(Hash(value, seed), Hash(value, seed + 1), Hash(value, seed - 1));
        }
    }
}