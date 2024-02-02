using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSdoom.Engine
{
    public class Animation
    {
        public Sprite target;
        private Vector2 StartPosition;

        public struct Keyframe
        {
            public float time;
            public Vector2 position;

            public Keyframe(float time, Vector2 position)
            {
                this.time = time;
                this.position = position;
            }
        }

        public List<Keyframe> keyframes;

        public Animation(Sprite target)
        {
            this.keyframes = new List<Keyframe>();
            this.target = target;
            StartPosition = target.position;
        }

        private int keyIndex = 0;
        private float time = 0;
        private Vector2 pos;
        private Vector2 last = Vector2.Zero;

        public void PlayRelative(Game1 game)
        {
            time += game.deltaTime;
            if (time >= keyframes[keyIndex].time)
            {
                last = keyframes[keyIndex].position;
                keyIndex = (keyIndex + 1) % keyframes.Count;
                pos = keyframes[keyIndex].position;
                time = 0;
            }

            target.position = Vector2.Lerp(target.position, pos, time * keyframes[keyIndex].time);
        }
    }
}