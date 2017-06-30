using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TrackSystem;
using System.Collections;

namespace UnitTestProject1 {
    [TestClass]
    public class TaskSystemTest {

        [TestMethod]
        public void MergeDevTestToListValid () {
            Developer developer;
            Tester tester;
            InitDevTest (out developer, out tester);

            List<SystemMember> expected = new List<SystemMember> ();
            expected.Add (new SystemMember { Name = "Mariya Ustenko", Salary = 1800, Proficiency = Proficiency.Junior });
            expected.Add (new SystemMember { Name = "Den Biliy", Salary = 1500, Proficiency = Proficiency.Junior });
            expected.Add (new SystemMember { Name = "Tedd Cosh", Salary = 3550, Proficiency = Proficiency.Middle });
            expected.Add (new SystemMember { Name = "Olga Timid", Salary = 2050, Proficiency = Proficiency.Middle });
            expected.Add (new SystemMember { Name = "Andrii Lebedev", Salary = 1200, Proficiency = Proficiency.Senior });
            expected.Add (new SystemMember { Name = "Alex Vovk", Salary = 2200, Proficiency = Proficiency.Senior });
            expected.Add (new SystemMember { Name = "Oleg Orlov", Salary = 1000, Proficiency = Proficiency.Junior });
            expected.Add (new SystemMember { Name = "Ivan Orlov", Salary = 1000, Proficiency = Proficiency.Junior });
            expected.Add (new SystemMember { Name = "Anna Volkova", Salary = 1550, Proficiency = Proficiency.Middle });
            expected.Add (new SystemMember { Name = "Elena Ivaniva", Salary = 2350, Proficiency = Proficiency.Middle });
            expected.Add (new SystemMember { Name = "Mark Neil", Salary = 1850, Proficiency = Proficiency.Senior });
            expected.Add (new SystemMember { Name = "Kate Ferdzano", Salary = 1600, Proficiency = Proficiency.Senior });

            List<SystemMember> actual = Reporter.MergeDevTestToList (developer, tester);

            /*my unittest was failing due to problem with values, 
             * turned out to be i had to override the Equals method to explain when the lists should be equal, 
             * for example, when Names are the same then the lists are equal - google helped     
             */
            CollectionAssert.AreEqual (expected, actual, new TestNameComparer ()); 
         }

        [TestMethod]
        public void DoSearchForNameStartingWithATest () {
            Developer developer;
            Tester tester;
            InitDevTest (out developer, out tester);

            List<SystemMember> expected = new List<SystemMember> ();
            expected.Add (new SystemMember { Name = "Andrii Lebedev" });
            expected.Add (new SystemMember { Name = "Alex Vovk" });
            expected.Add (new SystemMember { Name = "Anna Volkova"});

            List<SystemMember> actual = Reporter.DoSearchForNameStartingWithA (developer, tester);

            CollectionAssert.AreEqual (expected, actual, new TestNameComparer ());
        }

        [TestMethod]
        public void DoSortTestSalaryTest () {
            Developer developer;
            Tester tester;
            InitDevTest (out developer, out tester);

            List<SystemMember> expected = new List<SystemMember> ();
            expected.Add ( new SystemMember {  Salary = 1000 } );
            expected.Add (new SystemMember {  Salary = 1000 });
            expected.Add (new SystemMember { Salary = 1550 });
            expected.Add (new SystemMember { Salary = 2350 });
            expected.Add (new SystemMember { Salary = 2600 });            
            expected.Add (new SystemMember {  Salary = 2850 });            

            List<SystemMember> actual = Reporter.DoSortTestSalary (tester);

            CollectionAssert.AreEqual (expected, actual, new TestSalaryComparer ());
        }

        [TestMethod]
        public void CompareSalaryTest () {
            Developer developer;
            Tester tester;
            InitDevTest (out developer, out tester);

            List<SystemMember> expected = new List<SystemMember> ();
            expected.Add (new SystemMember { Salary = 1000 });
            expected.Add (new SystemMember { Salary = 1000 });
            expected.Add (new SystemMember { Salary = 1500 });
            expected.Add (new SystemMember { Salary = 1800 });

            expected.Add (new SystemMember { Salary = 1550 });
            expected.Add (new SystemMember { Salary = 2050 });
            expected.Add (new SystemMember { Salary = 2350 });
            expected.Add (new SystemMember { Salary = 3550 });

            expected.Add (new SystemMember { Salary = 2600 });
            expected.Add (new SystemMember { Salary = 2850 });
            expected.Add (new SystemMember { Salary = 3200 });
            expected.Add (new SystemMember { Salary = 4200 });


            List<SystemMember> actual = Reporter.MergeDevTestToList (developer, tester);
            actual.Sort ();

            CollectionAssert.AreEqual (expected, actual, new TestSalaryComparer ());
        }


        private static void InitDevTest (out Developer developer, out Tester tester) {
            developer = new Developer ();
            Dictionary<Proficiency, List<SystemMember>> emplyoeeDev = new Dictionary<Proficiency, List<SystemMember>> ();

            emplyoeeDev.Add (Proficiency.Junior, new List<SystemMember> () { new SystemMember { Name = "Mariya Ustenko", Salary = 1800 } });
            emplyoeeDev[Proficiency.Junior].Add (new SystemMember { Name = "Den Biliy", Salary = 1500 });

            emplyoeeDev.Add (Proficiency.Middle, new List<SystemMember> () { new SystemMember { Name = "Tedd Cosh", Salary = 3550 } });
            emplyoeeDev[Proficiency.Middle].Add (new SystemMember { Name = "Olga Timid", Salary = 2050 });

            emplyoeeDev.Add (Proficiency.Senior, new List<SystemMember> () { new SystemMember { Name = "Andrii Lebedev", Salary = 3200 } });
            emplyoeeDev[Proficiency.Senior].Add (new SystemMember { Name = "Alex Vovk", Salary = 4200 });

            developer.employee = emplyoeeDev;


            tester = new Tester ();
            Dictionary<Proficiency, List<SystemMember>> employeeTester = new Dictionary<Proficiency, List<SystemMember>> ();

            employeeTester.Add (Proficiency.Junior, new List<SystemMember> () { new SystemMember { Name = "Oleg Orlov", Salary = 1000 } });
            employeeTester[Proficiency.Junior].Add (new SystemMember { Name = "Ivan Orlov", Salary = 1000 });

            employeeTester.Add (Proficiency.Middle, new List<SystemMember> () { new SystemMember { Name = "Anna Volkova", Salary = 1550 } });
            employeeTester[Proficiency.Middle].Add (new SystemMember { Name = "Elena Ivaniva", Salary = 2350 });

            employeeTester.Add (Proficiency.Senior, new List<SystemMember> () { new SystemMember { Name = "Mark Neil", Salary = 2850 } });
            employeeTester[Proficiency.Senior].Add (new SystemMember { Name = "Kate Ferdzano", Salary = 2600 });

            tester.employee = employeeTester;
        }

        private class TestNameComparer : Comparer<SystemMember> {//found this on the internet

            public override int Compare (SystemMember x, SystemMember y) {
                return x.Name.CompareTo (y.Name);
            }
        }

        private class TestSalaryComparer : Comparer<SystemMember> {//found this on the internet

            public override int Compare (SystemMember x, SystemMember y) {
                return x.Salary.CompareTo (y.Salary);
            }
        }
    }
}
