using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

namespace IdleActionFarm.Scripts
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] 
        private Wallet _wallet;
        [SerializeField]
        private TextMeshProUGUI _walletAmount;

        private Transform _transform;

        private int _walletCount;
        private float _collectDelay = 0.05f;

        private Coroutine _collectRotuine;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _wallet.OnUpdateWallet += delegate ()
            {
                UpdateWalletAmount();
            };
        }

        private void OnDisable()
        {
            _wallet.OnUpdateWallet += delegate ()
            {
                UpdateWalletAmount();
            };
        }

        private void Start()
        {
            UpdateWalletAmount();
        }

        private void UpdateWalletAmount()
        {
            if (_collectRotuine == null)
                _collectRotuine = StartCoroutine(UpdateWallet());
        }

        private IEnumerator UpdateWallet()
        {
            while(_walletCount != _wallet.WalletAmount)
            {
                _walletCount += 1;
                _walletAmount.text = _walletCount.ToString();
                _transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.04f).SetEase(Ease.Linear).OnComplete(() => _transform.localScale = Vector3.one);
                yield return new WaitForSeconds(_collectDelay);
            }

            _collectRotuine = null;
        }
    }
}
