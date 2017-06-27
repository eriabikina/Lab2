using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
   public class Company : ScrumTeam{

        private static Company instance; //singleton

        private Company () { }

        public static Company Instance {
            get {
                if (instance == null) {
                    instance = new Company ();
                }
                return instance;
            }
        }
              
        private string ListOfTeamMembers (Dictionary<Proficiency, List<SystemMember>> member, string description, string memberName ="Team members\n") {
            description += "\n"+memberName +"\n";
            foreach (var item in member) {
                foreach (var inItem in item.Value) {
                    description += $"{item.Key}: {inItem.Name}\n";
                }
            }
            return description;
        }

        public override string Describe (Developer developer, Tester tester) {
            string intro = base.Describe (developer, tester);

            string description = ListOfTeamMembers (developer.member, intro, "Developers");
            description = ListOfTeamMembers (tester.member, description, "Testers");
            
            return description;
        }
    }
}
