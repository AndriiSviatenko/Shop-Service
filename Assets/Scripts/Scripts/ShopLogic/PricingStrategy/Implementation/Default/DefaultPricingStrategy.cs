using Game.Shop.Domain;

public class DefaultPricingStrategy : IPricingStrategy
{
    public Price GetEffectivePrice(OfferDefinition offer, IWallet wallet) =>
        offer.BasePrice;
}
