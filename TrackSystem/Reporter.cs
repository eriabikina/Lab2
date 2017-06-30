using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {

    public static class Reporter {


        public static void ShowDevByLevel (Developer developer, Proficiency proficiency) {

            string path = BuildPath ("Reports", "ShowDevLevel");
                       
            using (StreamWriter stream = new StreamWriter (path)) {
                stream.WriteLine (proficiency.ToString ());
                stream.WriteLine ("---------------------------------------");
                foreach (var item in DoShowDevByLevel (developer, proficiency)) {
                    foreach (var inItem in item.Value) {
                        stream.WriteLine (inItem.Name);
                    }
                }
            }
        }

        public static Dictionary<Proficiency, List<SystemMember>> DoShowDevByLevel (Developer developer, Proficiency proficiency) {
            return developer.employee.Where (x => x.Key == proficiency).ToDictionary (x => x.Key, x => x.Value);
        }

        public static void SortTestSalary (Tester tester) {

            string path = BuildPath ("Reports", "SortTestSalary");                       

            using (StreamWriter stream = new StreamWriter (path)) {
                foreach (var item in DoSortTestSalary (tester)) {
                    stream.WriteLine (item.Name + ":" + item.Salary.ToString ());
                }
            }
        }

        public static List<SystemMember> DoSortTestSalary (Tester tester) {
            List<SystemMember> testerList = new List<SystemMember> ();

            foreach (var item in tester.employee) {
                foreach (var inItem in item.Value) {
                    testerList.Add (inItem);
                }
            }
            List<SystemMember> result = testerList.OrderBy (x => x.Salary).ToList ();
            return result;
        }

        public static void GroupDevByAmountOfTasks (Developer developer) {

            string path = BuildPath ("Reports", "GroupDevByAmountOfTasks");

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

            string path = BuildPath ("Reports", "SearchForNameStartingWithA");

            using (StreamWriter stream = new StreamWriter (path)) {
                foreach (var item in DoSearchForNameStartingWithA (developer, tester)) {
                    stream.WriteLine (item.Name);
                }
            }
        }

        public static List<SystemMember> DoSearchForNameStartingWithA (Developer developer, Tester tester) {
            List<SystemMember> allList = MergeDevTestToList (developer, tester);
            List<SystemMember> result = allList.Where (x => x.Name.StartsWith ("A")).ToList ();
            return result;
        }

        public static void CompareSalary (Developer developer, Tester tester) {

            string path = BuildPath ("Reports", "CompareSalary");

            List<SystemMember> allList = MergeDevTestToList (developer, tester);
            allList.Sort (); //uses customized CompareTo method

            using (StreamWriter stream = new StreamWriter (path)) {
                foreach (var element in allList) {
                    stream.WriteLine (element.Proficiency + ":\t" + element.Name + "\t" + element.Salary);
                }
            }
        }

        public static void CompareTask (Tasks task) {

            string path = BuildPath ("Reports", "CompareTask");

            List<SystemTask> allTaskList = MergeTaskToList (task);

            allTaskList.Sort (); //uses customized CompareTo method, see SystemMember

            using (StreamWriter stream = new StreamWriter (path)) {
                foreach (var element in allTaskList) {
                    stream.WriteLine (element.Cr + ":\t" + element.Priority + "\t" + element.Estimate);
                }
            }
        }

        public static void SingleTesterPerProficiency (Tester tester) {

            string path = BuildPath ("Reports", "SingleTesterPerProficiency");

            var testList = new HashSet<SystemMember> (EmployeeProficiencyComparer.Instance);

            foreach (var item in tester.employee) {
                foreach (var inItem in item.Value) {
                    inItem.Proficiency = item.Key;
                    testList.Add (inItem);
                }
            }

            using (StreamWriter stream = new StreamWriter (path)) {
                foreach (var element in testList) {
                    stream.WriteLine (element.Proficiency + ":\t" + element.Name + "\t" + element.Salary);
                }
            }
        }

        public static List<SystemMember> MergeDevTestToList (Developer developer, Tester tester) {
            List<SystemMember> allList = new List<SystemMember> ();

            foreach (var item in developer.employee) {
                foreach (var inItem in item.Value) {
                    inItem.Proficiency = item.Key;
                    allList.Add (inItem);
                }
            }

            foreach (var item in tester.employee) {
                foreach (var inItem in item.Value) {
                    inItem.Proficiency = item.Key;
                    allList.Add (inItem);
                }
            }
            return allList;
        }

        public static List<SystemTask> MergeTaskToList (Tasks task) {
            List<SystemTask> allList = new List<SystemTask> ();

            foreach (var item in task.sampleTask) {
                foreach (var inItem in item.Value) {
                    inItem.Cr = item.Key;
                    allList.Add (inItem);
                }
            }

            return allList;
        }

        public static string BuildPath (string folderName, string fileName) {
            StringBuilder sb = new StringBuilder ();

            string path = Environment.CurrentDirectory + "/" + folderName;
            if (!Directory.Exists (path)) {
                Directory.CreateDirectory (path);
            }
            sb.Append (path).Append ($"/{ fileName}.txt");

            return sb.ToString ();
        }
    }
}
