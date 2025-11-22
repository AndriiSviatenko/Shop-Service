public readonly struct PurchaseResult
{
    public readonly PurchaseStatus Status;
    public readonly string OfferId;
    public readonly string Error;

    public bool IsSuccess => Status == PurchaseStatus.Success;
    public PurchaseResult(PurchaseStatus status, string offerId, string error = null)
    {
        Status = status;
        OfferId = offerId;
        Error = error;
    }
}