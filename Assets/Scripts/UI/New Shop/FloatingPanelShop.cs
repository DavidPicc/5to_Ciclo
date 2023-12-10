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
    [SerializeField] TextMeshProUGUI simpleDescriptionText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI priceGearText;
    [SerializeField] TextMeshProUGUI priceCoreText;
    public GameObject iconGear;
    public GameObject iconCore;
    public TextMeshProUGUI alternativeMessage;

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

    public void SetSimpleDescription(string description)
    {
        if (description != null) simpleDescriptionText.text = description;
        else simpleDescriptionText.text = "";
    }

    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }

    public void SetPrice(int gears, int cores, bool show)
    {
        if(show)
        {
            priceGearText.text = gears.ToString();
            priceCoreText.text = cores.ToString();
        }

        priceGearText.gameObject.SetActive(show);
        priceCoreText.gameObject.SetActive(show);
        iconGear.SetActive(show);
        iconCore.SetActive(show);
        alternativeMessage.gameObject.SetActive(!show);
    }

    public void SetAlternativeMessage(string alternativeMessage)
    {
        this.alternativeMessage.text = alternativeMessage;
    }
}
