using Game.Scripts.Money;
using Game.Scripts.Space.Common;
using Game.Scripts.Space.Stars;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Serialization
{
    public class SerializationHandler : MonoBehaviour
    {
        private const string SaveKey = "save_data";
        [SerializeField] private StarSystem starSystem;
        [SerializeField] private float autoSaveFrequency = 30;

        private float _autoSaveTimeLeft;

        private void Awake()
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                var json = GZip.Decompress(PlayerPrefs.GetString(SaveKey));
                var saveData = JsonConvert.DeserializeObject<SaveData>(json);
                Wallet.AddMoney(saveData.Money);

                starSystem.Star = new Star(saveData.StarDTO);
                return;
            }
            starSystem.Star = new Star();
        }

        private void Update()
        {
            _autoSaveTimeLeft -= Time.deltaTime;
            if (_autoSaveTimeLeft > 0)
                return;

            _autoSaveTimeLeft = autoSaveFrequency;
            Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void Save()
        {
            string compressedSaveData = GetCompressedSaveData();
            PlayerPrefs.SetString(SaveKey, compressedSaveData);
            PlayerPrefs.Save();
        }

        private string GetCompressedSaveData()
        {
            string rawJson = JsonConvert.SerializeObject(new SaveData(Wallet.CurrentMoney, starSystem.Star.DTO));
            return GZip.Compress(rawJson);
        }
    }
}