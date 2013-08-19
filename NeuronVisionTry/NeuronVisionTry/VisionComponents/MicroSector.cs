using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public class MicroSector: ISector
    {
        public MicroSector(int yPosition, int xPosition)
        {
            Location = new XYLocation(yPosition, xPosition);
            Receptors = new List<Receptor>();
            LaplaceFilters = new List<LaplaceFilter>();
            DecayRate = .2; //arbi
            //Threshold = 35; //arbi
            Muscle = GlobalLayersKnowledge.Muscle;
            SectorPlate = GlobalLayersKnowledge.MicroPlate;
        }

        public XYLocation Location;
        public double Charge;
        public List<Receptor> Receptors;
        public List<LaplaceFilter> LaplaceFilters; 
        public double DecayRate;
        public double Threshold;
        public EyeMuscle Muscle;
        public MicroSectorPlate SectorPlate;

        public void NewTurn()
        {
            //if (Charge > 0)
            //{
            //    Charge = Charge - ((DecayRate * Charge) + 1); //this works in manufactured images, but causes problems in complex ones.  Does the plate decay make up for this? 
            //}
            AttemptFire();
        }

        public void AttemptFire()
        {
            if (Charge > SectorPlate.Threshold)
            {
                Fire();
                SectorPlate.Execute();
            }
        }

        public void Fire()
        {
            Muscle.MoveEye(Location.X, Location.Y);
        }

        public void AbsorbCharge(int inputCharge)
        {
            Charge += inputCharge * 10;
        }
    }
}
