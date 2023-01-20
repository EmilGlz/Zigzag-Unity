using System;
using UnityEngine;
public interface IPooledObject
{
    GameObject Instance { get; }
    void OnObjectSpawn();
    Action OnObjectFinish { get; set; }
}
