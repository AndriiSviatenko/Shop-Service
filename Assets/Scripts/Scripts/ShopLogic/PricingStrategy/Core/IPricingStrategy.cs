public interface IPricingStrategy
{
    Price GetEffectivePrice(OfferDefinition offer, IWallet wallet);
}