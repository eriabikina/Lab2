using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
   public class Company : ScrumTeam{

        public override string Describe (Developer developer, Tester tester) {
            string result = base.Describe (developer, tester);

            result += "Developers\n";
            foreach (var item in developer.member) {
                foreach (var inItem in item.Value) {
                    result += $"{item.Key}: {inItem.Name}\n";
                }
            }
            
            result +="\nTesters";
            foreach (var item in tester.member) {
                foreach (var inItem in item.Value) {
                    result += $"{item.Key}: {inItem.Name}\n";
                }
            }
            return result;
        }

    }
}
