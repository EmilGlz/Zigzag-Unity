using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static HashSet<IUpdateable> _table = new HashSet<IUpdateable>();

    void Start()
    {
        ProjectController.Instance = new ProjectController();
    }

    public static void Register(IUpdateable obj)
    {
        if (obj == null) throw new System.ArgumentNullException();

        _table.Add(obj);
    }

    public static void Unregister(IUpdateable obj)
    {
        if (obj == null) throw new System.ArgumentNullException();

        _table.Remove(obj);
    }

    void Update()
    {
        var e = _table.GetEnumerator(); //avoid gc by calling GetEnumerator and iterating manually
        while (e.MoveNext())
        {
            if (Time.frameCount % e.Current.interval == 0)
            {
                e.Current.Tick();
            }
        }
    }
}
