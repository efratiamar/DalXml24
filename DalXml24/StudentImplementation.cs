namespace Dal;

using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class StudentImplementation : IStudent
{
    readonly string s_students_xml = "students";

    static Student getStudent(XElement s)
    {
        return new Student()
        {
            Id = s.ToIntNullable("Id") ?? throw new FormatException("can't convert id"),
            Name = (string?)s.Element("Name") ?? "",
            Alias = (string?)s.Element("Alias") ?? null,
            IsActive = (bool?)s.Element("IsActive") ?? false,
            //CurrentYear = s.ToEnumNullable<Year>("CurrentYear") ?? Year.FirstYear,
            BirthDate = s.ToDateTimeNullable("BirthDate"),
        };
    }

    static IEnumerable<XElement> createStudentElement(Student student)
    {
        yield return new XElement("Id", student.Id);
        yield return new XElement("Name", student.Name);
        if (student.Alias is not null)
            yield return new XElement("Alias", student.Alias);
        yield return new XElement("IsActive", student.IsActive);
        //yield return new XElement("CurrentYear", student.CurrentYear);
        if (student.BirthDate is not null)
            yield return new XElement("BirthDate", student.BirthDate);
    }

    public int Create(Student item)
    {
        //XElement
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_students_xml);

        if (XMLTools.LoadListFromXMLElement(s_students_xml)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("Id") == item.Id) is not null)
                throw new DalAlreadyExistsException($"Student with ID={item.Id} already exists");

        studentsRootElem.Add(new XElement("Student", createStudentElement(item)));
        XMLTools.SaveListToXMLElement(studentsRootElem, s_students_xml);

        return item.Id;

        //XMLSerializer
        //List<Student> Students = XMLTools.LoadListFromXMLSerializer<Student>(s_students_xml);

        ////for entities with normal id (not auto id)
        //if (Read(item.Id) is not null)
        //    throw new DalAlreadyExistsException($"Student with ID={item.Id} already exists");
        //Students.Add(item);

        //XMLTools.SaveListToXMLSerializer(Students, s_students_xml);

        //return item.Id;
    }
    public Student? Read(int id)
    {
        //XElement
        XElement? studentElem = XMLTools.LoadListFromXMLElement(s_students_xml).Elements().FirstOrDefault(st => (int?)st.Element("Id") == id);
        return studentElem is null ? null : getStudent(studentElem);

        //XMLSerializer
        //List<Student> Students = XMLTools.LoadListFromXMLSerializer<Student>(s_students_xml);
        //return Students.FirstOrDefault(item => item.Id == id);
    }

    public Student? Read(Func<Student, bool> filter)
    {
        //XElement
        return XMLTools.LoadListFromXMLElement(s_students_xml).Elements().Select(s => getStudent(s)).FirstOrDefault(filter);

        //XMLSerializer
        //List<Student> Students = XMLTools.LoadListFromXMLSerializer<Student>(s_students_xml);
        //return Students.FirstOrDefault(filter);
    }

    public IEnumerable<Student> ReadAll(Func<Student, bool>? filter = null)
    {
        //XElement
        if (filter == null)
            return XMLTools.LoadListFromXMLElement(s_students_xml).Elements().Select(s => getStudent(s));
        else
            return XMLTools.LoadListFromXMLElement(s_students_xml).Elements().Select(s => getStudent(s)).Where(filter);


        //XMLSerializer
        //List<Student> Students = XMLTools.LoadListFromXMLSerializer<Student>(s_students_xml);

        //if (filter == null)
        //    return Students.Select(item => item);
        //else
        //    return Students.Where(filter);
    }

    public void Update(Student item)
    {
        Delete(item.Id);
        Create(item);
    }
    public void Delete(int id)
    {
        //XElement
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_students_xml);

        (studentsRootElem.Elements()
            .FirstOrDefault(st => (int?) st.Element("Id") == id) ?? throw new DalDoesNotExistException($"Student with ID={id} does Not exist"))
            .Remove();

        XMLTools.SaveListToXMLElement(studentsRootElem, s_students_xml);

        //XMLSerializer
        //List<Student> Students = XMLTools.LoadListFromXMLSerializer<Student>(s_students_xml);
        //if (Students.RemoveAll(item => item.Id == id) == 0)
        //    throw new DalDoesNotExistException($"Student with ID={id} does Not exist");
        //XMLTools.SaveListToXMLSerializer(Students, s_students_xml);
    }
}
