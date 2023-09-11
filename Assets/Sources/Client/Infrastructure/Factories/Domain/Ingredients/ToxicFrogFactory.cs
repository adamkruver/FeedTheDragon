﻿using System;
using Sources.Client.Domain.Components;
using Sources.Client.Domain.Ingredients;

namespace Sources.Client.Infrastructure.Factories.Domain.Ingredients
{
    public class ToxicFrogFactory : IngredientFactoryBase
    {
        protected override void AddComponents(Ingredient ingredient, IngredientSpawnInfo spawnInfo)
        {
            if (ingredient.TryGetComponent(out PositionComponent position) == false)
                throw new Exception("Ingredient must have PositionComponent");
            
            SpeedComponent speed = new SpeedComponent(5f); // TODO: move to config
            
            ingredient.AddComponent(new DestinationComponent(spawnInfo.Position, position, speed));
            ingredient.AddComponent(speed);
        }
    }
}