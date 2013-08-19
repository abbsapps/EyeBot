using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public class Receptor
    {
        public Receptor(int yPosition, int xPosition, EyeBot parent)
        {
            RetinalPosition = new XYLocation(yPosition, xPosition);
            Value = 0.0;
            Parent = parent;

            var distance = Math.Sqrt((RetinalPosition.X*RetinalPosition.X) + (RetinalPosition.Y*RetinalPosition.Y));
            var distanceRatio = distance/
                                Math.Sqrt(((parent.ReceptorFieldWidth/2.0)*(parent.ReceptorFieldWidth/2.0)) +
                                          ((parent.ReceptorFieldHeight/2.0)*(parent.ReceptorFieldHeight/2.0)));
            FireChance = (float)Math.Pow(distanceRatio, .5);
            AdjacentFilters = new List<LaplaceFilter>();
        }

        public List<LaplaceFilter> AdjacentFilters;
        public LaplaceFilter SelfFilter;
        public XYLocation RetinalPosition;
        public float FireChance;
        public EyeBot Parent;
        public double Value;

        public void AttemptFireToLaplaceFilters()
        {
            if (GlobalLayersKnowledge.Randomer.NextDouble() > FireChance && 
                !((RetinalPosition.X + Parent.Location.X) >= GlobalLayersKnowledge.Environment.Width ||
                (RetinalPosition.X + Parent.Location.X) < 0 ||
                (RetinalPosition.Y + Parent.Location.Y) >= GlobalLayersKnowledge.Environment.Height ||
                (RetinalPosition.Y + Parent.Location.Y) < 0))
            {
                FireToLaplaceFilters();
            }
        }

        public void FireToLaplaceFilters()
        {
            Value = GlobalLayersKnowledge.Environment.GetPixel(RetinalPosition.X + Parent.Location.X,
                                             RetinalPosition.Y + Parent.Location.Y).GetBrightness();
            SelfFilter.Value = Value;
            foreach (var adjacentFilter in AdjacentFilters)
            {
                adjacentFilter.VarianceCharge += Math.Abs(Value - adjacentFilter.Value);
                adjacentFilter.PulseCharge += Math.Abs(Value - adjacentFilter.Value);
            }
        }
    }
}
