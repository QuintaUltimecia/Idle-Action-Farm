using UnityEngine;
using DG.Tweening;

namespace IdleActionFarm.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class GrassBlock : MonoBehaviour, IBeingCollected
    {
        [SerializeField] 
        private int _price = 15;

        private float _force = 5f;
        private float _duration = 0.2f;

        private Rigidbody _rigidbody;
        private Transform _transform;
        private BoxCollider _boxCollider;
        private GameObject _gameObject;

        private bool _isCollected = false;
        private Transform _defaultParent;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            _boxCollider = GetComponent<BoxCollider>();
            _gameObject = gameObject;
        }

        private void Start()
        {
            _defaultParent = _transform.parent;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isCollected == true)
                return;

            if (collision.transform.TryGetComponent(out Ground ground))
            {
                _rigidbody.isKinematic = true;
                _boxCollider.isTrigger = true;
            }
        }

        public void Jump()
        {
            _rigidbody.AddForce(_transform.TransformDirection(Vector3.up) +
                                _transform.TransformDirection(Vector3.back) *
                                _force, ForceMode.Impulse);
        }

        public void Collect(Stack stack)
        {
            if (_isCollected == true)
                return;

            _isCollected = true;
            _transform.SetParent(stack.Transform);
            _transform.DOLocalMove(stack.GetNextPosition(this), _duration).SetEase(Ease.Linear);
            _transform.DORotate(stack.Transform.eulerAngles, _duration).SetEase(Ease.Linear).OnComplete(() => _transform.rotation = new Quaternion(0, 0, 0, 0));
        }

        public void ResetCollect(Transform targetMove)
        {
            _transform.DOMove(targetMove.position, _duration).SetEase(Ease.Linear).OnComplete(ResetCollectComplete);
            _transform.DORotate(targetMove.eulerAngles, _duration).SetEase(Ease.Linear).OnComplete(() => _transform.rotation = new Quaternion(0, 0, 0, 0));
        }

        private void ResetCollectComplete()
        {
            _isCollected = false;
            _transform.SetParent(_defaultParent);
            _transform.position = Vector3.zero;
            _transform.rotation = new Quaternion(0, 0, 0, 0);
            _gameObject.SetActive(false);
        }

        public int Price { get => _price; }
    }
}
