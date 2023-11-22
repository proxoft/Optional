// See https://aka.ms/new-console-template for more information

using Proxoft.Optional;
using Proxoft.Sample.App;

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