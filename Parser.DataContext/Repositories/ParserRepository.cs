using Microsoft.EntityFrameworkCore;
using Parser.DataContext.Repositories.Interfaces;

namespace Parser.DataContext.Repositories
{
    public class ParserRepository : IRepository
    {
        private ParserDataContext _context = null;

        public ParserRepository(ParserDataContext context)
        {
            _context = context;
        }

        public void Delete<T>(Guid id) where T : class
        {
            DbSet<T> table = _context.Set<T>();
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            DbSet<T> table = _context.Set<T>();
            return table.ToList();
        }

        public void Insert<T>(T obj) where T : class
        {
            DbSet<T> table = _context.Set<T>();
            table.Add(obj);
        }

        public void Update<T>(T obj) where T : class
        {
            DbSet<T> table = _context.Set<T>();
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
