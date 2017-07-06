using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public class Manager {

        private Company company = Company.Instance;

        public void SubscribeForTaskEvent (Developer developer, Tester tester) {
            company.TaskFromClientArrived += (s, args) => {                DistributeTasks (developer, tester);            };
        }

        public void DistributeTasks (Developer developer, Tester tester) {
            Tasks task;
            Filler.Fill (out task, 20); // Randomly fill tasks 

            TaskStatiscticsReport (task);

            DoTaskHandler devTask = developer.DoTask;
            devTask (developer, task);// display development task distribution

            TestTaskHandler testTask = tester.TestTask;
            testTask (tester, task); // display test task distribution     

        }

        private static void TaskStatiscticsReport (Tasks task) {

            var title = new StandardTitle ();
            Backlog backlog = new Backlog (title, task);
            backlog.BacklogSummary (task);

            Reporter.CompareTask (task);
        }
    }
}
