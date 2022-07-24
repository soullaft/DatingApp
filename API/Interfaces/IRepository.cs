namespace API.Interfaces
{
    /// <summary>
    /// Represents default Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, new()
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Get all <see cref="T"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();
    }
}
