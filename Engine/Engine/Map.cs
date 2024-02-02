using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using static CSdoom.Engine.Utilities;

namespace CSdoom.Engine
{
    public class Map
    {
        public Texture2D[] textures;
        public VertexBuffer[] buffers;
        public BasicEffect effect;
        public Room[] rooms;
        private List<VertexPositionTexture>[] vertexGroups;

        public Map(List<RoomData> data, Game1 game)
        {
            textures = new Texture2D[]
            {
               game.Content.Load<Texture2D>("Wood"),
               game.Content.Load<Texture2D>("Brick"),
            };
            // set vertexGroups
            vertexGroups = new List<VertexPositionTexture>[textures.Length];
            for (int i = 0; i < vertexGroups.Length; i++) vertexGroups[i] = new List<VertexPositionTexture>();

            effect = new BasicEffect(game.GraphicsDevice);
            effect.TextureEnabled = true;
                      

            rooms = new Room[data.Count];

            for (int i = 0; i < rooms.Length; i++) rooms[i] = new Room(data[i], this);

            buffers = new VertexBuffer[textures.Length];

            for (int i = 0; i < buffers.Length; i++)
            {
                buffers[i] = new VertexBuffer(game.GraphicsDevice, VertexPositionTexture.VertexDeclaration, vertexGroups[i].Count, BufferUsage.WriteOnly);

                if (vertexGroups[i].Count > 0)
                    buffers[i].SetData(vertexGroups[i].ToArray());
            }
        }

        public void Draw(Game1 game)
        {
            effect.View = game.player.camera.view;
            effect.Projection = game.player.camera.perspective;
            

            for (int i = 0; i < buffers.Length; i++)
            {
                if (vertexGroups[i].Count > 0)
                {
                    effect.Texture = textures[i];
                    game.GraphicsDevice.SetVertexBuffer(buffers[i]);
                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        game.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexGroups[i].Count / 3);
                    }
                }
            }
        }

        public struct Room
        {
            public Rectangle bounds;
            public int floorHeight, ceilHeight, floorTextureIndex, wallTextureIndex, ceilTextureIndex;
            public int[] enabledWalls;

            public Room(RoomData data, Map map)
            {
                bounds = data.bounds;
                floorHeight = data.fHeight;
                ceilHeight = data.cHeight;
                wallTextureIndex = data.wIndex;
                floorTextureIndex = data.fIndex;
                ceilTextureIndex = data.cIndex;
                enabledWalls = data.enabledWalls;

                // load

                map.vertexGroups[floorTextureIndex].AddRange(HorizontalTextureQuad(bounds, floorHeight));
                map.vertexGroups[ceilTextureIndex].AddRange(HorizontalTextureQuad(bounds, ceilHeight));

                List<VertexPositionTexture> walls = new List<VertexPositionTexture>();
                if (enabledWalls.Contains(0)) walls.AddRange(VerticalTextureQuad(TopLeft(bounds), TopRight(bounds), ceilHeight));
                if (enabledWalls.Contains(3)) walls.AddRange(VerticalTextureQuad(TopRight(bounds), BottomRight(bounds), ceilHeight));
                if (enabledWalls.Contains(1)) walls.AddRange(VerticalTextureQuad(BottomRight(bounds), BottomLeft(bounds), ceilHeight));
                if (enabledWalls.Contains(2)) walls.AddRange(VerticalTextureQuad(BottomLeft(bounds), TopLeft(bounds), ceilHeight));

                map.vertexGroups[wallTextureIndex].AddRange(walls);
            }
        }
    }
}