using JetBrains.Annotations;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterFireController : MonoBehaviour
    {
        [SerializeField] private InputSystem _input;
        [SerializeField] private WeaponComponent _characterWeapon;

        [UsedImplicitly]
        private void Start() => _input.OnFire += OnFire;

        [UsedImplicitly]
        private void OnDestroy() => _input.OnFire -= OnFire;

        private void OnFire() => _characterWeapon.Fire(Vector2.up);
    }
}