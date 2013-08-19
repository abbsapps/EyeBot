using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public class MacroSector: ISector
    {
        public MacroSector(int yPosition, int xPosition)
        {
            Location = new XYLocation(yPosition, xPosition);
            Receptors = new List<Receptor>();
            LaplaceFilters = new List<LaplaceFilter>();
            DecayRate = .2; //arbi
            Muscle = GlobalLayersKnowledge.Muscle;
            SectorPlate = GlobalLayersKnowledge.MacroPlate;
        }

        public XYLocation Location;
        public double Charge;
        public List<Receptor> Receptors;
        public double DecayRate;
        public EyeMuscle Muscle;
        public MacroSectorPlate SectorPlate;
        public List<LaplaceFilter> LaplaceFilters;

        public void NewTurn()
        {
            //if (Charge > 0)
            //{
                //Charge = Charge - ((DecayRate * Charge) + 1); //arbi //this works in manufactured images, but causes problems in complex ones.  Does the plate decay make up for this? 
            //}
            if (Charge > SectorPlate.Threshold)
            {
                Fire();
            }
        }

        public void Fire()
        {
            SectorPlate.Execute();
            Muscle.MoveEye(Location.X, Location.Y);
        }

        public void AbsorbCharge(int inputCharge)
        {
            Charge += inputCharge;
        }
    }
}
