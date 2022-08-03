using UnityEngine;
using DG.Tweening;

namespace IdleActionFarm.Scripts
{
    public class WalletImageUI : MonoBehaviour
    {
        private float _duration = 0.2f;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void Move(Vector3 position)
        {
            _transform.DOMove(position, _duration).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
        }
    }
}
