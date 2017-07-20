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
            this.ScanLines = new List<ScanLine>();
            this.ParameterSets = new List<ParameterSet>();
        }

        public string Version { get; set; }

        public int Layer { get; set; }

        public List<Contour> Contours;

        public List<ScanLine> ScanLines;

        public List<ParameterSet> ParameterSets;
    }
}
