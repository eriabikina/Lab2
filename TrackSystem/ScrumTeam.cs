using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
 public  abstract class ScrumTeam {
        
        public virtual string Describe (Developer developer, Tester tester) {
            return "This is our scrum team\n";
        }
    }
}
