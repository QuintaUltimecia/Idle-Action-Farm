using UnityEngine;

namespace IdleActionFarm.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerContoller _playerController;
        private CutController _cutController;

        private int MOVE = Animator.StringToHash("isMove");
        private int ATTACK = Animator.StringToHash("isAttack");

        private void Awake() =>
            _animator = GetComponent<Animator>();

        private void OnDisable()
        {
            _playerController.OnMoveEvent -= delegate (bool value)
            {
                Move(value);
            };

            _cutController.OnAttack -= delegate ()
            {
                Attack();
            };

            _cutController.ResetAttack -= delegate ()
            {
                ResetAttack();
            };
        }

        private void Move(bool value) => _animator.SetBool(MOVE, value);
        private void Attack() => _animator.SetTrigger(ATTACK);
        private void ResetAttack() => _animator.ResetTrigger(ATTACK);

        private void Cut() => _cutController.Cut();

        public void InitPlayerController(PlayerContoller value)
        {
            _playerController = value;

            _playerController.OnMoveEvent += delegate (bool value)
            {
                Move(value);
            };
        }

        public void InitCutController(CutController value)
        {
            _cutController = value;

            _cutController.OnAttack += delegate ()
            {
                Attack();
            };

            _cutController.ResetAttack += delegate ()
            {
                ResetAttack();
            };
        }
    }
}
