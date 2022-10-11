using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapes_triusiai
{
    internal class Simuliator
    {
        private static readonly double FOX_CREATION_PROBABILITY = 0.02;
        private static readonly double RABBIT_CREATION_PROBABILITY = 0.06;

        private Field field;
        private List<IActor> actors;

        public int Step { get; private set; }

        public event Action<Field> StepDone;

        public Simuliator(int width, int height)
        {
            field = new Field(width, height);
            actors = new List<IActor>();

            Populate();
        }

        public void RunOneStep()
        {
            Step++;
            var newActors = new List<IActor>();
            foreach (var actor in actors)
            {
                actor.Act(newActors);
            }
            actors.AddRange(newActors);
            actors.RemoveAll(a => !a.IsAlive);

            //for (int i = 0; i < actors.Count; i++)
            //{
            //    if (!actors[i].IsAlive)
            //    {
            //        actors.RemoveAt(i);
            //        i--;
            //    }    
            // }
            StepDone?.Invoke(field);

        }

        public void Reset()
        {
            Step = 0;
            actors.Clear();
            Populate();
        }

        private void Populate()
        {
            var rand = new Random();
            field.Clear();
            for (int x = 0; x < field.Witdth; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    if (rand.NextDouble() < FOX_CREATION_PROBABILITY)
                    {
                        actors.Add(item: new Fox(field, location: new Point(x, y), randomAge: true));
                    }
                    else if (rand.NextDouble() < RABBIT_CREATION_PROBABILITY)
                    {
                        var rabbit = new Rabbit(field, location: new Point(x, y), randomAge: true);
                        actors.Add(rabbit);
                    }
                }
            }
        }
    }

}