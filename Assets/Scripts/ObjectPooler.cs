using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    #region Singleton
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        activeObjectsFromPool = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.name = pool.tag + i;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
            activeObjectsFromPool.Add(pool.tag, new Queue<GameObject>());
        }
    }
    #endregion
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, Queue<GameObject>> activeObjectsFromPool;
    public GameObject SpawnFromPool(string tag, Vector3 startPos, Transform parent = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} does not exist");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        if (parent != null)
            objectToSpawn.transform.SetParent(parent);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = startPos;
        poolDictionary[tag].Enqueue(objectToSpawn);
        activeObjectsFromPool[tag].Enqueue(objectToSpawn);
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
            pooledObject.OnObjectFinish = null;
            pooledObject.OnObjectFinish = () => { DequeueFromActivePool(tag); };
        }
        return objectToSpawn;
    }

    private void DequeueFromActivePool(string tag)
    {
        if (tag != "mapItem")
            return;
        activeObjectsFromPool[tag].Dequeue();
        //Debug.Log($"{obj.name} dequeued from active pool {tag}, count: {activeObjectsFromPool[tag].Count}");
    }

    public void DequeueAllObjectsFromPool(string tag)
    {
        var objArr = poolDictionary[tag].ToArray();
        for (int i = 0; i < objArr.Length; i++)
        {
            objArr[i].SetActive(false);
        }
        activeObjectsFromPool[tag] = new Queue<GameObject>();
    }
}
