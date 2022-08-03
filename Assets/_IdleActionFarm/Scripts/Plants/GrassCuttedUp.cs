using UnityEngine;
using DG.Tweening;

namespace IdleActionFarm.Scripts.Plants
{
    public class GrassCuttedUp : MonoBehaviour
    {
        [SerializeField] 
        private float _jumpPower = 0.06f;
        [SerializeField] 
        private float _durationUp = 0.3f;
        [SerializeField] 
        private float _durationDown = 1f;
        [SerializeField] 
        private float _offsetDown = 5f;
        [SerializeField] 
        private float _offsetUp = 0.06f;

        private Transform _transform;

        private Vector3 _startPosition;
        private Vector3 _startRotation;
        private Vector3 _offset;

        private bool _isCutted = false;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _startPosition = _transform.position;
            _startRotation = _transform.eulerAngles;
            _offset = new Vector3(0, _offsetUp, 0);
        }

        private void CompleteCut()
        {
            _transform.DOMove(_transform.position + Vector3.down * _offsetDown, _durationDown).SetEase(Ease.Linear).OnComplete(AfterCompleteCut);
        }

        private void AfterCompleteCut()
        {
            _transform.localScale = new Vector3(1, 0, 1);
        }

        public void Cut()
        {
            if (_isCutted == true)
                return;
            else _isCutted = true;

            _transform.DOJump(_transform.position + _offset, _jumpPower,  1, _durationUp).SetEase(Ease.Linear).OnComplete(CompleteCut);
        }

        public void ResetCut()
        {
            _transform.position = _startPosition;
            _transform.rotation = Quaternion.Euler(_startRotation);
            _transform.DOScale(Vector3.one, _durationUp).SetEase(Ease.Linear).OnComplete(() => _isCutted = false);
        }

        public bool IsCutted { get => _isCutted; }
    }
}
