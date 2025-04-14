using Microsoft.EntityFrameworkCore;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    int Add(T entity);
    void Update(T entity);
    void Delete(int id);
}
public class TaskRepository : IRepository<TaskEntity>
{
    private readonly TaskAPIDbContext _context;
    private readonly DbSet<TaskEntity> _dbSet;

    
    public TaskRepository(TaskAPIDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TaskEntity>();
    }
    public int Add(TaskEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = entity.CreatedAt;
        _dbSet.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }

    public void Delete(int id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }

    public void Update(TaskEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
        _context.SaveChanges();
    }
    
    public IEnumerable<TaskEntity> GetAll()
    {
        return _dbSet.AsEnumerable<TaskEntity>();
    }

     public TaskEntity GetById(int id) => _dbSet.Find(id);

}