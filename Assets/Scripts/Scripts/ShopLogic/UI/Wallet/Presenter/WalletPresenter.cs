using Game.Shop.Domain;
using UnityEngine;

public class WalletPresenter : MonoBehaviour
{
    [SerializeField] private WalletView softView;
    [SerializeField] private WalletView hardView;
    private IWallet _wallet;

    public void Init(IWallet wallet)
    {
        _wallet = wallet;
        _wallet.ChangeValue += UpdateView;

        InitRenderView();
    }

    public void InitRenderView()
    {
        softView.SetValue(_wallet.Get(CurrencyType.Soft));
        hardView.SetValue(_wallet.Get(CurrencyType.Hard));
    }

    public void UpdateView(CurrencyType currency, int value)
    {
        switch (currency)
        {
            case CurrencyType.None:
                break;
            case CurrencyType.Soft:
                softView.SetValue(value);
                break;
            case CurrencyType.Hard:
                hardView.SetValue(value);
                break;
            default:
                break;
        }
    }
}
