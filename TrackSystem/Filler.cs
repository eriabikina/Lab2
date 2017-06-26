using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    class Filler {

       public static void Fill (out Developer developer, out Tester tester, int sample) {
            developer = new Developer (sample);
            tester = new Tester (sample);
        }
    }
}
