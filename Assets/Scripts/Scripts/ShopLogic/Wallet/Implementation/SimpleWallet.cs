using Game.Shop.Domain;
using System;
using UnityEngine;

public class SimpleWallet : MonoBehaviour, IWallet
{
    [SerializeField] private int soft = 500;
    [SerializeField] private int hard = 10;

    public event Action<CurrencyType, int> ChangeValue;

    public int Get(CurrencyType currency) =>
        currency switch
        {
            CurrencyType.Soft => soft,
            CurrencyType.Hard => hard,
            _ => 0
        };

    public bool CanAfford(Price price) => Get(price.Currency) >= price.Amount;

    public void Add(CurrencyType currency, int amount, string reason = null)
    {
        switch (currency)
        {
            case CurrencyType.Soft:
                soft += amount;
                ChangeValue?.Invoke(currency, soft);
                break;
            case CurrencyType.Hard:
                hard += amount;
                ChangeValue?.Invoke(currency, hard);
                break;
        }
    }

    public bool Spend(Price price, string reason = null)
    {
        if (!CanAfford(price))
            return false;

        switch (price.Currency)
        {
            case CurrencyType.Soft:
                soft -= price.Amount;
                ChangeValue?.Invoke(price.Currency, soft);
                break;
            case CurrencyType.Hard:
                hard -= price.Amount;
                ChangeValue?.Invoke(price.Currency, hard);
                break;
            default:
                return false;
        }
        return true;
    }
}