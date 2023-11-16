namespace DO;

public enum Year
{
    FirstYear,
    SecondYear,
    ThirdYear,
    ExtraYear
}

public enum SemesterNames
{
    WinterA,
    SpringB,
    Year,
    Summer,
    Elul
}

public enum WeekDay
{
    Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
}

public record Student
(
        int Id,
        string? Name = null,
        string? Alias = null,
        bool IsActive = false,
        DateTime? BirthDate = null
)
{
    public Student() : this(0) { } //empty ctor for stage 3 

    /// <summary>
    /// RegistrationDate - registration date of the current student record 
    /// </summary>
    public DateTime RegistrationDate => DateTime.Now; //get only
}

public record Course
(
    int Id,
    string CourseNumber,
    string CourseName,
    Year? InYear = null,
    SemesterNames? InSemester = null,
    WeekDay? DayInWeek = null,
    TimeSpan? StartTime = null,
    TimeSpan? EndTime = null,
    int? Credits = null
)
{
    public Course() : this(0, "", "") { } //empty ctor for stage 3 
}

public record Link
(
    int Id,
    int StudentId,
    int CourseId,
    float? Grade = null
)
{
    public Link() : this(0, 0, 0) { } //empty ctor for stage 3 
}




[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}

[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible() : base("This entity can not be deleted - please deactiveate instead") { }
}


[Serializable]
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}