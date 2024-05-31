namespace PetManager.Core.Model.Exception
{
    public class MinimumStressLevelException : System.Exception
    {
        public MinimumStressLevelException()
        {
        }

        public MinimumStressLevelException(string message)
            : base(message)
        {
        }

        public MinimumStressLevelException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
