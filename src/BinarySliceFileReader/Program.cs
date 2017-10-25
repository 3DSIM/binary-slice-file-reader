using BinarySliceFileReader.ScanFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BinarySliceFileReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string usageText = "dotnet run <scanfile.bin> <scanFileSummary.txt>";

            if (args.Length == 0 || args.Length == 1 || args.Length > 2)
            {
                Console.WriteLine(usageText);
                Environment.Exit(0);
            }

            // there are 2 args
            string sourceFile = args[0];
            string summaryFile = args[1];

            var scanFile = BinaryScanFile.Read(sourceFile);
            OutputSummary(scanFile, summaryFile);
        }

        private static void OutputSummary(ScanFile.ScanFile scanFile, string summaryFileName)
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            StreamWriter writer = new StreamWriter(File.Open(summaryFileName, FileMode.Create));

            writer.WriteLine(scanFile.Version);
            writer.WriteLine($"Layer: {scanFile.Layer}");
            writer.WriteLine();

            writer.WriteLine("Parameter Sets");
            writer.WriteLine("ID\tType\tLaser Power\tLaser Speed");

            foreach (ParameterSet parameterSet in scanFile.ParameterSets)
            {
                writer.WriteLine($"{parameterSet.Id}\t{parameterSet.Type}\t{parameterSet.LaserPower}\t{parameterSet.LaserSpeed}");
            }

            writer.WriteLine();

            writer.WriteLine("Contours");
            writer.WriteLine("Count\tType\tx1, y1, x2, y2 ...");

            foreach (Contour contour in scanFile.Contours)
            {
                string line = $"{contour.Points.Count}\t{contour.Type}\t";
                foreach (Point point in contour.Points)
                {
                    line += $"{point.X}\t{point.Y}\t";
                }
                writer.WriteLine(line);
            }

            writer.WriteLine();

            foreach (ScanLineBlock block in scanFile.ScanLineBlocks)
            {
                writer.WriteLine(string.Empty);
                writer.WriteLine($"Scan Area Index: {block.ScanAreaId}");
                writer.WriteLine($"Rotation Angle: {block.RotationAngle}");
                writer.WriteLine($"Parameter Set: {block.ParameterSetId}");
                writer.WriteLine("Scan Lines");
                writer.WriteLine("x1\ty1\tx2\ty2");

                foreach (ScanLine scanLine in block.ScanLines)
                {
                    UpdateMinMax(scanLine, ref minX, ref minY, ref maxX, ref maxY);
                    writer.WriteLine($"{scanLine.X1}\t{scanLine.Y1}\t{scanLine.X2}\t{scanLine.Y2}");
                }

                writer.WriteLine();

            }

            writer.WriteLine($"Min x,y\tMax x,y\n{minX},{minY}\t{maxX},{maxY}\n");

            writer.Dispose();
        }

        public static void UpdateMinMax(ScanLine scanLine, ref float minX, ref float minY, ref float maxX, ref float maxY)
        {
            var xVals = new float[] { scanLine.X1, scanLine.X2 };
            var yVals = new float[] { scanLine.Y1, scanLine.Y2 };

            foreach (float val in xVals)
            {
                minX = (val < minX) ? val : minX;
                maxX = (val > maxX) ? val : maxX;
            }

            foreach (float val in yVals)
            {
                minY = (val < minY) ? val : minY;
                maxY = (val > maxY) ? val : maxY;
            }
        }
    }
}