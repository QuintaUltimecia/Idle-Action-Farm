using UnityEngine;

namespace IdleActionFarm.Scripts.Player
{
    public class PlayerDIContainer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] 
        private PlayerContoller _playerController;
        [SerializeField] 
        private PlayerAnimatorController _playerAnimatorController;
        [SerializeField] 
        private Transform _cameraTransform;
        [SerializeField]
        private Stack _stack;
        [SerializeField]
        private CutController _cutController;

        private void Awake()
        {
            _playerAnimatorController.InitPlayerController(_playerController);
            _playerAnimatorController.InitCutController(_cutController);
            _stack.InitPlayerController(_playerController);
        }

        private void Start()
        {
            _cameraTransform.SetParent(null);
        }
    }
}
