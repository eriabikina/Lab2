using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TrackSystem {
    class Program {
        static void Main (string[] args) {
            Company company = Company.Instance;
            Manager manager = new Manager ();
            Developer developer;
            Tester tester;
            int sample = 5;

            Task <Developer> InitDev = Task.Factory.StartNew (() => {
                Filler.Fill (out developer, sample); // Randomly fill dev
                return developer ;                 
            });
            Task<Tester> InitTest = InitDev.ContinueWith ((antecedent ) => {
                Filler.Fill (out tester, sample); // Randomly fill testers 
                return tester;
            });            

            Reporter.ShowDevLevel (InitDev.Result, Proficiency.Junior);
            Reporter.SortTestSalary (InitTest.Result);
            Reporter.SearchForNameStartingWithA (InitDev.Result, InitTest.Result);
            Reporter.CompareSalary (InitDev.Result, InitTest.Result);
            Reporter.SingleTesterPerProficiency (InitTest.Result);
                    
            Task <string>  T = Task.Factory.StartNew (() => {
                string scrumTeamMemebers = company.Describe (InitDev.Result, InitTest.Result); // Display scrum team members   
                return scrumTeamMemebers;
            });
            T.Wait ();
            Console.WriteLine (T.Result);

            company.SalaryPaid += OnSalaryPaid;   //company is subscribed to event: salary time 
            manager.SubscribeForTaskEvent (InitDev.Result, InitTest.Result);//manager is subscribed to event: tasks from client       

            Task T2 = Task.Factory.StartNew (() => {
                company.TaskFromClient ();// event: tasks from client 
            });

            Task T3 = Task.Factory.StartNew (() => {
                company.SalaryTime (InitTest.Result.employee);// event: salary time
            });
            T3.Wait ();

            Task T4 = Task.Factory.StartNew (() => {
                company.SalaryTime (InitDev.Result.employee);// event: salary time
            });
            T4.Wait ();

            Task[] tasks = { InitDev, InitTest, T, T2, T3, T4 };
            Task.WaitAll (tasks);
            Console.WriteLine ($"Reports can be found at: {Environment.CurrentDirectory}/Reports...");
            Console.Read ();
        }

        private static void OnSalaryPaid (object sender, SalaryPaidEventArgs args) {
            Console.WriteLine ($"My name is {args.Name} and I got paid: ${args.Salary}");
        }

    }
}
