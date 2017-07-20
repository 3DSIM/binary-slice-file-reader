using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinarySliceFileReader.ScanFile
{
    public class ParameterSet
    {
        public ParameterSet(int id, int type, int laserPower, float laserSpeed)
        {
            this.Id = id;
            this.Type = type;
            this.LaserPower = laserPower;
            this.LaserSpeed = laserSpeed;
        }

        public int Id { get; set; }

        public int Type { get; set; }

        public int LaserPower { get; set; }

        public float LaserSpeed { get; set; }
    }
}
