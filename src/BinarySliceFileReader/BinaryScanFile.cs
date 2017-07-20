using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinarySliceFileReader.ScanFile;
using System.Text;
using System;

namespace BinarySliceFileReader
{
    public static class BinaryScanFile
    {
        public const int DATA_BLOCK_VERSION = 1;
        public const int DATA_BLOCK_LAYER = 2;
        public const int DATA_BLOCK_CONTOURS = 3;
        public const int DATA_BLOCK_PARAMETER_SETS = 4;
        public const int DATA_BLOCK_SCAN_LINES = 5;

        public static ScanFile.ScanFile Read(string fileName)
        {
            var scanFile = new ScanFile.ScanFile();

            BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                int blockType = reader.ReadInt32();
                int blockSize = reader.ReadInt32();
                byte[] data = reader.ReadBytes(blockSize);

                if (blockType == DATA_BLOCK_VERSION)
                {
                    ReadVersionBlock(scanFile, data);
                }
                else if (blockType == DATA_BLOCK_LAYER)
                {
                    ReadLayerBlock(scanFile, data);
                }
                else if (blockType == DATA_BLOCK_CONTOURS)
                {
                    ReadContourBlock(scanFile, data);
                }
                else if (blockType == DATA_BLOCK_PARAMETER_SETS)
                {
                    ReadParameterSetBlock(scanFile, data);
                }
                else if (blockType == DATA_BLOCK_SCAN_LINES)
                {
                    ReadScanLineBlock(scanFile, data);
                }
                else
                {
                    throw new System.Exception($"Unknown block type: {blockType}");
                }
            }

            reader.Dispose();
            return scanFile;
        }

        private static void ReadVersionBlock(ScanFile.ScanFile scanFile, byte[] data)
        {
            scanFile.Version = Encoding.ASCII.GetString(data, 0, data.Length);
        }

        private static void ReadLayerBlock(ScanFile.ScanFile scanFile, byte[] data)
        {
            scanFile.Layer = BitConverter.ToInt32(data, 0);
        }

        private static void ReadContourBlock(ScanFile.ScanFile scanFile, byte[] data)
        {
            int offset = 0;
            int contourCount = BitConverter.ToInt32(data, offset);
            offset += sizeof(int);

            for (int i = 0; i < contourCount; i++)
            {
                var contour = new Contour();

                contour.Type = BitConverter.ToInt32(data, offset);
                offset += sizeof(int);

                int pointCount = BitConverter.ToInt32(data, offset);
                offset += sizeof(int);

                for (int j = 0; j < pointCount; j++)
                {
                    float x = BitConverter.ToSingle(data, offset);
                    offset += sizeof(float);
                    float y = BitConverter.ToSingle(data, offset);
                    offset += sizeof(float);

                    contour.Points.Add(new Point(x, y));
                }

                scanFile.Contours.Add(contour);
            }
        }

        private static void ReadParameterSetBlock(ScanFile.ScanFile scanFile, byte[] data)
        {
            int offset = 0;
            int parameterSetCount = BitConverter.ToInt32(data, offset);
            offset += sizeof(int);

            for (int i = 0; i < parameterSetCount; i++)
            {
                // get fields 
                int id = BitConverter.ToInt32(data, offset);
                offset += sizeof(int);
                int type = BitConverter.ToInt32(data, offset);
                offset += sizeof(int);
                int laserPower = BitConverter.ToInt32(data, offset);
                offset += sizeof(int);
                float laserSpeed = BitConverter.ToInt32(data, offset);
                offset += sizeof(float);

                scanFile.ParameterSets.Add(new ParameterSet(id, type, laserPower, laserSpeed));
            }
        }

        private static void ReadScanLineBlock(ScanFile.ScanFile scanFile, byte[] data)
        {
            int offset = 0;
            int scanLineCount = BitConverter.ToInt32(data, offset);
            offset += sizeof(int);

            for (int i = 0; i < scanLineCount; i++)
            {
                float x1 = BitConverter.ToSingle(data, offset);
                offset += sizeof(float);
                float y1 = BitConverter.ToSingle(data, offset);
                offset += sizeof(float);
                float x2 = BitConverter.ToSingle(data, offset);
                offset += sizeof(float);
                float y2 = BitConverter.ToSingle(data, offset);
                offset += sizeof(float);

                scanFile.ScanLines.Add(new ScanLine(x1, y1, x2, y2));
            }
        }
    }
}
