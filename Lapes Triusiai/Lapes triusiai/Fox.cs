using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapes_triusiai
{
    internal class Fox : IActor
    {
        private static readonly int BREDING_AGE = 10;
        private static readonly int MAX_AGE = 150;
        private static readonly double BREEDING_PROBABILITY = 0.35;
        private static readonly int MAX_LITTER_SIZE = 5;
        private static readonly int RABBIT_FOOD_VALUE = 7;

        private static readonly Random RANDOM = new Random();

        private bool alive = true;
        private Point location = new Point(-1, -1);
        private int age;
        private Field field;
        private int foodLevel;

        public bool IsAlive => alive;
        public Point Location => location;

        public Fox(Field field, Point location, bool randomAge = false)
        {
            // new Rabbit(field, location, true/false/kintamas)
            // new Rabbit(field, location)
            this.field = field;
            foodLevel = RABBIT_FOOD_VALUE;
            SetLocation(location);
            if (randomAge)
            {
                age = RANDOM.Next(MAX_AGE);
                foodLevel = RANDOM.Next(RABBIT_FOOD_VALUE);
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

        private void IncrementHunger()
        {
            foodLevel--;
            if (foodLevel <= 0)
            {
                SetDead();
            }
        }

        private void GiveBirth(List<IActor> newFoxes)
        {
            if (age >= BREDING_AGE && RANDOM.NextDouble() < BREEDING_PROBABILITY)
            {
                var newFoxesCount = RANDOM.Next(MAX_LITTER_SIZE) + 1;
                var freeSpace = field.GetFreeAdjacentLocations(location);
                while (freeSpace.Count > 0 && newFoxesCount > 0)
                {
                    var fox = new Fox(field, freeSpace[0]);
                    freeSpace.RemoveAt(0);
                    newFoxesCount--;
                    newFoxes.Add(fox);
                }
            }
        }

        Point FindFood()
        {
            var list = field.AdjacentLocations(location);
            foreach (var point in list)
            {
                var actor = field.GetActorAt(point);
                var rabbit = actor as Rabbit;
                if (rabbit !=null && rabbit.IsAlive)
                {
                    rabbit.SetDead();
                    foodLevel = RABBIT_FOOD_VALUE;
                    return point;
                }
            }
            return new Point(-1, -1);
        }

        public void Act(List<IActor> newActors)
        {
            IncrementAge();
            IncrementHunger();

            if (alive)
            {
                GiveBirth(newActors);
                var huntPoint = FindFood();
                if (huntPoint.X > -1)
                {
                    SetLocation(huntPoint);
                }
                else
                {
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
}
