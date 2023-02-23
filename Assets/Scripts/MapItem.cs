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
    [SerializeField] private MapItemScriptibleObject mapItemData;
    private Vector3 startPos;
    public Action OnObjectFinish { get; set; }
    public GameObject Instance { get => gameObject; }
    TweenerCore<Vector3, Vector3, VectorOptions> seq;
    MapGenerator generator;
    private bool _isReset = false;
    public bool isAnimating = false;
    private void Start()
    {
        generator = MapGenerator.Instance;
        startPos = transform.position;
    }
    public void OnObjectSpawn()
    {
        _isReset = false;
        isAnimating = false;
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
            isAnimating = true;
            seq = transform.DOMoveY(endValue: transform.position.y - mapItemData.downAnimationHeight,
                duration: mapItemData.downAnimationDuration)
                .SetDelay(mapItemData.downAnimationDelay)
                .OnComplete(DownAnimationComplete);
            seq.Play();
        }
    }

    public void ResetPosition()
    {
        _isReset = true;
        isTouched = false;
        seq.Kill();
        transform.position = startPos;
        gameObject.SetActive(true);
    }

    void DownAnimationComplete()
    {
        if (_isReset)
            ResetPosition();
        isAnimating = false;
        gameObject.SetActive(false);
        OnObjectFinish?.Invoke();
    }
}