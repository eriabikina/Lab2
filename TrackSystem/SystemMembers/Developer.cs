using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public class Developer : SystemMember, IGenerator<string> {

        public Developer (int sample) {

            RandomSystemMember (sample);
        }

        public void DoTask () { }


    }
}
