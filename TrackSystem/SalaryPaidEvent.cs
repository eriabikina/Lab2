using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
  public  class SalaryPaidEventArgs: EventArgs {
        public string Name { get; set; }
        public int Salary { get; set; }
    }
}
