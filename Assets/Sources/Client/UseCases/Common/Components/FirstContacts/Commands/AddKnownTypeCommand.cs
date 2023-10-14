using System;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.FirstContacts.Commands
{
    public class AddKnownTypeCommand : ComponentUseCaseBase<FirstContactComponent>
    {
        public AddKnownTypeCommand(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public void Handle(int id, Type type) =>
            GetComponent(id).AddKnownType(type);
    }
}