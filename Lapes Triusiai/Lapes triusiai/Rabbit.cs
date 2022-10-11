using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapes_triusiai
{
    internal class Rabbit : IActor
    {
        private static readonly int BREDING_AGE = 5;
        private static readonly int MAX_AGE = 40;
        private static readonly double BREEDING_PROBABILITY = 0.15;
        private static readonly int MAX_LITTER_SIZE = 4;
        private static readonly Random RANDOM = new Random();

        private bool alive = true;
        private Point location = new Point(-1, -1);
        private int age;
        private Field field;

        public bool IsAlive => alive;
        public Point Location => location;

        public Rabbit(Field field, Point location, bool randomAge = false)
        {
            // new Rabbit(field, location, true/false/kintamas)
            // new Rabbit(field, location)
            this.field = field;
            SetLocation(location);
            if (randomAge)
            {
                age = RANDOM.Next(MAX_AGE);
            }
        }

        private void SetLocation(Point newLocation)
        {
            if (location.X > -1)
            {
                field.Clear(location);
            }
            location = newLocation;
            field.Place(this);
        }

        public void SetDead()
        {
            alive = false;
            if (location.X > -1)
            {
                field.Clear(location);
                field = null;
                location = new Point(-1, -1);
            }
        }

        private void IncrementAge()
        {
            age++;
            if (age >= MAX_AGE)
            {
                SetDead();
            }
        }

        private void GiveBirth(List<IActor> newRabbits)
        {
            if (age >= BREDING_AGE && RANDOM.NextDouble() < BREEDING_PROBABILITY)
            {
                var newRabbitsCount = RANDOM.Next(MAX_LITTER_SIZE) + 1;
                var freeSpace = field.GetFreeAdjacentLocations(location);
                while (freeSpace.Count > 0 && newRabbitsCount > 0)
                {
                    var rabbit = new Rabbit(field, freeSpace[0]);
                    freeSpace.RemoveAt(0);
                    newRabbitsCount--;
                    newRabbits.Add(rabbit);
                }
            }
        }

        public void Act(List<IActor> newActors)
        {
            IncrementAge();
            if (alive)
            {
                GiveBirth(newActors);
                var freeLocations = field.GetFreeAdjacentLocations(location);
                if (freeLocations.Count > 0)
                {
                    SetLocation(freeLocations[0]);
                }
                else
                {
                    SetDead();
                }
            }
        }
    }
}