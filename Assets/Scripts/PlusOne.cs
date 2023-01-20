using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class PlusOne : MonoBehaviour, IPooledObject
{
    private Color32 startColor = new Color32(255, 0, 213, 255);
    private readonly float animTime = 1f;
    private readonly float yOffset = Screen.height / 10; 
    public GameObject Instance { get => gameObject; }
    public void OnObjectSpawn()
    {
        var recttrm = GetComponent<RectTransform>();
        var txt = GetComponent<TextMeshProUGUI>();
        txt.color = startColor;
        recttrm.DOAnchorPosY(recttrm.anchoredPosition.y + yOffset, animTime);
        txt.DOColor(new Color(0, 0, 0, 0), animTime).OnComplete(()=> { 
            OnObjectFinish?.Invoke();
        });
    }
    public Action OnObjectFinish { get; set; }
}
