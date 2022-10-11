using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices;

namespace Lapes_triusiai
{
    internal class Field
    {
        private int width, height;
        private IActor[,] field;

        public int Witdth
        {
            get => width;
        }
        public int Height => height;

        public Field(int with, int height)
        {
            this.width = with;
            this.height = height;
            field = new IActor[width, height];
        }

        //field = new IActor[width, height];
        public void Clear()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    field[i, j] = null;
                }
            }
        }
        public void Clear(Point position)
        {
            field[position.X, position.Y] = null;
        }
        public void Place(IActor actor)
        {
            field[actor.Location.X, actor.Location.Y] = actor;
        }

        public IActor GetActorAt(Point position)
        {
            return field[position.X, position.Y];
        }

        public IActor GetActorAt(int x, int y)
        {
            return field[x, y];
        }
        public List<Point> AdjacentLocations(Point position)
        {
            var list = new List<Point>();

            // x,y
            // x-1,y-1
            // x-1,y
            // x-1,y+1
            // x,y-1
            // x,y nereikia
            // x+1,y-1
            // x+1,y
            // x+y,y+1

            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                var x = position.X + xOffset;
                if (x >= 0 && x < width)
                {
                    for (int yOffset = -1; yOffset <= 1; yOffset++)
                    {
                        var y = position.Y + yOffset;
                        if (y >= 0 && y < height)
                        {
                            if (xOffset != 0 || yOffset != 0)
                                list.Add(new Point(x, y));
                        }
                    }
                }

            }
            return RandomaizeList(list);
        }


        public List<Point> GetFreeAdjacentLocations(Point position)
        {
            var list = AdjacentLocations(position);
            var free = new List<Point>();
            foreach (var point in list)
            {
                if (GetActorAt(point) == null)
                {
                    free.Add(point);
                }
            }
            return free;
        }

        //public List<Point> GetFreeAdjacentLocations(Point position)
        //{
        //    var list = GetFreeAdjacentLocations(positions);
        //    var free = new List<Point>();
        //    foreach (var point in list)
        //    {
        //        if (GetActorAt(point) == null)
        //        {
        //            free.Add(point);
        //        }
        //    }
        //    return free;
        //}
        private List<Point> RandomaizeList(List<Point> list)
        {
            var random = new Random();
            var newList = new List<Point>();
            while (list.Count > 0)
            {
                var i = random.Next(list.Count);
                newList.Add(list[i]);
                list.RemoveAt(i);
            }
            return newList;
        }
    }
}
    
