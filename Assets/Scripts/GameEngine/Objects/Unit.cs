using UnityEngine;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class Unit : MonoBehaviour
    {
        public string ID
        {
            get => id;
        }

        public string Type
        {
            get => type;
        }

        public int HitPoints
        {
            get => hitPoints;
            set => hitPoints = value;
        }

        public Vector3 Position
        {
            get => this.transform.position;
        }
        
        public Vector3 Rotation
        {
            get => this.transform.eulerAngles;
        }

        [SerializeField]
        private string id; // Добавил поле, чтобы обеспечить хоть какой-то признак для сравнения объектов

        [SerializeField]
        private string type;
        
        [SerializeField]
        private int hitPoints;

        private void Reset()
        {
            this.type = this.name;
            this.hitPoints = 10;
        }
    }
}