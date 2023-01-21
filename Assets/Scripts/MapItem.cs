using UnityEngine;
using DG.Tweening;
using System;

public class MapItem : MonoBehaviour, IPooledObject
{
    public GameObject crystalObject;
    public bool isTouched;
    public bool hasCrystal;
    MapGenerator generator;
    private readonly float downAnimationHeight = 10f;
    private readonly float downAnimationDuration = 1f;
    private readonly float downAnimationDelay = 1f;
    private Vector3 startPos;

    public Action OnObjectFinish { get; set; }
    public GameObject Instance { get => gameObject; }

    private void Start()
    {
        generator = MapGenerator.Instance;
        startPos = transform.position;
    }
    public void OnObjectSpawn()
    {
        hasCrystal = ProjectController.Instance.CanAddNewCrystal && UnityEngine.Random.Range(0, 2) % 2 == 0;
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
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isTouched)
        {
            if (collision.transform.position == transform.position)
            {
                var res = MapGenerator.Instance.pathCorners.ToArray();
                if (res[0] == transform)
                {
                    PlayerMovement.Instance.nextCornerDestination = res[1];
                    PlayerMovement.Instance.OnCornerPassed();
                    MapGenerator.Instance.pathCorners.Dequeue();
                }
            }
        }
    }
    public void ResetPosition()
    {
        transform.position = startPos;
        gameObject.SetActive(true);
    }
    void DownAnimationComplete()
    {
        gameObject.SetActive(false);
        OnObjectFinish?.Invoke();
    }
}