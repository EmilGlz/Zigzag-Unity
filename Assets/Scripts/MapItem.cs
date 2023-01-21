using UnityEngine;
using DG.Tweening;
using System;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Core;

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
    TweenerCore<Vector3, Vector3, VectorOptions> seq;
    bool playerCrossed = false;
    private void Start()
    {
        generator = MapGenerator.Instance;
        startPos = transform.position;
    }
    public void OnObjectSpawn()
    {
        playerCrossed = false;
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
            seq = transform.DOMoveY(endValue: transform.position.y - downAnimationHeight,
                duration: downAnimationDuration)
                .SetDelay(downAnimationDelay)
                .OnComplete(DownAnimationComplete);
            seq.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isTouched)
        {
            var dif = collision.transform.position - transform.position;
            if (Math.Abs(dif.x) < 0.2f && Math.Abs(dif.z) < 0.2f && !playerCrossed)
            {
                playerCrossed = true;
                var res = MapGenerator.Instance.pathCorners.ToArray();
                if (res[0] == transform)
                {
                    Debug.Log("Update next destination from" + res[0].name + " to " + res[1].name);
                    PlayerMovement.Instance.nextCornerDestination = res[1];
                    MapGenerator.Instance.pathCorners.Dequeue();
                }
            }
        }
    }
    public void ResetPosition()
    {
        seq.Kill();
        transform.position = startPos;
        gameObject.SetActive(true);
    }
    void DownAnimationComplete()
    {
        gameObject.SetActive(false);
        OnObjectFinish?.Invoke();
    }
}