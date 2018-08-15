using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RSDKv3
{
    public class CollisionMask
    {

        const int TILES_COUNT = 1024;

        public TileConfig[] CollisionPath1 = new TileConfig[TILES_COUNT];
        public TileConfig[] CollisionPath2 = new TileConfig[TILES_COUNT];

        public class TileConfig
        {
            // Collision position for each pixel
            public byte[] Collision = new byte[16]; //two Collision Values per byte

            // If has collision
            public bool[] HasCollision = new bool[16];

            // Unknown 5 bytes config
            //public byte[] Config;

            // Collision Mask config

            //for special occasions, like conveyor belts
            public byte special;
            //The Slope's angle
            public byte slopeAngle;
            //How the player's physics react to the slope
            public byte physics;
            //How much momentum is gained from the slope
            public byte momentum;
            //Unknown
            public byte unknown;


            public TileConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

            internal TileConfig(Reader reader)
            {

                special = reader.ReadByte();
                slopeAngle = reader.ReadByte();
                physics = reader.ReadByte();
                momentum = reader.ReadByte();
                unknown = reader.ReadByte();

                byte[] collision = reader.ReadBytes(8);

                ushort ActiveCollision = reader.ReadUInt16();

                int i = 0;
                int i2 = 1;

                int b = 0;

                for (int c = 0; c < 8; c++)
                {
                    Collision[i] = (byte)((collision[c] & 0xF0) >> 4);
                    Collision[i2] = (byte)(collision[c] & 0x0F);
                    i += 2;
                    i2 += 2;
                }

                for (int ii = 0; ii < 16; ii++)
                {
                    HasCollision[ii] = IsBitSet(ActiveCollision, b);
                    b++;
                }

            }

            public void Write(Writer writer)
            {
                writer.Write(special);
                writer.Write(slopeAngle);
                writer.Write(physics);
                writer.Write(momentum);
                writer.Write(unknown);

                byte[] collision = new byte[8];
                int CollisionActive = 0;

                int c = 0;

                for (int i = 0; i < 8; i++)
                {
                    collision[i] = AddNibbles(Collision[c++], Collision[c++]);
                }

                for (int i = 0; i < 16; i++)
                {
                    if (HasCollision[i])
                    {
                        CollisionActive |= 1 << i;
                    }
                    if (!HasCollision[i])
                    {
                        CollisionActive |= 0 << i;
                    }
                }

                writer.Write(collision);

                writer.Write((byte)(CollisionActive & 0xff));
                writer.Write((byte)(CollisionActive >> 8));
            }

            public byte AddNibbles(byte a, byte b)
            {
                return (byte)((a & 0xF) << 4 | (b & 0xF));
            }

            public bool IsBitSet(ushort b, int pos)
            {
                return (b & (1 << pos)) != 0;
            }

            public Bitmap DrawCMask(System.Drawing.Color bg, System.Drawing.Color fg, Bitmap tile = null)
            {
                Bitmap b;
                bool HasTile = false;
                if (tile == null)
                { b = new Bitmap(16, 16); }
                else
                {
                    b = tile.Clone(new Rectangle(0, 0, tile.Width, tile.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                    HasTile = true;
                }

                if (!HasTile)
                {
                    for (int h = 0; h < 16; h++) //Set the BG colour
                    {
                        for (int w = 0; w < 16; w++)
                        {
                            b.SetPixel(w, h, bg);
                        }
                    }
                }

                for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                {
                    if (Collision[w] <= 15 && HasCollision[w])
                    {
                        b.SetPixel(w, 15, fg);
                    }
                    if (Collision[w] <= 14 && HasCollision[w])
                    {
                        b.SetPixel(w, 14, fg);
                    }
                    if (Collision[w] <= 13 && HasCollision[w])
                    {
                        b.SetPixel(w, 13, fg);
                    }
                    if (Collision[w] <= 12 && HasCollision[w])
                    {
                        b.SetPixel(w, 12, fg);
                    }
                    if (Collision[w] <= 11 && HasCollision[w])
                    {
                        b.SetPixel(w, 11, fg);
                    }
                    if (Collision[w] <= 10 && HasCollision[w])
                    {
                        b.SetPixel(w, 10, fg);
                    }
                    if (Collision[w] <= 9 && HasCollision[w])
                    {
                        b.SetPixel(w, 9, fg);
                    }
                    if (Collision[w] <= 8 && HasCollision[w])
                    {
                        b.SetPixel(w, 8, fg);
                    }
                    if (Collision[w] <= 7 && HasCollision[w])
                    {
                        b.SetPixel(w, 7, fg);
                    }
                    if (Collision[w] <= 6 && HasCollision[w])
                    {
                        b.SetPixel(w, 6, fg);
                    }
                    if (Collision[w] <= 5 && HasCollision[w])
                    {
                        b.SetPixel(w, 5, fg);
                    }
                    if (Collision[w] <= 4 && HasCollision[w])
                    {
                        b.SetPixel(w, 4, fg);
                    }
                    if (Collision[w] <= 3 && HasCollision[w])
                    {
                        b.SetPixel(w, 3, fg);
                    }
                    if (Collision[w] <= 2 && HasCollision[w])
                    {
                        b.SetPixel(w, 2, fg);
                    }
                    if (Collision[w] <= 1 && HasCollision[w])
                    {
                        b.SetPixel(w, 1, fg);
                    }
                    if (Collision[w] <= 0 && HasCollision[w])
                    {
                        b.SetPixel(w, 0, fg);
                    }
                }

                return b;
            }

        }

        public CollisionMask(string filename) : this(new Reader(filename))
        {

        }

        public CollisionMask(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        private CollisionMask(Reader reader)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                CollisionPath1[i] = new TileConfig(reader);
                CollisionPath2[i] = new TileConfig(reader);
            }
            reader.Close();
        }

        public void Write(string filename)
        {
            Write(new Writer(filename));
        }

        public void Write(System.IO.Stream s)
        {
            Write(new Writer(s));
        }

        public void Write(Writer writer)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                CollisionPath1[i].Write(writer);
                CollisionPath2[i].Write(writer);
            }
            writer.Close();
        }

    }
}
