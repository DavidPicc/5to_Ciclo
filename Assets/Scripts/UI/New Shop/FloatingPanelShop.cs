using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingPanelShop : MonoBehaviour
{
    private const float DISTANCE_TO_NODE = 85f;

    [Header("Labels")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI priceText;

    private void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        RectTransform _rect = GetComponent<RectTransform>();

        Vector3[] worldCorners = new Vector3[4];

        GameObject.FindGameObjectWithTag("shopMenu").GetComponent<RectTransform>().GetWorldCorners(worldCorners);

        float halfWidht = _rect.rect.width / 2;
        float halfHeight = _rect.rect.height / 2;

        //Horizontal
        _rect.anchoredPosition = new Vector2(DISTANCE_TO_NODE, _rect.anchoredPosition.y);
        
        if(transform.position.x > worldCorners[2].x - halfWidht)
        {
            _rect.anchoredPosition = new Vector2(-DISTANCE_TO_NODE, _rect.anchoredPosition.y);
        }

        //Vertical
        if(transform.position.y < worldCorners[0].y + halfHeight)
        {
            float toAdd = worldCorners[0].y + halfHeight - transform.position.y;
            _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x, _rect.anchoredPosition.y + toAdd);
        } else if (transform.position.y > worldCorners[2].y - halfHeight)
        {
            float toSubstract = transform.position.y - (worldCorners[2].y - halfHeight);
            _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x, _rect.anchoredPosition.y - toSubstract);
        }
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }

    public void SetPrice(string price)
    {
        priceText.text = price;
    }
}