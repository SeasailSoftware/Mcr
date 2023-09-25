using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Database
{
    public class InstrumentEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string SN { get; set; }

        public string WhiteboardData { get; set; }
    }
}
