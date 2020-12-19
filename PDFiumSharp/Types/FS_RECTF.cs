#region Copyright and License
/*
This file is part of PDFiumSharp, a wrapper around the PDFium library for the .NET framework.
Copyright (C) 2017 Tobias Meyer
License: Microsoft Reciprocal License (MS-RL)
*/
#endregion
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PDFiumSharp.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FS_RECTF
    {
        public float Left { get; }
        public float Top { get; }
        public float Right { get; }
        public float Bottom { get; }

        public FS_RECTF(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public FS_RECTF Union(FS_RECTF rectangle)
        {
            return new FS_RECTF(
                Math.Min(Left, rectangle.Left),
                Math.Max(Top, rectangle.Top),
                Math.Max(Right, rectangle.Right),
                Math.Min(Bottom, rectangle.Bottom));
        }

        public FS_RECTF UnionAll(List<FS_RECTF> rectangles)
        {
            var rectList = new List<FS_RECTF>(rectangles)
            {
                this
            };
            return Union(rectList);
        }

        public static FS_RECTF Union(List<FS_RECTF> rectangles)
        {
            if (rectangles == null || rectangles.Count == 0)
            {
                return new FS_RECTF();
            }
            else
            {
                float left = rectangles[0].Left, right = rectangles[0].Right, top = rectangles[0].Top, bottom = rectangles[0].Top;
                for (int i = 1; i < rectangles.Count; i++)
                {
                    left   = Math.Min(left,   rectangles[i].Left);
                    top    = Math.Max(top,    rectangles[i].Top);
                    right  = Math.Max(right,  rectangles[i].Right);
                    bottom = Math.Min(bottom, rectangles[i].Bottom);
                }
                return new FS_RECTF(left, top, right, bottom);
            }
        }

        public float Height => Top - Bottom;

        public float Width => Right - Left;

        public bool IntersectsWith(FS_RECTF rectangle) => (Left < rectangle.Right && Right > rectangle.Left
                        && Top > rectangle.Bottom && Bottom < rectangle.Top);

        public bool Contains(FS_RECTF rectangle) => Left <= rectangle.Left && Right >= rectangle.Right
                && Top >= rectangle.Top && Bottom <= rectangle.Bottom;

        public bool ContainsPartially(FS_RECTF rectangle, float minPercentContained)
        {
            if (IntersectsWith(rectangle))
            {
                // calculate area of intersection
                float left   = Math.Max(Left, rectangle.Left),
                      right  = Math.Min(Right, rectangle.Right),
                      bottom = Math.Max(Bottom, rectangle.Bottom),
                      top    = Math.Min(Top, rectangle.Top);

                float areaContained = (right - left) * (top - bottom);

                return areaContained >= rectangle.Height * rectangle.Width * (minPercentContained / 100);
            }
            else
            {
                return false;
            }
        }
    }
}
