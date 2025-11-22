using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amount;

    private string _itemId;
    public string GetItemId() => _itemId;

    public void SetIcon(Sprite sprite) =>
        icon.sprite = sprite;

    public void SetAmount(string text) =>
        amount.SetText(text);

    public void SetItemId(string value) =>
        _itemId = value;
}
