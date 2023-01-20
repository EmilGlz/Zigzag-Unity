using UnityEngine;
using DG.Tweening;
public class MapItem : MonoBehaviour, IPooledObject
{
    public GameObject crystalObject;
    public bool isTouched;
    public bool hasCrystal;
    MapGenerator generator;
    private readonly float downAnimationHeight = 10f;
    private readonly float downAnimationDuration = 1f;
    private readonly float downAnimationDelay = 1f;
    private void Start()
    {
        generator = MapGenerator.Instance;
    }
    public void OnObjectSpawn()
    {
        hasCrystal = ProjectController.Instance.CanAddNewCrystal && Random.Range(0, 2) % 2 == 0;
        isTouched = false;
        crystalObject.SetActive(hasCrystal);
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
