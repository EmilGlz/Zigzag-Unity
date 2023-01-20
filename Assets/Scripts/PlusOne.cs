using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlusOne : MonoBehaviour, IPooledObject
{
    private Color32 startColor = new Color32(255, 0, 213, 255);
    private float animTime = 1f;
    private float yOffset = Screen.height / 10; 
    public void OnObjectSpawn()
    {
        var recttrm = GetComponent<RectTransform>();
        var txt = GetComponent<TextMeshProUGUI>();
        txt.color = startColor;
        recttrm.DOAnchorPosY(recttrm.anchoredPosition.y + yOffset, animTime);
        txt.DOColor(new Color(0, 0, 0, 0), animTime);
    }
}
