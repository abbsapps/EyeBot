using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public class LaplaceFilter
    {
        public LaplaceFilter(int yPosition, int xPosition)
        {
            RetinalPosition = new XYLocation(yPosition, xPosition);
            Value = 0.0;
            VarianceCharge = 0.0;
            PulseCharge = 0; //for now; ensures only one edge will be found
            DirectionRouter = GlobalLayersKnowledge.DirectionRouter;
        }

        public void AttemptFire()
        {
            if (VarianceCharge > 3)
            {
                Fire();
            }
        }

        public void Fire()
        {
            Sector.AbsorbCharge(3); 

            VarianceCharge -= 3;
            GlobalLayersKnowledge.Perception.SetPixel(RetinalPosition.X + (int)(GlobalLayersKnowledge.Perception.Width / 2.0),
                                RetinalPosition.Y + (int)(GlobalLayersKnowledge.Perception.Height / 2.0), Color.Yellow);

            if (PulseCharge > PulsePlate.Threshold)
            {
                GlobalLayersKnowledge.Eye.ExecutePulse(this, RetinalPosition.X, RetinalPosition.Y);
            }
        }

        public void DirectionFire()
        {
            if (VarianceCharge > 0.0)
            {
                DirectionRouter.RouteDirection(RetinalPosition.X, RetinalPosition.Y, VarianceCharge);
            }
        }

        public XYLocation RetinalPosition;
        public double Value;
        public double VarianceCharge;
        public ISector Sector;
        public DirectionPulsePlate PulsePlate;
        public double PulseCharge;
        public DirectionRouter DirectionRouter;
    }
}
