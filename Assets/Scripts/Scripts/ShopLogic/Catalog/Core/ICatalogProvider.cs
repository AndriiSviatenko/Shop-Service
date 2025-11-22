using System.Collections.Generic;

public interface ICatalogProvider
{
    IReadOnlyList<OfferDefinition> GetAllOffers();
    OfferDefinition GetOfferById(string offerId);
}