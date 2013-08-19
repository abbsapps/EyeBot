using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public class MicroSectorPlate
    {
        public MicroSectorPlate(int thresholdBase, int thresholdSpike, int thresholdDecay) //all arbis
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

        public void Execute()
        {
            Threshold += ThresholdSpike;
        }
    }
}
