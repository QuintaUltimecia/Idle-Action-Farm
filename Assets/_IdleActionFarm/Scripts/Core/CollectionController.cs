using UnityEngine;
using System.Collections;

namespace IdleActionFarm.Scripts.Core
{
    public class CollectionController : MonoBehaviour
    {
        [SerializeField] 
        private Stack _stack;
        [SerializeField] 
        private Wallet _wallet;

        private float _sellDelay = 0.5f;

        private Coroutine _sellRoutine;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IBeingCollected collected))
            {
                if (_stack.StackAmount == _stack.StackMax)
                    return;

                collected.Collect(_stack);
            }

            if (other.TryGetComponent(out Ambar ambar))
            {
                _sellRoutine = StartCoroutine(Sell(ambar.transform, ambar));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Ambar ambar))
            {
                if (_sellRoutine != null)
                    StopCoroutine(_sellRoutine);
            }
        }

        private IEnumerator Sell(Transform targetTransform, Ambar ambar)
        {
            while (_stack.StackAmount != 0)
            {
                yield return new WaitForSeconds(_sellDelay);
                _wallet.CollectWallet(_stack.GetCurrentGrassBlock().Price);
                _stack.SellStack(targetTransform);
                ambar.GetWalletImage();
            }
        }
    }
}
