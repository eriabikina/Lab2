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

        public static void Fill (out Developer developer, int sample) {
            developer = new Developer (sample);                        
        }

        public static void Fill ( out Tester tester, int sample) {
            tester = new Tester (sample);
        }

        public static void Fill (out Tasks task, int sample) {
            task = new Tasks (sample);
        }
    }
}
