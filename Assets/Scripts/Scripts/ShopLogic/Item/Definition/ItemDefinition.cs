using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    [SerializeField] private string itemId;
    [SerializeField] private string displayName;
    [SerializeField] private Sprite icon;

    public ItemId Id => new(itemId);
    public string DisplayName => displayName;
    public Sprite Icon => icon;
}