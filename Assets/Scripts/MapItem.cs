using UnityEngine;

public class MapItem : MonoBehaviour, IPooledObject
{
    public bool isTouched;
    MapGenerator generator;
    private void Start()
    {
        generator = MapGenerator.Instance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTouched)
        {
            isTouched = true;
            generator.AddNewItem();
        }
    }
    public void OnObjectSpawn()
    {
        isTouched = false;
    }
}
