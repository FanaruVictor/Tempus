using Tempus.Core.Entities;
using Tempus.Data.Context;

namespace Tempus.IntegrationTests.Configuration;

internal static class DbSeed
{
    public static void SeedUsers(TempusDbContext context)
    {
        var users = new List<User>
        {
            new (
                new Guid("6627df4f-6ac6-4ff6-bf8e-6d358fd88025"),
                "victor",
                "victor@fanaru"
                ),
            new(
                new Guid("68af0be2-624d-4fe6-9a19-a83e932038bf"),
                "daniel",
                "daniel@fanaru"
            ),
            new(
                new Guid("68cf3d1c-07c9-4df4-898e-30db1ebeb888"),
                "victor",
                "victor@daniel"
            ),
            new(
                new Guid("6a0cd002-0cc5-4565-9852-01e3a90f01e9"),
                "daniel",
                "daniel@victor"
            ),
            new(
                new Guid("687318c1-1b11-4b1f-abda-7d230bb18ee1"),
                "fanaru",
                "victor.daniel@fanaru"
            )
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }

    public static void SeedCategories(TempusDbContext context)
    {
        var categories = new List<Category>
        {
            new(
                new Guid("c4abd929-0cdd-4c04-afa4-3dbeb3f686d1"),
                "category1",
                DateTime.UtcNow,
                DateTime.UtcNow,
                "color1",
                new Guid("68af0be2-624d-4fe6-9a19-a83e932038bf")
            ),
            new(
                new Guid("c8591507-e077-4ad3-a673-4d7fcb944215"),
                "category2",
                DateTime.UtcNow,
                DateTime.UtcNow,
                "color2",
                new Guid("68af0be2-624d-4fe6-9a19-a83e932038bf")
            ),
            new(
                new Guid("218e7d32-4ab0-47fb-aae5-b10b309163e3"),
                "category3",
                DateTime.UtcNow,
                DateTime.UtcNow,
                "color3",
                new Guid("6a0cd002-0cc5-4565-9852-01e3a90f01e9")
            ),
            new(
                new Guid("b91a8f2e-5294-4ec7-b563-0e367f36572b"),
                "category4", DateTime.UtcNow,
                DateTime.UtcNow,
                "color4",
                new Guid("6a0cd002-0cc5-4565-9852-01e3a90f01e9")
            ),
            new(
                new Guid("d2bbbffc-d7d0-4477-be87-d2e68aeb0ffa"),
                "category5",
                DateTime.UtcNow,
                DateTime.UtcNow,
                "color5",
                new Guid("6a0cd002-0cc5-4565-9852-01e3a90f01e9")
            )
        };

        context.Categories.AddRange(categories);
        context.SaveChanges();
    }

    public static void SeedRegistration(TempusDbContext context)
    {
        var registrations = new List<Registration>
        {
            new(
                new Guid("1b5bdce1-68e2-4d4e-b0fa-88c23cf0bbfe"),
                "registration1",
                "content1",
                DateTime.UtcNow,
                DateTime.UtcNow,
                new Guid("218e7d32-4ab0-47fb-aae5-b10b309163e3")
            ),
            new(
                new Guid("a2713b60-bf31-4493-975c-eb29e5a89e1f"),
                "registration2",
                "content2",
                DateTime.UtcNow,
                DateTime.UtcNow,
                new Guid("218e7d32-4ab0-47fb-aae5-b10b309163e3")
            ),
            new(
                new Guid("1b409dea-6d37-45b4-8d74-1b6c43271660"),
                "registration3",
                "content3",
                DateTime.UtcNow,
                DateTime.UtcNow,
                new Guid("218e7d32-4ab0-47fb-aae5-b10b309163e3")
            ),
            new(
                new Guid("d5f3a769-b42c-4f61-9039-1912defdcbbc"),
                "registration4",
                "content4",
                DateTime.UtcNow,
                DateTime.UtcNow,
                new Guid("d2bbbffc-d7d0-4477-be87-d2e68aeb0ffa")
            ),
            new(
                new Guid("846a5469-9d9f-45a2-a38f-c5de09deb7f7"),
                "registration5",
                "content5",
                DateTime.UtcNow,
                DateTime.UtcNow,
                new Guid("d2bbbffc-d7d0-4477-be87-d2e68aeb0ffa")
            )
        };

        context.Registrations.AddRange(registrations);
        context.SaveChanges();
    }
}