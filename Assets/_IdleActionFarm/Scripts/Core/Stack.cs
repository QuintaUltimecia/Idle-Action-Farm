using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace IdleActionFarm.Scripts
{
    public class Stack : MonoBehaviour
    {
        [SerializeField] 
        private int _stackMax = 40;

        private int _stackAmount = 0;
        private float _offsetY = 0.1f;
        private float _offsetRotateX = 3f;
        private float _rotateDuration = 0.4f;

        private Transform _transform;
        private PlayerContoller _playerController;

        private Vector3 _positionOffset;
        private Vector3 _targetRotate;

        private bool _isMove = false;

        private Tweener _moveTweener;

        private List<GrassBlock> _grassBlockPool = new List<GrassBlock>();

        public delegate void CollectStackHandler();
        public event CollectStackHandler OnCollectStack;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnDisable()
        {
            _playerController.OnMoveEvent -= delegate (bool value)
            {
                OnMoveEvent(value);
            };
        }

        private void Start()
        {
            _positionOffset = new Vector3(0, _offsetY, 0);
            _targetRotate = new Vector3(_offsetRotateX, 0, 0);

            OnCollectStack.Invoke();
        }

        private void Update()
        {
            if (_isMove == true)
                Rotate();
        }

        private void OnMoveEvent(bool value) =>
            _isMove = value;

        private void Rotate()
        {
            if (_moveTweener != null)
                return;

            _moveTweener = _transform.DOLocalRotate(_targetRotate, _rotateDuration).SetEase(Ease.Linear).OnComplete(ResetRotate);
        }

        private void ResetRotate()
        {
            _moveTweener = _transform.DOLocalRotate(Vector3.zero, _rotateDuration).SetEase(Ease.Linear).OnComplete(() => _moveTweener = null);
        }

        public Vector3 GetNextPosition(GrassBlock value)
        {
            CollectStack();
            _grassBlockPool.Add(value);
            return Vector3.zero + (_positionOffset * _stackAmount);
        }

        public void CollectStack()
        {
            _stackAmount++;
            OnCollectStack.Invoke();
        }

        public void SellStack(Transform targetTransform)
        {
            _stackAmount--;

            if (_stackAmount < 0)
                _stackAmount = 0;

            OnCollectStack.Invoke();
            _grassBlockPool[_transform.childCount - 1].ResetCollect(targetTransform);
            _grassBlockPool.RemoveAt(_transform.childCount - 1);
        }

        public GrassBlock GetCurrentGrassBlock()
        {
            return _grassBlockPool[_grassBlockPool.Count - 1];
        }

        public void InitPlayerController(PlayerContoller value)
        {
            _playerController = value;

            _playerController.OnMoveEvent += delegate (bool value)
            {
                OnMoveEvent(value);
            };
        }

        public Transform Transform { get => _transform; }
        public Vector3 PositionOffset { get => _positionOffset; }
        public int StackAmount { get => _stackAmount;}
        public int StackMax { get => _stackMax; }
    }
}