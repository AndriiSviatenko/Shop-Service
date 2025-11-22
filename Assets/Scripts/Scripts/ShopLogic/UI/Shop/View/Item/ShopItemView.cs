using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Button buyButton;

    private string _offerId;
    public string GetOfferId() => _offerId;
    public void SetIcon(Sprite sprite) => 
        icon.sprite = sprite;

    public void SetTitle(string text) => 
        title.SetText(text);

    public void SetPrice(string value) =>
        price.SetText(value.ToString());

    public void SetOfferId(string value) => 
        _offerId = value;

    public void Subscribe(UnityAction action) =>
        buyButton.onClick.AddListener(action);

    public void UnSubscribe(UnityAction action) => 
        buyButton.onClick.RemoveListener(action);
}
