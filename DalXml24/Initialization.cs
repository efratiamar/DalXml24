namespace DalXml2024;

using Dal;
using DalApi;
using DO;
using System;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

public static class Initialization
{
    private static IDal? s_dal;

    private static readonly Random s_rand = new();
    private const int MIN_ID = 200000000;
    private const int MAX_ID = 400000000;

    public static void Do(IDal dal)
    {
        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!");

        Console.WriteLine("Initializing Students list ...");
        createStudents();
        //Thread.Sleep(2000);
        
        Console.WriteLine("Initializing Courses list ...");
        createCourses();
        //Thread.Sleep(2000);

        Console.WriteLine("Initializing Links list ...");
        createLinks();

    }

    private static void createStudents()
    {
        string[] studentNames =
        {
            "Dani Levi",
            "Eli Amar",
            "Yair Cohen",
            "Ariela Levin",
            "Dina Klein",
            "Shira Israelof"
        };

        foreach (var _name in studentNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Student.Read(_id) != null);

            bool _b = (_id % 2) == 0 ? true : false;
            DateTime _bdt = getRandomDate();
            string? _alias = (_id % 2) == 0 ? _name + "ALIAS" : null;

            Student newStu = new(_id, _name, _alias, _b, _bdt);

            s_dal!.Student.Create(newStu);
        }
    }


    private static void createCourses()
    {
        string[] couresNames =
        {
            "CourseA 1",
            "CourseB 222",
            "CourseC 234",
            "CourseD"
        };
        string[] couresNumbers =
{
            "101-666-777",
            "101-666-555",
            "102-633-777",
            "103-666-767"
        };
        int i = 0;
        foreach (var _name in couresNames)
        {
            Year _year = (Year)s_rand.Next((int)Year.FirstYear, (int)Year.ExtraYear + 1);
            SemesterNames _sem = (SemesterNames)s_rand.Next((int)SemesterNames.WinterA, (int)SemesterNames.SpringB + 1);
            WeekDay _day = (WeekDay)s_rand.Next((int)WeekDay.Sunday, (int)WeekDay.Thursday + 1);
            
            TimeSpan _startTime = new TimeSpan(s_rand.Next(8, 19), 0,0); //rand hour between 8:00:00 to 18:00:00 
            TimeSpan _endTime = new TimeSpan(s_rand.Next(9, 20), 0, 0); //rand hour between 9:00:00 to 19:00:00
            int _credits = s_rand.Next(1, 4);

            //no need to set the id of course, since its will be set automatically inside DAL implementation
            Course newCourse = new(0, couresNumbers[i++], _name, _year, _sem, _day, _startTime, _endTime, _credits);

            s_dal!.Course.Create(newCourse);
        }
    }

    private static void createLinks()
    {

        int countOfCourses = s_dal!.Course.ReadAll().Count();
        int _courseId;
        int index;

        foreach (var _student in s_dal!.Student.ReadAll())
        {
            int limit = s_rand.Next(countOfCourses + 1);
            for (int i = 0; i < limit; i++)
            {
                index = s_rand.Next(0, countOfCourses);
                _courseId = s_dal!.Course.ReadAll().ElementAt(index).Id;

                //Thread.Sleep(100);
                if (s_dal!.Link.Read(_student!.Id, _courseId) == null)
                {
                    float _grade = s_rand.Next(0, 101);

                    Link newLink = new(0, _student!.Id, _courseId, _grade);

                    s_dal!.Link.Create(newLink);
                }
            }
        }
    }

    private static DateTime getRandomDate()
    {
        DateTime start = new DateTime(1995, 1, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(s_rand.Next(range));
    }


}
