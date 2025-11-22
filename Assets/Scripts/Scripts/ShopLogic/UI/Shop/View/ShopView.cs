using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private ShopItemView itemPrefab;
    [SerializeField] private Transform contentRoot;

    private readonly List<ShopItemView> _spawned = new();

    public void Clear()
    {
        foreach (var view in _spawned)
        {
            if (view)
                Destroy(view.gameObject);
        }

        _spawned.Clear();
    }

    public ShopItemView AddItem()
    {
        var view = Instantiate(itemPrefab, contentRoot);
        _spawned.Add(view);
        return view;
    }
}