using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinarySliceFileReader.ScanFile
{
    public class Contour
    {
        public Contour()
        {
            this.Points = new List<Point>();
        }

        public List<Point> Points { get; set; }

        public int Type { get; set; }
    }
}
