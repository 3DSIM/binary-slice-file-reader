using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinarySliceFileReader.ScanFile
{
    public class ScanFile
    {
        public ScanFile()
        {
            this.Contours = new List<Contour>();
            this.ScanLineBlocks = new List<ScanLineBlock>();
            this.ParameterSets = new List<ParameterSet>();
        }

        public string Version { get; set; }

        public int Layer { get; set; }

        public List<Contour> Contours;

        public List<ScanLineBlock> ScanLineBlocks;

        public List<ParameterSet> ParameterSets;
    }
}
