namespace Dal;
using DalApi;

sealed internal class DalXml : IDal 
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IStudent Student => new StudentImplementation();
    public ICourse Course => new CourseImplementation();
    public ILink Link =>  new LinkImplementation();
}
