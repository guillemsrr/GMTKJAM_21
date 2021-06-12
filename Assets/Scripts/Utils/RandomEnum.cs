using System;
using Random = UnityEngine.Random;

namespace Utils
{
    public class RandomEnum
    {
        public static T GetRandomFromEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            int random = Random.Range(0, values.Length);
            return (T)values.GetValue(random);
        }
    }
}