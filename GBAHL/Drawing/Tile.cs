﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GBAHL.Drawing
{
    /// <summary>
    /// Represents an 8x8 array of pixel data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Tile : IEquatable<Tile>
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Row
        {
            private byte p0;
            private byte p1;
            private byte p2;
            private byte p3;
            private byte p4;
            private byte p5;
            private byte p6;
            private byte p7;

            public byte this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return p0;
                        case 1: return p1;
                        case 2: return p2;
                        case 3: return p3;
                        case 4: return p4;
                        case 5: return p5;
                        case 6: return p6;
                        case 7: return p7;

                        default:
                            throw new IndexOutOfRangeException();
                    }
                }

                set
                {
                    switch (index)
                    {
                        case 0: p0 = value; break;
                        case 1: p1 = value; break;
                        case 2: p2 = value; break;
                        case 3: p3 = value; break;
                        case 4: p4 = value; break;
                        case 5: p5 = value; break;
                        case 6: p6 = value; break;
                        case 7: p7 = value; break;

                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
            }

            public bool Equals(Row other)
            {
                return p0 == other.p0
                    && p1 == other.p1
                    && p2 == other.p2
                    && p3 == other.p3
                    && p4 == other.p4
                    && p5 == other.p5
                    && p6 == other.p6
                    && p7 == other.p7;
            }

            public override bool Equals(object obj)
            {
                return (obj is Row) && Equals((Row)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return p1.GetHashCode() ^ p2.GetHashCode() ^ p3.GetHashCode()
                         ^ p4.GetHashCode() ^ p5.GetHashCode() ^ p6.GetHashCode()
                         ^ p7.GetHashCode();
                }
            }

            public static bool operator ==(Row r1, Row r2)
            {
                return r1.Equals(r2);
            }

            public static bool operator !=(Row r1, Row r2)
            {
                return !(r1 == r2);
            }
        }

        private Row row0;
        private Row row1;
        private Row row2;
        private Row row3;
        private Row row4;
        private Row row5;
        private Row row6;
        private Row row7;

        /// <summary>
        /// Gets or sets the specified pixel.
        /// </summary>
        /// <param name="index">The index of the pixel.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public byte this[int index]
        {
            get => this[index % 8, index / 8];
            set => this[index % 8, index / 8] = value;
        }

        /// <summary>
        /// Gets or sets the specified pixel.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public byte this[int x, int y]
        {
            get
            {
                switch (y)
                {
                    case 0: return row0[x];
                    case 1: return row1[x];
                    case 2: return row2[x];
                    case 3: return row3[x];
                    case 4: return row4[x];
                    case 5: return row5[x];
                    case 6: return row6[x];
                    case 7: return row7[x];

                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (y)
                {
                    case 0: row0[x] = value; break;
                    case 1: row1[x] = value; break;
                    case 2: row2[x] = value; break;
                    case 3: row3[x] = value; break;
                    case 4: row4[x] = value; break;
                    case 5: row5[x] = value; break;
                    case 6: row6[x] = value; break;
                    case 7: row7[x] = value; break;

                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Determines whether this <see cref="Tile"/> is equivalent to the specified
        /// <see cref="Tile"/> value with the specified flipping.
        /// </summary>
        /// <param name="other">The tile to compare to.</param>
        /// <param name="flipX">Determines whether <paramref name="other"/> is to be flipped from left to right.</param>
        /// <param name="flipY">Determines whether <paramref name="other"/> is to be flipped from top to bottom.</param>
        /// <returns><c>true</c> if the tiles are equivalent; otherwise, <c>false</c>.</returns>
        public bool Equals(Tile other, bool flipX = false, bool flipY = false)
        {
            if (flipX || flipY)
            {
                for (int srcY = 0; srcY < 8; srcY++)
                {
                    for (int srcX = 0; srcX < 8; srcX++)
                    {
                        var dstX = flipX ? (7 - srcX) : srcX;
                        var dstY = flipY ? (7 - srcY) : srcY;

                        if (this[srcX, srcY] != other[dstX, dstY])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            else
            {
                return row0 == other.row0
                    && row1 == other.row1
                    && row2 == other.row2
                    && row3 == other.row3
                    && row4 == other.row4
                    && row5 == other.row5
                    && row6 == other.row6
                    && row7 == other.row7;
            }
        }

        /// <summary>
        /// Determines whether this <see cref="Tile"/> is equivalent to the specified <see cref="Tile"/>.
        /// </summary>
        /// <param name="other">The tile to compare to.</param>
        /// <returns></returns>
        public bool Equals(Tile other)
        {
            return Equals(other, false, false);
        }

        /// <summary>
        /// Determines whether this <see cref="Tile"/> is equivalent to the specified object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is Tile) && Equals((Tile)obj);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Tile"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = -1582387468;
                hashCode = hashCode * -1521134295 + row0.GetHashCode();
                hashCode = hashCode * -1521134295 + row1.GetHashCode();
                hashCode = hashCode * -1521134295 + row2.GetHashCode();
                hashCode = hashCode * -1521134295 + row3.GetHashCode();
                hashCode = hashCode * -1521134295 + row4.GetHashCode();
                hashCode = hashCode * -1521134295 + row5.GetHashCode();
                hashCode = hashCode * -1521134295 + row6.GetHashCode();
                hashCode = hashCode * -1521134295 + row7.GetHashCode();
                return hashCode;
            }
        }
    }
}
