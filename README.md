# Optional
Optional objects: Option&lt;T>, Either&lt;Left, Right>


# Usage

```csharp

public record Entity(int Id, int Value);

```

```csharp

public class Repository
{
    private List<Entity> _entities = new List<Entity>();

    public Option<Entity> FindBy(int id)
    {
        Entity? entity = _entities.SingleOrDefault(e => e.Id == id);
        return entity.WhenNotNull();
    }

    public Either<string, Entity> Create(int value)
    {
        if (value < 0)
        {
            return "cannot create entity with value less then 0";
        }

        int newId = (_entities.LastOrDefault()?.Id ?? 0) + 1;
        Entity newEntity = new(newId, value);
        _entities.Add(newEntity);

        return newEntity;
    }

    public Either<string, Entity> Update(int id, int value)
    {
        if (_entities.All(e => e.Id != id))
        {
            return $"entity with id {id} does not exist";
        }

        if (value < 0)
        {
            return $"cannot update entity {id} with value {value} which is less then 0";
        }

        int index = _entities.FindIndex(e => e.Id == id);
        _entities[index] = _entities[index] with
        {
            Value = value
        };

        return _entities[index];
    }

    public Option<string> Delete(int id)
    {
        if (_entities.All(e => e.Id != id))
        {
            return $"entity with id {id} does not exist";
        }

        _entities.RemoveAll(e => e.Id == id);
        return None.Instance;
    }
}

```

```csharp

Console.WriteLine("Let's try options!");

Repository repo = new();

string message = "";

Console.WriteLine();
Console.WriteLine("== Create entity ==");
Console.WriteLine();

message = repo
    .Create(15)
    .Reduce(
        e => $"entity with id {e.Id} created",
        e => e
    );
Console.WriteLine(message);

message = repo
    .Create(-2)
    .Reduce(
        e => $"entity with id {e.Id} created",
        e => e
    );
Console.WriteLine(message);

message = repo
    .Create(9)
    .Reduce(
        e => $"entity with id {e.Id} created",
        e => e
    );
Console.WriteLine(message);

Console.WriteLine();
Console.WriteLine("== Find entity ==");
Console.WriteLine();

Option<Entity> entity = repo
    .FindBy(id: 1);

message = entity
    .Map(e => $"entity {e.Id} has value is {e.Value}")
    .Reduce($"entity with id {1} not found");

Console.WriteLine(message);

Console.WriteLine();
Console.WriteLine("== Find and update entity ==");
Console.WriteLine();

entity = repo
    .FindBy(id: 11);

message = entity
    .Map(e => $"entity {e.Id} has value is {e.Value}")
    .Reduce($"entity with id {11} not found");

Console.WriteLine(message);

message = repo
    .FindBy(id: 1)
    .Either(none: "not found")
    .Map(maybe => TrySetValueAndSave(maybe))
    .Reduce(
        e => $"entity {e.Id} has value is {e.Value}",
        error => error
    );
Console.WriteLine(message);

message = repo
    .FindBy(id: 2)
    .Either(none: "not found")
    .Map(maybe => TrySetValueAndSave(maybe))
    .Reduce(
        e => $"entity {e.Id} has value is {e.Value}",
        error => error
    );
Console.WriteLine(message);

message = repo
    .FindBy(id: 3)
    .Either(none: "3 not found")
    .Map(maybe => TrySetValueAndSave(maybe)) // won't be executed at all
    .Reduce(
        e => $"entities value is {e.Value}",
        error => error
    );
Console.WriteLine(message);

Either<string, Entity> TrySetValueAndSave(Either<string, Entity> entity)
{
    return entity
        .Map(e => e with
        {
            Value = e.Value - 10
        })
        .Map(e => repo.Update(e.Id, e.Value));
}

```

