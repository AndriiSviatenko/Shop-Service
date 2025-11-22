using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Catalog")]
public class ScriptableCatalogProvider : ScriptableObject, ICatalogProvider
{
    [SerializeField] private OfferDefinition[] offers;

    public IReadOnlyList<OfferDefinition> GetAllOffers() =>
        offers?.Where(o => o != null).ToList();

    public OfferDefinition GetOfferById(string offerId) =>
        offers?.FirstOrDefault(o => o != null && o.OfferId == offerId);
}
