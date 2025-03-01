using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Core.Systems
{
    public class DealDamageSystem : ReactiveSystem<GameEntity>
    {
        public DealDamageSystem(IContext<GameEntity> context) : base(context) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.Damage, GameMatcher.Health)
                .NoneOf(GameMatcher.Destroyed)
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasDamage && entity.hasHealth;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                int currentHealth = entity.health.value;
                int damageValue = Mathf.RoundToInt(entity.damage.value);
                int newHealth = currentHealth - damageValue;

                entity.RemoveDamage();

                if (newHealth <= 0)
                {
                    entity.RemoveHealth();
                    entity.isDestroyed = true;
                    return;
                }

                entity.ReplaceHealth(newHealth);
            }
        }
    }
}