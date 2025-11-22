using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Offer Definition")]
public class OfferDefinition : ScriptableObject
{
    [SerializeField] private string offerId;
    [SerializeField] private OfferType type;
    [SerializeField] private ItemDefinition item;
    [SerializeField] private int quantity = 1;
    [SerializeField] private Price basePrice;
    [SerializeField] private bool enabled = true;
    [SerializeField] private string[] tags;

    public string OfferId => offerId;
    public OfferType Type => type;
    public ItemDefinition Item => item;
    public int Quantity => quantity;
    public Price BasePrice => basePrice;
    public bool Enabled => enabled;
    public string[] Tags => tags;
}