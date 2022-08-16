using Newtonsoft.Json;
using UnityEngine;

namespace Cars_5_5.Assets.Internal.Scripts.Data
{
    public struct PlayerCarData
    {
        public float MaxMotorTorgue;
        public float MaxTurnAngle;
        public float MaxBrakeTorgue;
        public float CarMass;
        public float Downforce;
    }

    public class PlayerTuningData
    {
        private const string PlayerCarDataKey = "PlayerCarData";

        public static PlayerCarData LoadPlayerCarData()
        {
            string jsonSource = PlayerPrefs.GetString(PlayerCarDataKey);
            if (jsonSource == string.Empty)
            {
                return CarSettings.Defaults;
            }    
            return JsonConvert.DeserializeObject<PlayerCarData>(jsonSource);
        }

        public static void SavePlayerCarData(PlayerCarData data)
        {
            PlayerPrefs.SetString(PlayerCarDataKey, JsonConvert.SerializeObject(data));
        }
    }
}
