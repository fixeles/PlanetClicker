using Game.Scripts.StarSystem.Stars;
using UnityEngine;

namespace Game.Scripts.StarSystem.Common
{
    public class StarSystem : MonoBehaviour
    {
        private SpaceBody _star;

        private void Start()
        {
            _star = new Star();
        }

        private void Update()
        {
            _star.Update();
        }
    }
}