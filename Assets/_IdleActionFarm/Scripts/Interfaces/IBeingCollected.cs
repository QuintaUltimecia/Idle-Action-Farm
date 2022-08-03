using UnityEngine;

namespace IdleActionFarm.Scripts
{
    public interface IBeingCollected
    {
        public void Collect(Stack stack);
    }
}
