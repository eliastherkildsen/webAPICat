using MongoDB.Driver;
using webAPICat.Entity;

namespace webAPICat;

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        //builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5000));
        
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.Configure<CatDatabase>(
        builder.Configuration.GetSection("DatabaseSettings"));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        //app.MapGet("/", () => "Hello World!");

        // CAT 
        
        IList<Cat> cats = new List<Cat>();
        cats.Add(new Cat {Id = "0", Name = "Hans", Breed = "RadioaktivHund"});
        cats.Add(new Cat {Id = "1", Name = "Garfild", Breed = "TyskStenHugger"});
        cats.Add(new Cat {Id = "2", Name = "Bubby", Breed = "SvenskSkovKat"});
        
        
        // creating mongo db. connection
        var mc = new MongoClient("mongodb://localhost:27017");
        var mongoDB = mc.GetDatabase("catDB");
        var _cats = mongoDB.GetCollection<Cat>("cats");

        
        app.MapGet("/cat/", async (string id) =>
        {
            //return cats.Where(c => c.Id == id)
            await _cats.Find(cat => cat.Id == id).FirstOrDefaultAsync();
        });
        
        app.MapPost("/cat/", async (Cat cat) =>
        {
            cats.Add(cat);
            await _cats.InsertOneAsync(cat);
        });
        
        
        app.Run();



    }    
}