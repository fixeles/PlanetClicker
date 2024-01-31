using Game.Scripts.DTO;
using Game.Scripts.StarSystem.Stars;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.StarSystem.Common
{
    public class StarSystem : MonoBehaviour
    {
        private const string SaveKey = "star_system_save_data";
        private Star _star;

        private void Start()
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                var json = PlayerPrefs.GetString(SaveKey);
                var dto = JsonConvert.DeserializeObject<StarDTO>(json);
                _star = new Star(dto);
                return;
            }
            _star = new Star();
        }

        private void Update()
        {
            _star.Update();

            if (Input.GetKeyDown(KeyCode.A))
            {
                string json = JsonConvert.SerializeObject(_star.DTO);
                PlayerPrefs.SetString(SaveKey, json);
                PlayerPrefs.Save();
            }
        }
    }
}