using Newtonsoft.Json;

namespace Game.Scripts.Money.Upgrades
{
    public class Upgrade
    {
        [JsonProperty]
        public int Level { get; private set; }

        public Upgrade(int level = 1)
        {
            Level = level;
        }

        public void AddLevel()
        {
            Level++;
        }
    }
}