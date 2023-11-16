//using DalApi;

using DO;

namespace DalXml2024;

internal class Program
{
    static readonly DalApi.IDal s_dal = Dal.DalXml.Instance;

    static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dal);

            TestStudent_XElement();
            TestCourse_XmlSerializer();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void TestStudent_XElement()
    {
        Console.WriteLine("-------- TestStudent - XElement ------------");

        Console.WriteLine("### Test Student Create - XElement ###");    
        createStudent(out Student st1);
        Console.WriteLine(s_dal!.Student.Create(st1));

        Console.WriteLine("### Test Student Read - XElement ###");        
        Console.WriteLine("Enter an id");
        if (false == int.TryParse(Console.ReadLine(), out int id1))
            Console.WriteLine("Wrong input");
        Console.WriteLine(s_dal!.Student.Read(id1));

        Console.WriteLine("### Test Student ReadAll - XElement ###");
        foreach (var item in s_dal!.Student.ReadAll())
            Console.WriteLine(item);

        Console.WriteLine("### Test Student Update - XElement ###");
        Console.WriteLine("Enter an id");
        if (false == int.TryParse(Console.ReadLine(), out int id2))
            Console.WriteLine("Wrong input");
        Console.WriteLine(s_dal!.Student.Read(id2));
        createStudent(out Student st2, id2);
        s_dal!.Student.Update(st2);

        Console.WriteLine("### Test Student Delete - XElement ###");
        Console.WriteLine("Enter an id");
        if (false == int.TryParse(Console.ReadLine(), out int id))
            Console.WriteLine("Wrong input");
        s_dal!.Student.Delete(id);
    }


    static void TestCourse_XmlSerializer()
    {
        Console.WriteLine("-------- Test Course - XmlSerializer ------------");

        Console.WriteLine("### Test Course Create - XmlSerializer ###");
        createCourse(out Course cs1);
        Console.WriteLine(s_dal!.Course.Create(cs1));

        Console.WriteLine("### Test Course Read - XmlSerializer ###");
        Console.WriteLine("Enter an id");
        if (false == int.TryParse(Console.ReadLine(), out int id1))
            Console.WriteLine("Wrong input");
        Console.WriteLine(s_dal!.Course.Read(id1));

        Console.WriteLine("### Test Course ReadAll - XmlSerializer ###");
        foreach (var item in s_dal!.Course.ReadAll())
            Console.WriteLine(item);

        Console.WriteLine("### Test Course Update - XmlSerializer ###");
        Console.WriteLine("Enter an id");
        if (false == int.TryParse(Console.ReadLine(), out int id2))
            Console.WriteLine("Wrong input");
        Console.WriteLine(s_dal!.Course.Read(id2));
        createCourse(out Course cs2, id2);
        s_dal!.Course.Update(cs2);

        Console.WriteLine("### Test Course Delete - XmlSerializer ###");
        Console.WriteLine("Enter an id");
        if (false == int.TryParse(Console.ReadLine(), out int id))
            Console.WriteLine("Wrong input");
        s_dal!.Course.Delete(id);
    }


    //private static void createLink(out Link lk, int id = 0)
    //{
    //    Console.Write("enter StudentId of the Link: ");
    //    if (!int.TryParse(Console.ReadLine(), out int stId))
    //        throw new FormatException("Wrong input");

    //    Console.Write("enter CourseId of the Link: ");
    //    if (!int.TryParse(Console.ReadLine(), out int csId))
    //        throw new FormatException("Wrong input");

    //    Console.WriteLine("");

    //    lk = new Link(id, stId, csId);
    //}
    private static void createCourse(out Course cr, int id = 0)
    {
        Console.Write("enter Name of the Course: ");
        string? name = Console.ReadLine() ?? throw new FormatException("Wrong input");

        Console.Write("enter Numer (string) of the Course: ");
        string? course_num = Console.ReadLine() ?? throw new FormatException("Wrong input");

        Console.Write("enter Year of the Course: ");
        if (!Enum.TryParse(Console.ReadLine(), out Year year))
            throw new FormatException("Wrong input");

        Console.Write("enter Semester of the Course: ");
        if (!Enum.TryParse(Console.ReadLine(), out SemesterNames sem))
            throw new FormatException("Wrong input");

        Console.Write("enter WeekDay of the Course: ");
        if (!Enum.TryParse(Console.ReadLine(), out WeekDay day))
            throw new FormatException("Wrong input");

        Console.WriteLine("enter the StartHour of the Course (in format hh:mm:ss): ");
        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan sh)) throw new FormatException("StartHour is invalid!");

        Console.WriteLine("enter the EndHour of the Course (in format hh:mm:ss): ");
        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan eh)) throw new FormatException("EndHour is invalid!");

        Console.WriteLine("");

        cr = new Course(id, course_num, name, year, sem, day, sh, eh);
    }
    private static void createStudent(out Student st, int id = 0)
    {
        if (id == 0)
        {
            Console.Write("enter StudentId: ");
            if (!int.TryParse(Console.ReadLine(), out int _id))
                throw new FormatException("Wrong input");
            else
                id = _id;
        }

        Console.Write("enter Name of the Student: ");
        string? name = Console.ReadLine() ?? throw new FormatException("Wrong input");

        Console.Write("enter Alias of the Student: ");
        string? alias = Console.ReadLine() ?? throw new FormatException("Wrong input");

        Console.Write("enter true/false if the Student is active: ");
        if (!bool.TryParse(Console.ReadLine(), out bool active))
            throw new FormatException("Wrong input");

        Console.WriteLine("enter the BirthDate of the Student (in format dd/mm/yy hh:mm:ss): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime bdt)) throw new FormatException("BirthDate is invalid!");

        Console.WriteLine("");

        st = new Student(id, name, alias, active, bdt);
    }
}