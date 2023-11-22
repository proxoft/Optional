using Proxoft.Optional;

namespace Proxoft.Sample.App;

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
