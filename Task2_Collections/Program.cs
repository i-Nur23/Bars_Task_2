using System;
namespace Program;
internal static class Program
{
    private static void Main()
    {

        Entity EntityRoot = new Entity(1, "Root entity", 0);
        Entity Entity1 = new Entity(2, "Child of 1 entity", 1);
        Entity Entity2 = new Entity(3, "Child of 1 entity", 1);
        Entity Entity3 = new Entity(4, "Child of 2 entity", 2);
        Entity Entity4 = new Entity(5, "Child of 4 entity", 4);

        List<Entity> entitiesList = new List<Entity>() { EntityRoot, Entity1, Entity2, Entity3, Entity4 };

        Dictionary<int, List<Entity>> entitiesDict = ConvertToDict(entitiesList);

        foreach (var item in entitiesList)
        {
            Console.WriteLine($"Entity{{Id = {item.Id}, ParentId = {item.ParentId}, Name = {item.Name}}}");
        }

        Console.WriteLine("==============");

        foreach (var keys in entitiesDict)
        {
            Console.Write($"Key : {keys.Key}, Value: List{{");
            foreach (var entities in keys.Value)
            {
                Console.Write($"Entity{{Id = {entities.Id}}} ");
            }
            Console.WriteLine("}");
        }
    }

    public static Dictionary<int, List<Entity>> ConvertToDict(List<Entity> list)
    {
        Dictionary<int, List<Entity>> newDict = new Dictionary<int, List<Entity>>();
        
        foreach (Entity entity in list)
        {
            if (newDict.TryGetValue(entity.ParentId, out List<Entity> entitiesFromOneParents))
            {
                if (!entitiesFromOneParents.Contains(entity))
                {
                    entitiesFromOneParents.Add(entity);
                }
            }

            else
            {
                newDict.Add(entity.ParentId, new List<Entity>() { entity });
            }

        }
        return newDict;
    }
}

public class Entity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ParentId { get; set; }

    public Entity(int Id, string Name, int ParentId)
    {
        this.Id = Id;
        this.Name = Name;
        this.ParentId = ParentId;
    }
}
