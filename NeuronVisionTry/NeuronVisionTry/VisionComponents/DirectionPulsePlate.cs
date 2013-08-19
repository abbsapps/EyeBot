using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public class DirectionPulsePlate
    {
        public DirectionPulsePlate(int thresholdBase, int thresholdSpike, int thresholdDecay) //all arbis
        {
            ThresholdBase = thresholdBase;
            Threshold = thresholdBase;
            ThresholdSpike = thresholdSpike;
            ThresholdDecay = thresholdDecay;
        }

        public double ThresholdBase;
        public double Threshold;
        public double ThresholdSpike;
        public double ThresholdDecay;

        public void NewTurn()
        {
            if (Threshold > ThresholdBase)
            {
                Threshold -= ThresholdDecay;
                if (Threshold < ThresholdBase)
                {
                    Threshold = ThresholdBase;
                }
            }
        }

        public void Execute(LaplaceFilter pulseFilter, double direction)
        {
            Threshold += ThresholdSpike;
            var newX = pulseFilter.RetinalPosition.X + (Math.Cos((Math.PI / 180) * direction) * 25);
            var newY = pulseFilter.RetinalPosition.Y + (Math.Sin((Math.PI / 180) * direction) * 25);
            GlobalLayersKnowledge.Muscle.MoveEye((int)newX, (int)newY);
        }
    }
}
