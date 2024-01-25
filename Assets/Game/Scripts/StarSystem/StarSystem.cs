using System.Collections.Generic;
using SubLib.Extensions;
using UnityEngine;

namespace Game.Scripts.StarSystem
{
    public class StarSystem : MonoBehaviour
    {
        [SerializeField] private SpaceBody star;

        [SerializeField] private SpaceBody prefab;

        private HashSet<SpaceBody> _bodies = new();

        private void Start()
        {
            _bodies.Add(star);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var newBody = Instantiate(prefab);
                newBody.SetRandomValues();
                newBody.SetParentBody(_bodies.GetRandomElement());
                _bodies.Add(newBody);
            }
        }
    }
}