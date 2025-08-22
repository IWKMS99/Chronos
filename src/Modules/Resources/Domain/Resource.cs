namespace Chronos.Resources.Domain;

public class Resource {
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Capacity { get; private set; }

    private Resource() { }

    public Resource(Guid id, string name, int capacity) {
        Id = id;
        Name = name;
        Capacity = capacity;
    }

    public void Update(string name, int capacity) {
        Name = name;
        Capacity = capacity;
    }
}