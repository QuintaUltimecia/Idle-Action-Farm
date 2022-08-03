using UnityEngine;
using System.Collections;

namespace IdleActionFarm.Scripts.Plants
{
    public class Garden : MonoBehaviour, ICut
    {
        [Header("Features")]
        [SerializeField] 
        private float _timeToGrow = 10f;

        [Header("Links")]
        [SerializeField] 
        private GrassCuttedUp _grassCuttedUp;
        [SerializeField] 
        private ParticleSystem _particleSystem;
        [SerializeField] 
        private GrassBlock _grassBlockPrefab;

        private Transform _transform;

        private PoolObjects<GrassBlock> _grassBlockPool;
        private int _poolCount = 40;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _grassBlockPool = new PoolObjects<GrassBlock>(_grassBlockPrefab, _poolCount, _transform);
            _grassBlockPool.AutoExpand = true;
        }

        private void GrowUp()
        {
            _grassCuttedUp.ResetCut();
        }

        private IEnumerator GrowUpRoutine()
        {
            yield return new WaitForSeconds(_timeToGrow);
            GrowUp();
        }

        public void Cut()
        {
            if (_grassCuttedUp.IsCutted == true)
                return;

            _grassCuttedUp.Cut();
            _particleSystem.Play();
            StartCoroutine(GrowUpRoutine());

            GrassBlock newBlock = _grassBlockPool.GetFreeElement(_transform.position + new Vector3(0, 0.50f, 0));
            newBlock.Jump();
        }

        public bool IsCutted()
        {
            return _grassCuttedUp.IsCutted;
        }
    }
}
