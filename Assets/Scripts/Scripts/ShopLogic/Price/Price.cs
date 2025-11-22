using Game.Shop.Domain;
using System;

[Serializable]
public struct Price
{
    public CurrencyType Currency;
    public int Amount;
    public Price(CurrencyType currency, int amount)
    {
        Currency = currency;
        Amount = amount;
    }
    public override string ToString() => $"{Amount} {Currency}";
}