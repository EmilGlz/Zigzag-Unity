using UnityEngine;
using DG.Tweening;
public class MapItem : MonoBehaviour, IPooledObject
{
    public bool isTouched;
    MapGenerator generator;
    private readonly float downAnimationHeight = 10f;
    private readonly float downAnimationDuration = 1f;
    private readonly float downAnimationDelay = 1.5f;
    private void Start()
    {
        generator = MapGenerator.Instance;
    }
    public void OnObjectSpawn()
    {
        isTouched = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTouched)
        {
            isTouched = true;
            generator.AddNewItem();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isTouched)
        {
            transform.DOMoveY(endValue: transform.position.y - downAnimationHeight, 
                duration: downAnimationDuration)
                .SetDelay(downAnimationDelay)
                .OnComplete(DownAnimationComplete);
        }
    }

    void DownAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
