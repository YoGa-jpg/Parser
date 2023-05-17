namespace Parser.DataContext.Repositories.Interfaces
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>() where T : class;
        void Insert<T>(T obj) where T : class;
        void Update<T>(T obj) where T : class;
        void Delete<T>(Guid id) where T : class;
        void Save();
    }
}
