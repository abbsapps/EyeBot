using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronVisionTry.VisionComponents
{
    public class DirectionRouter
    {
        public int XNormal;
        public int YNormal;
        public List<DirectionNode> DirectionNodes;

        public DirectionRouter(List<DirectionNode> directionNodes)
        {
            DirectionNodes = directionNodes;
        }

        public void RouteDirection(int retinaLocationX, int retinaLocationY, double varianceCharge)
        {
            var direction = Math.Atan2(retinaLocationY - YNormal, retinaLocationX - XNormal); //should be in radians
            var distance =
                Math.Sqrt(((retinaLocationX - XNormal)*(retinaLocationX - XNormal)) +
                           ((retinaLocationY - YNormal)*(retinaLocationY - YNormal)));
            var pointLandedOnX = Math.Cos(direction)*distance;
            var pointLandedOnY = Math.Sin(direction)*distance;
            var directionsQualifiedForList = new List<int>();
            if (distance < 30)
            {
                for (int i = 0; i < GlobalLayersKnowledge.DirectionNodes.Count; i++)
                {
                    var xCos = Math.Cos((Math.PI / 180) * DirectionNodes[i].Direction);
                    var ySin = Math.Sin((Math.PI / 180) * DirectionNodes[i].Direction);

                    if (pointLandedOnX < (xCos * distance + DirectionNodes[i].Width)
                        && pointLandedOnX > (xCos * distance - DirectionNodes[i].Width)
                        && pointLandedOnY < (ySin * distance + DirectionNodes[i].Width)
                        && pointLandedOnY > (ySin * distance - DirectionNodes[i].Width)

                        && (Math.Abs(pointLandedOnX) > 2 || Math.Abs(pointLandedOnY) > 2))
                    {
                        directionsQualifiedForList.Add(i);
                    }
                }
                foreach (var i in directionsQualifiedForList)
                {
                    DirectionNodes[i].Charge += varianceCharge;
                }
            }
            var stopHere = 1;
        }
    }
}
