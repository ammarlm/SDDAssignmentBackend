namespace SDDAssignmentBackend.Context
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveChangesAsync();
    }
}
