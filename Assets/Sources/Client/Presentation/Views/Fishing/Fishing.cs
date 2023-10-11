using Sources.Client.Presentation.Cameras.Types;
using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class Fishing : MonoBehaviour
    {
        [field: SerializeField] public FishingCharacter Character { get; private set; }
        [field: SerializeField] public FishingCanvas Canvas { get; private set; }
        [field: SerializeField] public RectTransform UnderWaterRect { get; private set; }
        [field: SerializeField] public FishingCamera Camera { get; private set; }
    }
}