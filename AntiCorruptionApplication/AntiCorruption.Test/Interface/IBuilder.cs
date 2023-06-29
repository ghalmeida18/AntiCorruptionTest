namespace AntiCorruption.Test.Interface
{
    public interface IBuilder<T> where T : class
    {
        T Build();
    }
}
