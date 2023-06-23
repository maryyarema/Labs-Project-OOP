namespace UserManagementEF.UserManagementEF.DAL.Helpers.Contracts
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string? orderByString);
    }
}
