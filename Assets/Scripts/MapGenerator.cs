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
    public Queue<Transform> pathCorners;
    public Transform firstItem;
    [SerializeField] int startItemCount = 15;
    Transform currentItem;
    ObjectPooler _pooler;
    bool lastItemWasRight;
    private float itemWidth;
    private void Start()
    {
        _pooler = ObjectPooler.Instance;
        ResetItems();
    }

    public void ResetItems()
    {
        pathCorners = new Queue<Transform>();
        itemWidth = firstItem.GetChild(1).transform.localScale.x;
        pathCorners.Enqueue(firstItem);
        AddFirstItems();
    }

    public void AddFirstItems()
    {
        currentItem = firstItem;
        for (int i = 0; i < startItemCount; i++)
        {
            AddNewItem();
        }
    }

    public void AddNewItem()
    {
        var nextItemIsRight = Random.Range(0, 2) % 2 == 0;
        if (lastItemWasRight != nextItemIsRight) // direction changed
        {
            if (currentItem != firstItem)
            {
                pathCorners.Enqueue(currentItem);
            }
        }
        if (nextItemIsRight)
        {
            currentItem = _pooler.SpawnFromPool("mapItem", currentItem.position + Vector3.right * itemWidth).transform;
        }
        else
        {
            currentItem = _pooler.SpawnFromPool("mapItem", currentItem.position + Vector3.forward * itemWidth).transform;
        }
        lastItemWasRight = nextItemIsRight;
    }
}
