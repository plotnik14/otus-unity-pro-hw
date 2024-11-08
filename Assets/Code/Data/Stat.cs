using System;
using UnityEngine;

namespace Data
{
    // [CreateAssetMenu(fileName = "Stat", menuName = "Data/New Stat")]
    [Serializable]
    public class Stat// : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _value;

        public string Name => _name;
        public int Value => _value;
    }
}