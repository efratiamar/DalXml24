using DO;

namespace DalApi;
public interface ICrud<T> where T : class
{
    int Create(T item); //Create new entity object in DAL
    T? Read(int id); //Read entity object by its ID 
    T? Read(Func<T, bool> filter); //Read entity object by a condition
    IEnumerable<T> ReadAll(Func<T, bool>? filter = null); //Read all antity objects filtered by a condition
    void Update(T item); //Update entity object
    void Delete(int id); //Delete an object by its Id
}

public interface IStudent : ICrud<Student>
{ }

public interface ICourse : ICrud<Course>
{ }

public interface ILink : ICrud<Link>
{
    //new extra method
    Link? Read(int _StudentId, int _CourseId); //Read entity object by 2 IDs
}

public interface IDal
{
    IStudent Student { get; }
    ICourse Course { get; }
    ILink Link { get; }
}