using System;
using System.Collections.Generic;

public class SimpleInventory : IInventory
{
    private readonly Dictionary<string, int> _items = new();

    public event Action<string, int> ChangeItemCount;

    public IReadOnlyDictionary<string, int> Items => _items;

    public bool Has(ItemId item, int minQuantity = 1) => GetCount(item) >= minQuantity;

    public int GetCount(ItemId item) =>
        _items.TryGetValue(item.Value, out var amount) ? amount : 0;

    public void Add(ItemId item, int quantity, string reason = null)
    {
        if (_items.ContainsKey(item.Value))
        {
            _items[item.Value] += quantity;
            ChangeItemCount?.Invoke(item.Value, _items[item.Value]);
        }
        else
        {
            _items[item.Value] = quantity;
            ChangeItemCount?.Invoke(item.Value, _items[item.Value]);
        }
    }
    public bool Consume(ItemId item, int quantity, string reason = null)
    {
        var c = GetCount(item);

        if (c < quantity)
            return false;

        _items[item.Value] = c - quantity;
        ChangeItemCount?.Invoke(item.Value, _items[item.Value]);
        return true;
    }
}