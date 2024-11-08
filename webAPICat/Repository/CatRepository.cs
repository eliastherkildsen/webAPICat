using webAPICat.Entity;

namespace webAPICat.Repository;

interface ICatRepository
{
    Task Add(Cat cat);
    Task<Cat> Get(string id);
}

public class CatRepository : ICatRepository
{
    public async Task Add(Cat cat)
    {
        throw new NotImplementedException();
    }

    public async Task<Cat> Get(string id)
    {
        throw new NotImplementedException();
    }
}