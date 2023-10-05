using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class Fishing : MonoBehaviour
    {
        [field: SerializeField] public FishingCharacter FishingCharacter { get; private set; }
        [field: SerializeField] public FishingCanvas FishingCanvas { get; private set; }
        [field: SerializeField] public RectTransform UnderWaterRect { get; private set; }
    }
}