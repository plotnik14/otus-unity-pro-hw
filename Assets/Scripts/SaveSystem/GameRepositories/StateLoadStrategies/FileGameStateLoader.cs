using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.GameRepositories.StateLoadStrategies
{
    public class FileGameStateLoader : IGameStateLoader
    {
        public Dictionary<string, string> ReadState(string key)
        {
            if (ES3.KeyExists(key))
            {
                return ES3.Load(key, new Dictionary<string, string>());
            }

            Debug.LogError("State was not loaded");
            return new Dictionary<string, string>();
        }

        public void WriteState(string key, Dictionary<string, string> data)
        {
            ES3.Save(key, data);
        }
    }
}