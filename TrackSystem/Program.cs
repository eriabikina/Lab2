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
            Tasks task;
            Filler.Fill (out developer, out tester,out task, 5);

            Company company = new Company ();
            string result = company.Describe (developer, tester);

            Console.WriteLine (result);
            Console.ReadLine ();
        }
        
    }
}
