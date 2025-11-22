using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class InventoryPresenter : MonoBehaviour
{
    [SerializeField] private InventoryView viewPrefab;
    [SerializeField] private Transform context;

    private List<InventoryView> _currentViews = new();

    private ICatalogProvider _catalogProvider;
    private IInventory _inventory;

    public void Init(IInventory inventory, ICatalogProvider catalogProvider)
    {
        _inventory = inventory;
        _catalogProvider = catalogProvider;

        _inventory.ChangeItemCount += TryUpdateView;
    }

    private void TryUpdateView(string id, int amount)
    {
        bool isHasView = false;
        foreach (var view in _currentViews)
        {
            if (view.GetItemId().Equals(id))
            {
                view.SetAmount(amount.ToString());
                isHasView = true;
                return;
            }
        }

        if(isHasView == false)
        {
            var instance = Instantiate(viewPrefab, context);
            instance.SetItemId(id);
            instance.SetAmount(amount.ToString());
            var offer = _catalogProvider.GetOfferById(id);
            instance.SetIcon(offer.Item.Icon);
            _currentViews.Add(instance);
        }
    }

    public void Render()
    {
        foreach (var item in _inventory.Items)
        {
            var instance = Instantiate(viewPrefab, context);
            instance.SetItemId(item.Key);
            instance.SetAmount(item.Value.ToString());
            var offer = _catalogProvider.GetOfferById(item.Key);
            instance.SetIcon(offer.Item.Icon);
            _currentViews.Add(instance);
        }
    }
}
