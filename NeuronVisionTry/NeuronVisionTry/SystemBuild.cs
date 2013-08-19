using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using NeuronVisionTry.VisionComponents;
using System.Drawing;
using Gif.Components;
using MoreLinq;

namespace NeuronVisionTry
{
    class Program
    {
        static void Main(string[] args)
        {
            Image environmentImage =
                    Image.FromFile(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                          "visionPictures/Example.png"));
            GlobalLayersKnowledge.Environment = new Bitmap(environmentImage);
            GlobalLayersKnowledge.Randomer = new Random();
            GlobalLayersKnowledge.Counter = 0;

            GlobalLayersKnowledge.MicroPlate = new MicroSectorPlate(thresholdBase: 100, thresholdSpike: 25, thresholdDecay: 4);
            GlobalLayersKnowledge.MacroPlate = new MacroSectorPlate(thresholdBase: 300, thresholdSpike: 100, thresholdDecay: 6);
            GlobalLayersKnowledge.DirectionPulsePlate = new DirectionPulsePlate(thresholdBase: 100, thresholdSpike: 25, thresholdDecay: 5);
            GlobalLayersKnowledge.DirectionNodes = BuildDirectionNodes(nodeCount: 200, activationZoneWidth: 2.0);
            GlobalLayersKnowledge.DirectionRouter = new DirectionRouter(directionNodes: GlobalLayersKnowledge.DirectionNodes);

            GlobalLayersKnowledge.Eye = new EyeBot(receptorFieldWidth: 400, receptorFieldHeight: 400);
            GlobalLayersKnowledge.Muscle = new EyeMuscle(GlobalLayersKnowledge.Eye);









            //testing below here
            for (int j = 0; j < 81; j++)
            {
                Bitmap perception = new Bitmap(GlobalLayersKnowledge.Eye.ReceptorFieldWidth, GlobalLayersKnowledge.Eye.ReceptorFieldHeight);
                GlobalLayersKnowledge.Perception = perception;
                Bitmap environment = new Bitmap(environmentImage);
                GlobalLayersKnowledge.Eye.newTurn(environment, perception);
                GlobalLayersKnowledge.Counter++;

                //below for testing purposes
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        perception.SetPixel((int)(perception.Width / 2) + x, (int)(perception.Height / 2) + y, Color.HotPink);
                        if (GlobalLayersKnowledge.Eye.Location.X > 1 && GlobalLayersKnowledge.Eye.Location.X < (environment.Width - 1) && GlobalLayersKnowledge.Eye.Location.Y > 1 && GlobalLayersKnowledge.Eye.Location.Y < (environment.Height - 1))
                        {
                            environment.SetPixel(GlobalLayersKnowledge.Eye.Location.X + x, GlobalLayersKnowledge.Eye.Location.Y + y, Color.HotPink);
                        }
                        
                    }
                }

                if (j % 10 == 0 || (j < 21))
                {
                    string fileName = "visionPictures/neuronStuff/perception" + j.ToString() + ".png";
                    var filePath = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        fileName);
                    perception.Save(filePath);

                    string environmentFileName = "visionPictures/neuronStuff/perception" + (j+1).ToString() + "e.png";
                    var environmentFilePath = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        environmentFileName);
                    environment.Save(environmentFilePath);
                    //var whatevs = GlobalLayersKnowledge.LaplaceFilters.Any(x => x.VarianceCharge > 50);
                }
                //end test area
            }  
        }





        public static List<DirectionNode> BuildDirectionNodes(int nodeCount, double activationZoneWidth)
        {
            var directionNodes = new List<DirectionNode>();

            for (double i = 0; i < 360; i += (360.0 / nodeCount))
            {
                directionNodes.Add(new DirectionNode(direction: i, pulsePlate: GlobalLayersKnowledge.DirectionPulsePlate, width: activationZoneWidth));
            }

            return directionNodes;
        }
    }
}
