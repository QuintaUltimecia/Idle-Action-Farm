using UnityEngine;

namespace IdleActionFarm.Scripts
{
    public class CutController : MonoBehaviour
    {
        private ICut _cutter;

        public delegate void AttackHandler();
        public event AttackHandler OnAttack;
        public event AttackHandler ResetAttack;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICut cut))
            {
                if (cut.IsCutted() == true)
                    return;

                OnAttack.Invoke();
                _cutter = cut;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ICut cut))
            {
                if (_cutter == cut)
                {
                    _cutter = null;
                    ResetAttack.Invoke();
                }
            }
        }

        public void Cut()
        {
            if (_cutter != null)
            {
                _cutter.Cut();
                _cutter = null;
            }
        }
    }
}
