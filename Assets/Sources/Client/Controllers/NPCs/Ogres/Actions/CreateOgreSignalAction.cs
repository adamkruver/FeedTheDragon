using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.Controllers.NPCs.Ogres.Signals;
using Sources.Client.Domain.NPCs;
using Sources.Client.Domain.NPCs.Ogres;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.NPCs.Ogres.Queries;

namespace Sources.Client.Controllers.NPCs.Ogres.Actions
{
    public class CreateOgreSignalAction : ISignalAction<CreateOgreSignal>
    {
        private readonly CreateOgreQuery _createOgreQuery;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly OgreViewModelFactory _ogreViewModelFactory;
        private readonly Environment _environment;
        private readonly ISignalBus _signalBus;

        public CreateOgreSignalAction(
            ISignalBus signalBus,
            IBindableViewFactory bindableViewFactory,
            Environment environment,
            CreateOgreQuery createOgreQuery,
            OgreViewModelFactory ogreViewModelFactory
        )
        {
            _createOgreQuery = createOgreQuery;
            _bindableViewFactory = bindableViewFactory;
            _ogreViewModelFactory = ogreViewModelFactory;
            _environment = environment;
            _signalBus = signalBus;
        }

        public void Handle(CreateOgreSignal signal)
        {
            int id = _createOgreQuery.Handle(signal.Position);

            IBindableView view = _bindableViewFactory.Create(_environment.View["NPC"], nameof(Ogre));
            IViewModel viewModel = _ogreViewModelFactory.Create(id);
            
            view.Bind(viewModel);
            
            _signalBus.Handle(new CreateQuestSignal(id, 5)); //todo to level config
        }
    }
}