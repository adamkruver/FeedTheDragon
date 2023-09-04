using UnityEngine;

public class SignalBus : MonoBehaviour
{
    [SerializeField] private SignalHandler _signalHandler;

    public void Handle(MoveSignal signal)
    {
        // Отправка на сервер
        
        
        _signalHandler.Handle(signal);
    }
}