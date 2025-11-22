using Game.Shop.Domain;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ShopPresenter : MonoBehaviour
{
    [SerializeField] private ShopView view;

    private ShopService _service;

    public void Init(ShopService service)
    {
        _service = service;
        Render();
    }

    public void Render()
    {
        view.Clear();

        var offers = _service.GetAvailableOffers(new string[] {"F"});

        foreach (var o in offers.OrderBy(o => o.BasePrice.Currency).ThenBy(o => o.BasePrice.Amount))
        {
            var v = view.AddItem();
            v.SetOfferId(o.OfferId);
            v.SetTitle(o.Item != null ? o.Item.DisplayName : o.OfferId);
            v.SetIcon(o.Item != null ? o.Item.Icon : null);

            var price = _service.GetPrice(o.OfferId);
            v.SetPrice($"{price.Amount} {price.Currency}");
            v.Subscribe(() => _ = OnBuyClickedAsync(v.GetOfferId()));
        }
    }

    private async Task OnBuyClickedAsync(string offerId)
    {
        var res = await _service.TryPurchaseAsync(offerId);

        if (res.IsSuccess)
            Debug.Log($"Purchased {offerId}");

        else 
            Debug.LogWarning($"Purchase failed {offerId}: {res.Status} {res.Error}");

        Render();
    }
}