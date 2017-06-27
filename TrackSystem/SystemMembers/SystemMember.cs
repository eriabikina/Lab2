using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {

    public enum Proficiency {
        Junior,
        Middle,
        Senior
    };

    public class SystemMember {

        public string Name { get; set; }

        public int Salary { get; set; }

        static readonly IGenerator<string> FirstNameGen = new FirstNameGenerator ();// found ObjectHydrator on github to generate random full names using real English names and surname
        static readonly IGenerator<string> LastNameGen = new LastNameGenerator ();
        public string GenerateRandomFullName () {
            return FirstNameGen.GenerateRandomFullName () + " " + LastNameGen.GenerateRandomFullName ();
        }

        public Dictionary<Proficiency, List<SystemMember>> member = new Dictionary<Proficiency, List<SystemMember>> ();
        public void RandomSystemMember (int sample) {
            Random random = new Random (Guid.NewGuid ().GetHashCode ());

            member.Add (Proficiency.Junior, new List<SystemMember> () { new SystemMember { Name = GenerateRandomFullName (), Salary = random.Next (1000, 5000) } });
            member.Add (Proficiency.Middle, new List<SystemMember> () { new SystemMember { Name = GenerateRandomFullName (), Salary = random.Next (1000, 5000) } });
            member.Add (Proficiency.Senior, new List<SystemMember> () { new SystemMember { Name = GenerateRandomFullName (), Salary = random.Next (1000, 5000) } });

            for (int i = 0; i < sample - 3; i++) {
                member[RandomEnum.GenerateRandomEnum<Proficiency> ()].Add (new SystemMember { Name = GenerateRandomFullName (), Salary = random.Next (1000, 5000) });
            }
        }

    }
}

