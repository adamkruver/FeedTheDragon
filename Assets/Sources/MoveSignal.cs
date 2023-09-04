using UnityEngine;

public class MoveSignal
{
    public MoveSignal(Vector3 moveDelta)
    {
        MoveDelta = moveDelta;
    }

    public Vector3 MoveDelta { get; }
}