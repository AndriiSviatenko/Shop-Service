using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI walletText;

    public void SetValue(int value) => 
        walletText.SetText(value.ToString());
}
