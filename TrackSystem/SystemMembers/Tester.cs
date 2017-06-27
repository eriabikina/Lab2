using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public class Tester : SystemMember, IGenerator<string> {

        public Tester (int sample) {

            RandomSystemMember (sample);
        }


        public void TestTask () { }
    }
}


