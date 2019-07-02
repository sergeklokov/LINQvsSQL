using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQvsSQL
{
    class Program
    {
        /// Demo app comparing joins
        /// LINQ vs SQL
        /// 
        /// Let's try to use LINQ and get same result, as with SQL
        /// I will try to convert some SQL statement into equivalent LINQ statements
        /// 
        /// Written by Serge Klokov (2019)

        static AdventureWorks2016CTP3Entities1 db = new AdventureWorks2016CTP3Entities1();

        static void Main(string[] args)
        {

            SelectPeople();
            PeopleWithPhones();
            PeopleWithPhones2(); // outer join and not null, really outer join used as inner join
            PeopleAndPhonesIfOwe();
            CartesianJoin();

            Console.WriteLine();
            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }

        // inner join
        private static void PeopleWithPhones()
        {
            // ***********************************************
            // select Name, Number from People l inner join CellPhone p on l.CellPhoneID = p.CellPhoneID  
            // -- people with phones , and phone numbers
            var peopleWithPhones =  from p in db.People
                                    join cp in db.CellPhones
                                    on p.CellPhoneID equals cp.CellPhoneID
                                    //select new { p.Name, cp.Number };
                                    select new NameAndNumber() { Name = p.Name, Number = cp.Number }; // let's use named type to pass IQueriable 

            Print("People with phones (INNER JOIN)", peopleWithPhones);
        }

        // outer join and not null,  really outer join used as inner join
        private static void PeopleWithPhones2()
        {
            // ***********************************************
            // really outer join used as inner join
            // select Name, Number from People l left outer join CellPhone p on l.CellPhoneID = p.CellPhoneID where p.CellPhoneID IS NOT NULL	
            // -- people with CellPhone

            var peopleWithPhones = from p in db.People.AsNoTracking()
                                   join cp in db.CellPhones.AsNoTracking()
                                   on p.CellPhoneID equals cp.CellPhoneID
                                   into cps
                                   from cp in cps.DefaultIfEmpty()
                                   where cp.Number != null
                                   //select new { p.Name, cp.Number };
                                   select new NameAndNumber() { Name = p.Name, Number = cp.Number }; 

            Print("People with phones (OUTER JOIN..phone NOT NULL)", peopleWithPhones);
        }


        private static void Print(
            string header,
            IQueryable<NameAndNumber> queryable)
        {
            // let's print it 
            Console.WriteLine(header);
            Console.WriteLine("Name" + Spaces("Name") + "Number");
            queryable
                .OrderBy(p => p.Name)
                .ToList()
                .ForEach(p => Console.WriteLine("{0}{1}{2}", p.Name, Spaces(p.Name), p.Number));

            Console.WriteLine("Count: " + queryable.Count());

            Console.WriteLine("SQL: " + Environment.NewLine + 
                ((System.Data.Entity.Infrastructure.DbQuery<NameAndNumber>)queryable).Sql);

            Console.WriteLine();
            Console.WriteLine();
        }

        private static string Spaces(string s)
        {
            return new String(' ', 25 - s.Length);
        }

        // outer join
        private static void PeopleAndPhonesIfOwe()
        {
            // ***********************************************
            // select Name, Number from People l left outer join CellPhone p on l.CellPhoneID = p.CellPhoneID 
            // -- all people and phone # if owe

            var peopleWithPhones = from p in db.People.AsNoTracking()
                                   join cp in db.CellPhones.AsNoTracking()
                                   on p.CellPhoneID equals cp.CellPhoneID
                                   into cps 
                                   from cp in cps.DefaultIfEmpty()
                                   select new NameAndNumber() { Name = p.Name, Number = cp.Number };

            Print("People with phones (OUTER JOIN)", peopleWithPhones);
        }

        private static void SelectPeople()
        {
            // select * from People order by Name;
            var people = db.People.OrderBy(p => p.Name);

            Console.WriteLine("Person ID \t Name");  // tab formatted print

            people
                .ToList()
                .ForEach(p => Console.WriteLine("{0} \t\t {1} ", p.PersonID, p.Name));

            Console.WriteLine();
            Console.WriteLine();
        }

        // cartesian join (multiplication or CROSS JOIN)
        // select Name, Number  from People l, CellPhone p order by 1, 2 
        // select *  from People l, CellPhone p 
        private static void CartesianJoin()
        {
            var cartesianJoin = from p in db.People  //.AsNoTracking() - not allowed here!!!
                                from cp in db.CellPhones  
                                //select new { p.Name, cp.Number };
                                select new NameAndNumber() { Name = p.Name, Number = cp.Number };

            Print("People * phones (multiplication)", cartesianJoin);
        }
    }
}
