using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public delegate void SalaryTimeHandler (object sender, SalaryPaidEventArgs args);
    public delegate void TasksFromClientHandler (object sender, TaskFromClientEventArgs args);

    public class Company : ScrumTeam {

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

        private string ListOfTeamMembers (Dictionary<Proficiency, List<SystemMember>> employee, string description, string employeeName = "Team member(s)\n") {
            description += "\n" + employeeName + "\n";
            foreach (var item in employee) {
                foreach (var inItem in item.Value) {
                    description += $"{item.Key}: {inItem.Name}\n";
                }
            }
            return description;
        }

        public override string Describe (Developer developer, Tester tester) {
            string intro = base.Describe (developer, tester);

            string description = ListOfTeamMembers (developer.employee, intro, "Developer(s)");
            description = ListOfTeamMembers (tester.employee, description, "Tester(s)");

            return description;
        }

        public event SalaryTimeHandler SalaryPaid;

        public void SalaryTime (Dictionary<Proficiency, List<SystemMember>> employee) {
            if (SalaryPaid != null) {
                Console.WriteLine ("===========");
                Console.WriteLine ("Salary time");
                Console.WriteLine ("===========");
                foreach (var item in employee) {
                    foreach (var inItem in item.Value) {
                        SalaryPaidEventArgs args = new SalaryPaidEventArgs ();
                        args.Name = inItem.Name;
                        args.Salary = inItem.Salary;
                        SalaryPaid (this, args);
                    }
                }
                Console.WriteLine ();
            }
        }

        public event TasksFromClientHandler TaskFromClientArrived;

        public Tasks TaskFromClient () {
            Tasks task=null;
            if (TaskFromClientArrived != null) {
                TaskFromClientEventArgs args = new TaskFromClientEventArgs ();                  
                TaskFromClientArrived (this, args);
            }
            return task;
        }
    }
}




