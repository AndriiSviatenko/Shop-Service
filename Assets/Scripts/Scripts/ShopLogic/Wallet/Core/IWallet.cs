using Game.Shop.Domain;
using System;

public interface IWallet
{
    event Action<CurrencyType, int> ChangeValue;
    int Get(CurrencyType currency);
    bool CanAfford(Price price);
    void Add(CurrencyType currency, int amount, string reason = null);
    bool Spend(Price price, string reason = null);
}
