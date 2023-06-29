namespace AntiCorruption.Model.Exceptions
{
    public class DuplicatedRepositoryException : Exception
    {
        public DuplicatedRepositoryException(string message) : base(message) { }
    }
}
