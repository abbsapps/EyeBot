using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public class EyeMuscle
    {
        public EyeMuscle(EyeBot eye)
        {
            Eye = eye;
        }

        public EyeBot Eye;

        public void MoveEye(int xMove, int yMove)
        {
            Eye.changePosition(xMove, yMove);
            foreach (MacroSector macroSector in GlobalLayersKnowledge.MacroSectors)
            {
                //haxor
                macroSector.Charge = 0.0;
                //end haxor
            }
            foreach (MicroSector microSector in GlobalLayersKnowledge.MicroSectors)
            {
                //haxor
                microSector.Charge = 0.0;
                //end haxor
            }
        }
    }
}
