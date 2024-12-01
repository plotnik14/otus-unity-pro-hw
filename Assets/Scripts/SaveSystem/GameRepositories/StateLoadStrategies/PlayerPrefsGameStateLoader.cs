using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem.GameRepositories.StateLoadStrategies
{
    public class PlayerPrefsGameStateLoader : IGameStateLoader
    {
        public Dictionary<string, string> ReadState(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var gameStateJson = PlayerPrefs.GetString(key);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(gameStateJson);
            }

            Debug.LogError("State was not loaded");
            return new Dictionary<string, string>();
        }

        public void WriteState(string key, Dictionary<string, string> data)
        {
            var gameStateJson = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(key, gameStateJson);
        }
    }
}