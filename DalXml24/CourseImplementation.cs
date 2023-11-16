namespace Dal; 

using DalApi;
using DO;
using System.Linq;

internal class CourseImplementation : ICourse
{
    readonly string s_courses_xml = "courses";
    public int Create(Course item)
    {
        List<Course> Courses = XMLTools.LoadListFromXMLSerializer<Course>(s_courses_xml);

        //for entities with auto id
        int nextId = Config.NextCourseId;
        Course copy = item with { Id = nextId };
        Courses.Add(copy);

        XMLTools.SaveListToXMLSerializer(Courses, s_courses_xml);

        return nextId;
    }

    public Course? Read(int id)
    {
        List<Course> Courses = XMLTools.LoadListFromXMLSerializer<Course>(s_courses_xml);
        return Courses.FirstOrDefault(o => o.Id == id);

    }

    public Course? Read(Func<Course, bool> filter)
    {
        List<Course> Courses = XMLTools.LoadListFromXMLSerializer<Course>(s_courses_xml);
        return Courses.FirstOrDefault(filter);
    }

    public IEnumerable<Course> ReadAll(Func<Course, bool>? filter = null)
    {
        List<Course> Courses = XMLTools.LoadListFromXMLSerializer<Course>(s_courses_xml);

        if (filter == null)
            return Courses.Select(item => item);
        else
            return Courses.Where(filter);
    }

    public void Update(Course item)
    {
        Delete(item.Id);
        Create(item);
    }
    public void Delete(int id)
    {
        throw new DalDeletionImpossible();
    }
}
