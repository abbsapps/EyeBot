using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Linq;

namespace NeuronVisionTry.VisionComponents
{
    public class EyeBot
    {
        public EyeBot(int receptorFieldWidth, int receptorFieldHeight)//BREAKS WHEN WIDTH AND HEIGHT ARE NOT SET TO IDENTICAL VALUES
        {
            GlobalLayersKnowledge.Muscle = new EyeMuscle(this);

            ReceptorField = new List<Receptor>();
            LaplaceFilterField = new List<LaplaceFilter>();
            Location = new XYLocation((int)(GlobalLayersKnowledge.Randomer.NextDouble() * GlobalLayersKnowledge.Environment.Width), (int)(GlobalLayersKnowledge.Randomer.NextDouble() * GlobalLayersKnowledge.Environment.Height));
            ReceptorFieldWidth = receptorFieldWidth; 
            ReceptorFieldHeight = receptorFieldHeight;

            List<List<Receptor>> receptorLocationHolder= new List<List<Receptor>>();
            List<List<LaplaceFilter>> laplaceFilterLocationHolder = new List<List<LaplaceFilter>>();
            for (int i = (int)(-1 * (.5 * ReceptorFieldWidth)); i < (int)(.5 * ReceptorFieldWidth); i++)
            {
                receptorLocationHolder.Add(new List<Receptor>());
                laplaceFilterLocationHolder.Add(new List<LaplaceFilter>());
                for (int j = (int)(-1 * (.5 * ReceptorFieldHeight)); j < (int)(.5 * ReceptorFieldHeight); j++)
                {
                    var newReceptor = new Receptor(i, j, this);
                    ReceptorField.Add(newReceptor);
                    receptorLocationHolder[i + (int)(.5 * ReceptorFieldWidth)].Add(newReceptor);

                    var newLaplaceFilter = new LaplaceFilter(i, j);
                    LaplaceFilterField.Add(newLaplaceFilter);
                    laplaceFilterLocationHolder[i + (int)(.5 * ReceptorFieldWidth)].Add(newLaplaceFilter);

                }
            }

            foreach (Receptor receptor in ReceptorField)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            receptor.SelfFilter = 
                                laplaceFilterLocationHolder[receptor.RetinalPosition.Y + (int)(.5 * ReceptorFieldWidth) + i][
                                    receptor.RetinalPosition.X + (int)(.5 * ReceptorFieldHeight) + j];
                        }
                        if (receptor.RetinalPosition.Y >= -(int)(.5 * ReceptorFieldHeight) + 1 && receptor.RetinalPosition.Y < (int)(.5 * ReceptorFieldHeight) - 1 && receptor.RetinalPosition.X >= -(int)(.5 * ReceptorFieldWidth) + 1 && receptor.RetinalPosition.X < (int)(.5 * ReceptorFieldWidth) - 1)
                        {

                            receptor.AdjacentFilters.Add(                                    
                                laplaceFilterLocationHolder[receptor.RetinalPosition.Y + (int)(.5 * ReceptorFieldWidth) + i][
                                    receptor.RetinalPosition.X + (int)(.5 * ReceptorFieldHeight) + j]);
                        }
                    }
                }
            }

            List<MacroSector> macroSectors = new List<MacroSector>();
            List<List<MacroSector>> macroSectorLocationHolder = new List<List<MacroSector>>();
            var counter = 0;
            for (int i = (int)(-1 * (.5 * ReceptorFieldWidth)); i < (int)(.5 * ReceptorFieldWidth); i = i + (int)(.1 * ReceptorFieldWidth))
            {
                macroSectorLocationHolder.Add(new List<MacroSector>());
                for (int j = (int)(-1 * (.5 * ReceptorFieldHeight)); j < (int)(.5 * ReceptorFieldHeight); j = j + (int)(.1 * ReceptorFieldHeight))
                {
                    var newMacroSector = new MacroSector(i + ((int)(.05 * ReceptorFieldHeight)), j + ((int)(.05 * ReceptorFieldWidth))); //haxorz
                    macroSectorLocationHolder[counter].Add(newMacroSector);
                    macroSectors.Add(newMacroSector);
                }
                counter++;
            }

            //micro version copy
            List<MicroSector> microSectors = new List<MicroSector>();
            List<List<MicroSector>> microSectorLocationHolder = new List<List<MicroSector>>();
            var microCounter = 0;
            for (int i = (int)(-1 * (.05 * ReceptorFieldWidth)); i < (int)(.05 * ReceptorFieldWidth); i = i + (int)(.01 * ReceptorFieldWidth))
            {
                microSectorLocationHolder.Add(new List<MicroSector>());
                for (int j = (int)(-1 * (.05 * ReceptorFieldHeight)); j < (int)(.05 * ReceptorFieldHeight); j = j + (int)(.01 * ReceptorFieldHeight))
                {
                    var newMicroSector = new MicroSector(i + ((int)(.005 * ReceptorFieldHeight)), j + ((int)(.005 * ReceptorFieldWidth))); //haxorz
                    microSectorLocationHolder[microCounter].Add(newMicroSector);
                    microSectors.Add(newMicroSector);
                }
                microCounter++;
            }
            //end copy

            foreach (LaplaceFilter laplaceFilter in LaplaceFilterField)
            {
                laplaceFilter.PulsePlate = GlobalLayersKnowledge.DirectionPulsePlate;

                if ((Math.Abs(laplaceFilter.RetinalPosition.X) < .05 * ReceptorFieldWidth) &&
                    (Math.Abs(laplaceFilter.RetinalPosition.Y) < .05 * ReceptorFieldHeight))
                {
                    var receptorXDivisor = (int)(.01 * ReceptorFieldWidth);
                    var receptorYDivisor = (int)(.01 * ReceptorFieldHeight);
                    var xIndex = (int)(((double)laplaceFilter.RetinalPosition.X / (double)receptorXDivisor) + 5);
                    var yIndex = (int)(((double)laplaceFilter.RetinalPosition.Y / (double)receptorYDivisor) + 5);
                    laplaceFilter.Sector = microSectorLocationHolder[yIndex][xIndex];
                    microSectorLocationHolder[yIndex][xIndex].LaplaceFilters.Add(laplaceFilter);
                }
                else
                {
                    var receptorXDivisor = (int)(.1 * ReceptorFieldWidth);
                    var receptorYDivisor = (int)(.1 * ReceptorFieldHeight);
                    var xIndex = (int)(((double)laplaceFilter.RetinalPosition.X / (double)receptorXDivisor) + 5);
                    var yIndex = (int)(((double)laplaceFilter.RetinalPosition.Y / (double)receptorYDivisor) + 5);
                    laplaceFilter.Sector = macroSectorLocationHolder[yIndex][xIndex];
                    macroSectorLocationHolder[yIndex][xIndex].LaplaceFilters.Add(laplaceFilter);
                }
            }
            GlobalLayersKnowledge.Receptors = ReceptorField;
            GlobalLayersKnowledge.LaplaceFilters = LaplaceFilterField;
            GlobalLayersKnowledge.MacroSectors = macroSectors;
            GlobalLayersKnowledge.MicroSectors = microSectors;
        }

        public XYLocation Location;
        public List<Receptor> ReceptorField;
        public List<LaplaceFilter> LaplaceFilterField; 
        public int ReceptorFieldWidth;
        public int ReceptorFieldHeight;

        public void newTurn(Bitmap environment, Bitmap perception)
        {

            foreach (Receptor receptor in ReceptorField)
            {
                receptor.AttemptFireToLaplaceFilters();
            }
            foreach (var laplaceFilter in LaplaceFilterField)
            {
                laplaceFilter.AttemptFire();
            }
            foreach (MacroSector macroSector in GlobalLayersKnowledge.MacroSectors)
            {
                macroSector.NewTurn();
            }
            foreach (MicroSector microSector in GlobalLayersKnowledge.MicroSectors)
            {
                microSector.NewTurn();
            }
            foreach (DirectionNode directionNode in GlobalLayersKnowledge.DirectionNodes)
            {
                directionNode.NewTurn();
            }

            GlobalLayersKnowledge.MicroPlate.NewTurn();
            GlobalLayersKnowledge.MacroPlate.NewTurn();
            GlobalLayersKnowledge.DirectionPulsePlate.NewTurn();
        }

        public void changePosition(int xChange, int yChange)
        {
            Location.X += xChange;
            Location.Y += yChange;
            foreach (var receptor in ReceptorField)
            {
                if (
                    !((receptor.RetinalPosition.X + receptor.Parent.Location.X) >=
                      GlobalLayersKnowledge.Environment.Width ||
                      (receptor.RetinalPosition.X + receptor.Parent.Location.X) < 0 ||
                      (receptor.RetinalPosition.Y + receptor.Parent.Location.Y) >=
                      GlobalLayersKnowledge.Environment.Height ||
                      (receptor.RetinalPosition.Y + receptor.Parent.Location.Y) < 0))
                {
                    receptor.Value =
                        GlobalLayersKnowledge.Environment.GetPixel(
                            receptor.RetinalPosition.X + receptor.Parent.Location.X,
                            receptor.RetinalPosition.Y + receptor.Parent.Location.Y).GetBrightness();
                }
                else
                {
                    receptor.Value = 0;
                }
                receptor.SelfFilter.Value = receptor.Value;
            }
        }

        public void ExecutePulse(LaplaceFilter pulseFilter, int xStart, int yStart)
        {
            GlobalLayersKnowledge.DirectionRouter.XNormal = xStart;
            GlobalLayersKnowledge.DirectionRouter.YNormal = yStart;

            foreach (var laplaceFilter in LaplaceFilterField)
            {
                laplaceFilter.DirectionFire();
            }



            //testing stuff past here
            var environmentCopyOne = new Bitmap(GlobalLayersKnowledge.Environment);
            if (GlobalLayersKnowledge.DirectionNodes.Max(x => x.Charge) > GlobalLayersKnowledge.DirectionPulsePlate.Threshold)
            {
                var highestNode =
                    GlobalLayersKnowledge.DirectionNodes.First(
                        x => (x.Charge == GlobalLayersKnowledge.DirectionNodes.Max(y => y.Charge)));

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        environmentCopyOne.SetPixel(Location.X + pulseFilter.RetinalPosition.X + x, Location.Y + pulseFilter.RetinalPosition.Y + y, Color.Crimson);
                        var endPointX = Location.X + pulseFilter.RetinalPosition.X + (Math.Cos((Math.PI / 180) * highestNode.Direction) * 25);
                        var endPointY = Location.Y + pulseFilter.RetinalPosition.Y + (Math.Sin((Math.PI / 180) * highestNode.Direction) * 25);
                        environmentCopyOne.SetPixel((int)endPointX + x, (int)endPointY + y, Color.Chartreuse);
                    }
                }

                string environmentFileName = "visionPictures/neuronStuff/EDGENODETRY" +
                             GlobalLayersKnowledge.Counter + ".png";
                var environmentFilePath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    environmentFileName);
                environmentCopyOne.Save(environmentFilePath);

                GlobalLayersKnowledge.DirectionPulsePlate.Execute(pulseFilter, highestNode.Direction);
            }
            //end testing zone
        }
    }
}
