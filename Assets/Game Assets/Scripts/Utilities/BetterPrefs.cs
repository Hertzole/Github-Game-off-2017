using UnityEngine;

namespace Hertzole.Github2017
{
    public class BetterPrefs
    {
        public static void SetVector3(string key, Vector3 value)
        {
            PlayerPrefs.SetString(key, value.x.ToString() + "," + value.y.ToString() + "," + value.z.ToString());
        }

        public static Vector3 GetVector3(string key)
        {
            string vectorString = PlayerPrefs.GetString(key);
            string[] values = vectorString.Split(',');
            float x = float.Parse(values[0]);
            float y = float.Parse(values[1]);
            float z = float.Parse(values[2]);

            return new Vector3(x, y, z);
        }
    }
}
