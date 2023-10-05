using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishingCharacter : MonoBehaviour
    {
        [field: SerializeField] public FishingLine FishingLine { get; private set; }
    }
}