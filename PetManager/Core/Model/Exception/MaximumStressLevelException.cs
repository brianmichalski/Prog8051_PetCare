namespace PetManager.Core.Model.Exception
{
    public class MaximumStressLevelException : System.Exception
    {
        public MaximumStressLevelException()
        {
        }

        public MaximumStressLevelException(string message)
            : base(message)
        {
        }

        public MaximumStressLevelException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
