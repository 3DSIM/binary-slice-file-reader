using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinarySliceFileReader.ScanFile
{
    public class ScanLineBlock
    {
        public ScanLineBlock()
        {
            this.ScanLines = new List<ScanLine>();
        }

        public List<ScanLine> ScanLines;

        public int ParameterSetId;

        public float RotationAngle;

        public int ScanAreaId;
    }
}
