using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public static class Reporter {

        public static void ShowDevLevel (Developer developer, Proficiency proficiency) {

            Repository repository = new Repository ();
            string path = repository.BuildPath ("/Reports", "ShowDevLevel");

            using (StreamWriter stream = new StreamWriter (path)) {
                stream.WriteLine (proficiency.ToString ());
                stream.WriteLine ("---------------------------------------");
                foreach (var item in developer.employee.Where (x => x.Key == proficiency)) {
                    foreach (var inItem in item.Value) {
                        stream.WriteLine (inItem.Name);
                    }
                }
            }
        }

        public static void SortTestSalary (Tester tester) {

            Repository repository = new Repository ();
            string path = repository.BuildPath ("/Reports", "SortTestSalary");

            List<SystemMember> testerList = new List<SystemMember> ();

            foreach (var item in tester.employee) {
                foreach (var inItem in item.Value) {
                    testerList.Add (inItem);
                }
            }

            using (StreamWriter stream = new StreamWriter (path)) {
                foreach (var item in testerList.OrderBy (x => x.Salary).ToList ()) {
                    stream.WriteLine (item.Name + ":" + item.Salary.ToString ());
                }
            }
        }

        public static void GroupDevByAmountOfTasks (Developer developer) {

            Repository repository = new Repository ();
            string path = repository.BuildPath ("/Reports", "GroupDevByAmountOfTasks");

            List<SystemMember> devList = new List<SystemMember> ();

            foreach (var item in developer.employee) {
                foreach (var inItem in item.Value) {
                    devList.Add (inItem);
                }
            }

            var query = from e in devList
                        group e by e.NumberOfTasks into eg
                        select new {
                            NumberKey = eg.Key,
                            EmployeeValue = eg
                        };

            using (StreamWriter stream = new StreamWriter (path)) {
                foreach (var item in query) {
                    stream.WriteLine (item.NumberKey.ToString () + ":\n");
                    foreach (var inItem in item.EmployeeValue) {
                        stream.WriteLine (inItem.Name + "\n");
                    }
                }
            }
        }

        public static void SearchForNameStartingWithA (Developer developer, Tester tester) {

            Repository repository = new Repository ();
            string path = repository.BuildPath ("/Reports", "SearchForNameStartingWithA");

            List<SystemMember> allList = new List<SystemMember> ();

            foreach (var item in developer.employee) {
                foreach (var inItem in item.Value) {
                    allList.Add (inItem);
                }
            }
            
            foreach (var item in tester.employee) {
                foreach (var inItem in item.Value) {
                    allList.Add (inItem);
                }
            }
                       
            using (StreamWriter stream = new StreamWriter (path)) {
                foreach (var item in allList.Where (x => x.Name.StartsWith ("A"))) {
                    stream.WriteLine (item.Name);
                }
            }
          
        }
    }
}
