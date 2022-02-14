using UnityEngine;

namespace Week05
{
    public class Logger : MonoBehaviour
    {
        public void LogInt(int value)
        {
            Debug.Log($"[{name}] (int) {value}");
        }

        public void LogFloat(float value)
        {
            Debug.Log($"[{name}] (float) {value}");
        }

        public void LogString(string value)
        {
            Debug.Log($"[{name}] (string) {value}");
        }
    }
}
