using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
   class EmployeeProficiencyComparer: IEqualityComparer<SystemMember> {

        private static readonly EmployeeProficiencyComparer instance = new EmployeeProficiencyComparer ();

        public static EmployeeProficiencyComparer Instance { get { return instance; } }

        private EmployeeProficiencyComparer() { }        

        public bool Equals (SystemMember x, SystemMember y) {
            return x.Proficiency == y.Proficiency;
        }

        public int GetHashCode (SystemMember obj) {
            return obj.Proficiency.GetHashCode ();
        }
    }
}
