using Microsoft.EntityFrameworkCore;
using System.Linq;
using HotChocolate;
using System;

namespace gql
{
  public class Key : Attribute
  {

  }


  public class User
  {
    public string Id { get; set; }
    public string Username { get; set; }

    public ChatClient ChatClient { get; set; }
  }

  public class UserType : ObjectType<User>
  {

  }


  public class ChatClient
  {
    public string Id { get; set; }
    public string Username { get; set; }
  }


  public class ChatMessages
  {
    public string Id { get; set; }

    public ChatClient To { get; set; }
    public ChatClient From { get; set; }
  }


  public class ApplicationDBContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<ChatClient> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().Property(b => b.Username).IsRequired();

      modelBuilder.Entity<ChatClient>().Property(b => b.Username).IsRequired();

      modelBuilder.Entity<ChatMessages>().Property(b => b.To).IsRequired();
      modelBuilder.Entity<ChatMessages>().Property(b => b.From).IsRequired();
    }
  }


  public class ApplicationQueryContext
  {
    public ApplicationDBContext DB { get; set; }
  }


  public class Query
  {
    public IQueryable<User> GetUsers([Service] ApplicationQueryContext context) => context.DB.Users;
  }


  public class Program
  {
    static void Main(string[] args)
    {
      var schema = SchemaBuilder.New();
      schema.AddQueryType<Query>().Create();

      //schema


      Console.WriteLine("Hello World!");

    }
  }
}
