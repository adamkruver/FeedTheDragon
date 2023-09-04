using UnityEngine;

public class SignalHandler : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;

    public void Handle(MoveSignal signal)
    {
        // Прием с сервера
        _characterMovement.Move(signal.MoveDelta);
    }
}