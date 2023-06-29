using AntiCorruption.Model;

namespace AntiCorruption.Business.Interface
{
    public interface IListBrachExecutor
    {
        List<Branch> Execute(long id);
    }
}
