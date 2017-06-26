using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    class Program {
        static void Main (string[] args) {

            Developer developer;
            Tester tester;
            Filler.Fill (out developer, out tester, 5);




            foreach (var item in developer.member) {
                foreach (var inItem in item.Value) {
                    Console.WriteLine ("{0}: {1}", item.Key, inItem.Name);
                }
            }

            foreach (var item in tester.member) {
                foreach (var inItem in item.Value) {
                    Console.WriteLine ("{0}: {1}", item.Key, inItem.Name);
                }
            }

            Console.ReadLine ();
        }
        
    }
}
