using UnityEngine;

public partial class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private ScriptableCatalogProvider catalog;
    [SerializeField] private ShopPresenter presenter;
    [SerializeField] private WalletPresenter walletPresenter;
    [SerializeField] private InventoryPresenter inventoryPresenter;

    private IWallet _wallet;
    private IInventory _inventory;
    private IPricingStrategy _pricing;
    private ShopService _shopService;

    private void Awake()
    {
        CreateWallet();
        CreateInventory();
        CreatePricing();

        CreateShopService();

        InitPresenter();
        InitWalletPresenter();
        InitInventoryPresenter();
    }

    private void CreateWallet() =>
        _wallet = FindAnyObjectByType<SimpleWallet>();

    private void CreateInventory() =>
        _inventory = new SimpleInventory();
    private void CreatePricing() =>
        _pricing = new DefaultPricingStrategy();

    private void CreateShopService() => 
        _shopService = new ShopService(
                catalog,
                _wallet,
                _inventory,
                _pricing
        );
    private void InitPresenter() => 
        presenter.Init(_shopService);
    private void InitWalletPresenter() =>
        walletPresenter.Init(_wallet);
    private void InitInventoryPresenter() =>
        inventoryPresenter.Init(_inventory, catalog);
}
