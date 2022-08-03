using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IdleActionFarm.Scripts.UI
{
    public class StackUI : MonoBehaviour
    {
        [Header("Features")]
        [SerializeField]
        private Image _progressBar;
        [SerializeField]
        private TextMeshProUGUI _stackAmount;
        [Header("Links")]
        [SerializeField] 
        private Stack _stack;

        private void OnEnable()
        {
            _stack.OnCollectStack += delegate ()
            {
                UpdateProgressBar();
            };
        }

        private void OnDisable()
        {
            _stack.OnCollectStack -= delegate ()
            {
                UpdateProgressBar();
            };
        }

        private void UpdateProgressBar()
        {
            _progressBar.fillAmount = StackCast();
            _stackAmount.text = $"{_stack.StackAmount}/{_stack.StackMax}";
        }

        private float StackCast()
        {
            float value;

            try
            {
                value = (float)_stack.StackAmount / (float)_stack.StackMax;
            }
            catch
            {
                value = 0;
            }

            return value;
        }
    }
}
