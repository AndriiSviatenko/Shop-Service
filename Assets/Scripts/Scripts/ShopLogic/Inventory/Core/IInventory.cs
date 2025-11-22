using System;
using System.Collections.Generic;

public interface IInventory
{
    event Action<string, int> ChangeItemCount;
    int GetCount(ItemId item);
    void Add(ItemId item, int quantity, string reason = null);
    bool Consume(ItemId item, int quantity, string reason = null);
    bool Has(ItemId item, int minQuantity = 1);
    IReadOnlyDictionary<string, int> Items { get; }
}