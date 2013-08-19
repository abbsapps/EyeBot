using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public interface ISector
    {
        void NewTurn();
        void AttemptFire();
        void Fire();
        void AbsorbCharge(int inputCharge);
    }
}
