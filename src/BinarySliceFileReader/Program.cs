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

            writer.WriteLine($"Parameter Sets [{scanFile.ParameterSets.Count}]");
            writer.WriteLine("ID\tType\tLaser Power\tLaser Speed");

            foreach (ParameterSet parameterSet in scanFile.ParameterSets)
            {
                writer.WriteLine($"{parameterSet.Id}\t{parameterSet.Type}\t{parameterSet.LaserPower}\t{parameterSet.LaserSpeed}");
            }

            writer.WriteLine();

            writer.WriteLine($"Contours [{scanFile.Contours.Count}]");
            writer.WriteLine("Type\tPoint Count\tx1, y1, x2, y2 ...");

            foreach (Contour contour in scanFile.Contours)
            {
                string line = $"{contour.Type}\t{contour.Points.Count}\t";
                foreach (Point point in contour.Points)
                {
                    line += $"{point.X}\t{point.Y}\t";
                    UpdateMinMax(point, ref minX, ref minY, ref maxX, ref maxY);
                }
                writer.WriteLine(line);
            }

            writer.WriteLine();
            writer.WriteLine($"Scan line blocks [{scanFile.ScanLineBlocks.Count}]");

            foreach (ScanLineBlock block in scanFile.ScanLineBlocks)
            {
                writer.WriteLine($"Scan Area Index: {block.ScanAreaId}");
                writer.WriteLine($"Rotation Angle: {block.RotationAngle}");
                writer.WriteLine($"Parameter Set: {block.ParameterSetId}");
                writer.WriteLine("Scan Lines");
                writer.WriteLine("x1\ty1\tx2\ty2");

                foreach (ScanLine scanLine in block.ScanLines)
                {
                    UpdateMinMax(new Point(scanLine.X1, scanLine.Y1), ref minX, ref minY, ref maxX, ref maxY);
                    UpdateMinMax(new Point(scanLine.X2, scanLine.Y2), ref minX, ref minY, ref maxX, ref maxY);
                    writer.WriteLine($"{scanLine.X1}\t{scanLine.Y1}\t{scanLine.X2}\t{scanLine.Y2}");
                }

                writer.WriteLine();

            }

            writer.WriteLine($"Min x,y\tMax x,y\n{minX},{minY}\t{maxX},{maxY}\n");

            writer.Dispose();
        }

        public static void UpdateMinMax(Point point, ref float minX, ref float minY, ref float maxX, ref float maxY)
        {
            minX = (point.X < minX) ? point.X : minX;
            maxX = (point.X > maxX) ? point.X : maxX;
            minY = (point.Y < minY) ? point.Y : minY;
            maxY = (point.Y > maxY) ? point.Y : maxY;
        }
    }
}