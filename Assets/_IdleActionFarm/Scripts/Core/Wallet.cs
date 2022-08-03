using UnityEngine;

namespace IdleActionFarm.Scripts
{
    public class Wallet : MonoBehaviour
    {
        private int _walletAmount = 0;

        public delegate void UpdateWalletHandler();
        public event UpdateWalletHandler OnUpdateWallet;

        public void CollectWallet(int value)
        {
            WalletAmount += value;
            OnUpdateWallet.Invoke();
        }

        public void SpendWallet(int value)
        {
            WalletAmount -= value;
            OnUpdateWallet.Invoke();
        }

        public int WalletAmount { get => _walletAmount; set => _walletAmount = Mathf.Max(0, value); }
    }
}
