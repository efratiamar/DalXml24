namespace Dal;

using DalApi;
using DO;
using System.Linq;

internal class LinkImplementation : ILink
{
    readonly string s_links_xml = "links";
    public int Create(Link item)
    {
        List<Link> Links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);
        
        //for entities with auto nextId
        int nextId = Config.NextLinkId;
        Link copy = item with { Id = nextId };
        Links.Add(copy);

        XMLTools.SaveListToXMLSerializer(Links, s_links_xml);

        return nextId;
    }

    public Link? Read(int id)
    {
        List<Link> Links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);
        
        return Links.FirstOrDefault(item => item.Id == id);
    }


    public Link? Read(Func<Link, bool> filter)
    {
        List<Link> Links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);

        return Links.FirstOrDefault(filter);
    }

    public IEnumerable<Link> ReadAll(Func<Link, bool>? filter = null)
    {
        List<Link> Links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);

        if (filter == null)
            return Links.Select(item => item);
        else
            return Links.Where(filter);
    }

    public void Update(Link item)
    {
        Delete(item.Id);
        Create(item);
    }
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    public Link? Read(int _StudentId, int _CourseId) //Read entity object by 2 IDs
    {
        List<Link> Links = XMLTools.LoadListFromXMLSerializer<Link>(s_links_xml);
        return Links.FirstOrDefault(item => (item.StudentId == _StudentId && item.CourseId == _CourseId)); //stage 2
    }
}
