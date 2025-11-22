using Game.Shop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public sealed class ShopService
{
    private readonly ICatalogProvider _catalog;
    private readonly IWallet _wallet;
    private readonly IInventory _inventory;
    private readonly IPricingStrategy _pricing;


    public ShopService(
        ICatalogProvider catalog,
        IWallet wallet,
        IInventory inventory,
        IPricingStrategy pricing)
    {
        _catalog = catalog;
        _wallet = wallet;
        _inventory = inventory;
        _pricing = pricing;
    }

    public IReadOnlyList<OfferDefinition> GetAvailableOffers(string[] requiredTags = null)
    {
        var list = _catalog.GetAllOffers()
            .Where(o => requiredTags == null || requiredTags.All(tag => o.Tags != null && o.Tags.Contains(tag)))
            .ToList();
        return list;
    }

    public Price GetPrice(string offerId)
    {
        var offer = _catalog.GetOfferById(offerId);
        if (offer == null)
            throw new ArgumentException($"Offer '{offerId}' not found");

        return _pricing.GetEffectivePrice(offer, _wallet);
    }

    public async Task<PurchaseResult> TryPurchaseAsync(string offerId)
    {
        var offer = _catalog.GetOfferById(offerId);

        if (offer == null)
            return new PurchaseResult(PurchaseStatus.UnknownError, offerId, "Offer not found");

        if (!offer.Enabled)
            return new PurchaseResult(PurchaseStatus.NotActive, offerId);

        if (offer.Type == OfferType.NonConsumable && _inventory.Has(offer.Item.Id))
            return new PurchaseResult(PurchaseStatus.LimitReached, offerId, "Item already purchased");

        var price = _pricing.GetEffectivePrice(offer, _wallet);

        if (!_wallet.CanAfford(price))
            return new PurchaseResult(PurchaseStatus.NotEnoughCurrency, offerId);

        if (!_wallet.Spend(price, reason: $"shop:{offerId}"))
            return new PurchaseResult(PurchaseStatus.UnknownError, offerId, "Spend failed");

        Grant(offer, reason: $"shop:{offerId}");

        return new PurchaseResult(PurchaseStatus.Success, offerId);
    }

    private void Grant(OfferDefinition offer, string reason)
    {
        switch (offer.Type)
        {
            default:
                if (offer.Item != null)
                    _inventory.Add(offer.Item.Id, offer.Quantity, reason);
                break;
        }
    }
}