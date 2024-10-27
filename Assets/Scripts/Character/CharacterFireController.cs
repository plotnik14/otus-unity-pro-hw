using System;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterFireController : IDisposable, INonLazy
    {
        private readonly IInputSystem _input;
        private readonly WeaponComponent _characterWeapon;

        public CharacterFireController(IInputSystem input, WeaponComponent characterWeapon)
        {
            _input = input;
            _characterWeapon = characterWeapon;

            _input.OnFire += OnFire;
        }

        public void Dispose() => _input.OnFire -= OnFire;

        private void OnFire() => _characterWeapon.Fire(Vector2.up);
    }
}