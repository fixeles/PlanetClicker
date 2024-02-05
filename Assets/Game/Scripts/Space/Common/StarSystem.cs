using Game.Scripts.Space.Stars;
using UnityEngine;

namespace Game.Scripts.Space.Common
{
    public class StarSystem : MonoBehaviour
    {
        public Star Star;

        private void Update()
        {
            Star.Update();
        }
    }
}