using UnityEngine;
using UnityEngine.AI;

namespace IdleActionFarm.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerContoller : MonoBehaviour
    {
        [Header("Features")]
        [SerializeField] 
        private float _moveSpeed;
        [SerializeField] 
        private float _rotateSpeed;
        [SerializeField] 
        private float _moveOffset;

        private NavMeshAgent _navMeshAgent;
        private Transform _transform;

        private float _startStickOffset;
        private float _targetStickOffset;
        private float _normalizedMagnitude;

        private Vector3 _startMousePosition;
        private Vector3 _newMousePosition;
        private Vector3 _movementDirection;

        private bool _isActive = false;

        public delegate void OnMoveHandler<in T>(T obj);
        public event OnMoveHandler<bool> OnMoveEvent;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if (_isActive == false)
                {
                    _movementDirection = Vector3.zero;
                }
                else
                {
                    if (Input.GetMouseButton(0))
                    {
                        _movementDirection = Vector3.zero;
                    }
                }
            }
        }

        private void Awake()
        {
            _transform = transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _startStickOffset = _moveOffset;
            _targetStickOffset = 0;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }

            if (Input.GetMouseButton(0))
            {
                _newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                _movementDirection = new Vector3(_newMousePosition.x - _startMousePosition.x, 0.0f,
                    _newMousePosition.y - _startMousePosition.y);

                _normalizedMagnitude = _movementDirection.magnitude / 100f;
                _normalizedMagnitude = Mathf.Clamp01(_normalizedMagnitude);

                _movementDirection = _movementDirection.normalized;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _movementDirection = Vector3.zero;
                _normalizedMagnitude = 0;
                _moveOffset = _startStickOffset;
            }


            if (IsActive == false) return;

            Move();
        }

        private void Move()
        {
            if (IsMove())
            {
                _navMeshAgent.Move(_movementDirection * _moveSpeed * Time.deltaTime);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(_movementDirection),
                    _rotateSpeed * Time.fixedDeltaTime);

                OnMoveEvent?.Invoke(true);
            }
            else OnMoveEvent?.Invoke(false);
        }

        private void OnDisable()
        {
            _movementDirection = Vector3.zero;
        }

        private void OnEnable()
        {
            IsActive = true;
        }

        public Vector3 MovementDirection { get => _movementDirection; }

        public bool IsMove()
        {
            if (_movementDirection != Vector3.zero && _movementDirection.magnitude > _moveOffset)
            {
                _moveOffset = _targetStickOffset;
                return true;
            }
            else return false;
        }
    }
}
