using UnityEngine;

namespace IdleActionFarm.Scripts
{
    public class Ambar : MonoBehaviour
    {
        [SerializeField]
        private Transform _walletImageContainer;
        [SerializeField]
        private WalletImageUI _walletImagePrefab;

        private Transform _transform;
        private Camera _camera;

        private PoolObjects<WalletImageUI> _walletImagesPool;
        private int _poolCount = 40;

        private void Awake()
        {
            _transform = transform;
            _camera = Camera.main;
        }

        private void Start()
        {
            _walletImagesPool = new PoolObjects<WalletImageUI>(_walletImagePrefab, _poolCount, _walletImageContainer);
            _walletImagesPool.AutoExpand = true;
        }

        public void GetWalletImage()
        {
            WalletImageUI newWallet = _walletImagesPool.GetFreeElement(_camera.WorldToScreenPoint(_transform.position));
            newWallet.Move(_walletImageContainer.position);
        }
    }
}
