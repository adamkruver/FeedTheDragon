﻿using Sources.Client.Domain;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.NPCs;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using UnityEngine;

namespace Sources.Client.UseCases.NPCs.Common.Queries
{
    public class CreateQuestQuery
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IQuestFactory _questFactory;
        private readonly IEntityRepository _entityRepository;

        public CreateQuestQuery
        (
            IIdGenerator idGenerator,
            IQuestFactory questFactory,
            IEntityRepository entityRepository
        )
        {
            _idGenerator = idGenerator;
            _questFactory = questFactory;
            _entityRepository = entityRepository;
        }

        public int Handle()
        {
            int id = _idGenerator.GetId();

            Quest quest = _questFactory.Create(id);
            _entityRepository.Add(quest);

            return id;
        }
    }
}