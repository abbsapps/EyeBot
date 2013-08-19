using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NeuronVisionTry.VisionComponents;

namespace NeuronVisionTry
{
    public static class GlobalLayersKnowledge
    {
        public static List<Receptor> Receptors;
        public static List<LaplaceFilter> LaplaceFilters; 
        public static List<MacroSector> MacroSectors;
        public static List<MicroSector> MicroSectors;
        public static List<DirectionNode> DirectionNodes; 
        public static EyeBot Eye;
        public static EyeMuscle Muscle;
        public static MacroSectorPlate MacroPlate;
        public static MicroSectorPlate MicroPlate;
        public static DirectionPulsePlate DirectionPulsePlate;
        public static DirectionRouter DirectionRouter;

        public static Bitmap Environment;
        public static Bitmap Perception;

        public static Random Randomer;

        public static int Counter;
    }
}
