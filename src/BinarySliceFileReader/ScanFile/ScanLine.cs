using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinarySliceFileReader.ScanFile
{
    public class ScanLine
    {
        public ScanLine(float x1, float y1, float x2, float y2)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
        }

        public float X1 { get; set; }

        public float Y1 { get; set; }

        public float X2 { get; set; }

        public float Y2 { get; set; }
    }
}
