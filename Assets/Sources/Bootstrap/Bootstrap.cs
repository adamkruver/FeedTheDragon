using Sources.Character;
using Sources.Controllers;
using Sources.Infrastructure.Actions;
using Sources.Infrastructure.SignalBus;
using UnityEngine;

namespace Sources.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _characterMovement;

        private void Awake()
        {
            SignalHandler signalHandler = new SignalHandler();

            CharacterMoveSignalAction characterMoveSignalAction = new CharacterMoveSignalAction(_characterMovement);

            CharacterSignalController characterSignalController = new CharacterSignalController
            (
                new[] { characterMoveSignalAction }
            );
            
            signalHandler.Register(characterSignalController);

            SignalBus signalBus = new SignalBus(signalHandler);
            
            _characterMovement.Init(signalBus);
        }
    }
}