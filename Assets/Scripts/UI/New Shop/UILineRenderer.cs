using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : MonoBehaviour
{
    public Image lineImage;
    public float lineWidth;

    RectTransform rect;

    public void DrawLine(RectTransform fromTransform, RectTransform toTransform)
    {
        rect = GetComponent<RectTransform>();
        rect.localScale = Vector3.one;
        transform.SetAsFirstSibling();

        rect.localPosition = (fromTransform.anchoredPosition + toTransform.anchoredPosition) / 2;
        Vector3 dif = (fromTransform.anchoredPosition - toTransform.anchoredPosition);
        rect.sizeDelta = new Vector3(dif.magnitude, lineWidth);
        rect.rotation = Quaternion.Euler(new Vector3(0, 0, 180 * Mathf.Atan(dif.y / dif.x) / Mathf.PI));
    }

    public void SetColor(Color color)
    {
        lineImage.color = color;
    }
}
