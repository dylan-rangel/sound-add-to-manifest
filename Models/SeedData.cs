using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using project.Data;
using System;
using System.Linq;

namespace project.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new projectContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<projectContext>>()))
        {
            // Look for any movies.
            if (context.Entry.Any())
            {
                return;   // DB has been seeded
            }
            context.Entry.AddRange(
                new Entry
                {
                    Name = "Example Sound",
                    Channel = "Example Channel",
                    Directory = "Example Dir",
                },
                new Entry
                {
                    Name = "Sound",
                    Channel = "Channel",
                    Directory = "Dir",
                }
            );
            context.SaveChanges();
        }
    }
}
