using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public class DirectionNode
    {
        public double Direction;
        public double Charge;
        public DirectionPulsePlate PulsePlate;
        public double Width;
        public double DecayRate;

        public DirectionNode(double direction, DirectionPulsePlate pulsePlate, double width)
        {
            Direction = direction;
            PulsePlate = pulsePlate;
            Charge = 0.0;
            Width = width;
            DecayRate = 5;
        }

        public void NewTurn()
        {
            if (Charge > 0)
            {
                Charge -= DecayRate;
                if (Charge < 0)
                {
                    Charge = 0;
                }
            }
        }
    }
}
