using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Singleton
    private static MapGenerator _instance;
    public static MapGenerator Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    [SerializeField] Transform firstItem;
    [SerializeField] GameObject mapItemPrefab;
    Transform currentItem;
    [SerializeField] int startItemCount = 5;
    ObjectPooler _pooler;
    private void Start()
    {
        _pooler = ObjectPooler.Instance;
        currentItem = firstItem;
        for (int i = 0; i < startItemCount; i++)
        {
            AddNewItem();
        }
    }

    public void AddNewItem()
    {
        var nextItemIsRight = Random.Range(0, 2) % 2 == 0;
        if (nextItemIsRight)
        {
            currentItem = _pooler.SpawnFromPool("mapItem", currentItem.position + Vector3.right * 2, Quaternion.identity, 0f).transform;
        }
        else
        {
            currentItem = _pooler.SpawnFromPool("mapItem", currentItem.position + Vector3.forward * 2, Quaternion.identity, 0f).transform;
        }
    }
}
