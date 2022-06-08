using UnityEngine;

public class CityBehaviour : LevelBehaviour
{
    private static CityBehaviour _instance = null;

    public static CityBehaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CityBehaviour>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.AddComponent<CityBehaviour>();
                    go.name = typeof(CityBehaviour).ToString();
                }
            }

            return _instance;
        }
    }
}
