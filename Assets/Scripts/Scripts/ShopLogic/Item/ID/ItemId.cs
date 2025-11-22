public readonly struct ItemId
{
    public readonly string Value;
    public ItemId(string value) => Value = value;
    public override string ToString() => Value;
}