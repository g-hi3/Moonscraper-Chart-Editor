using UnityEngine;

namespace Util
{
    public class DebugWrapper : IDebugWrapper
    {
        public static DebugWrapper Instance { get; } = new DebugWrapper();

        private DebugWrapper()
        {
        }

        public void Assert(bool condition, string message)
        {
            Debug.Assert(condition, message);
        }
    }
}