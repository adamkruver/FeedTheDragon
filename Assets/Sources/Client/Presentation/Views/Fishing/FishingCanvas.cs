using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishingCanvas : MonoBehaviour
    {
        [field: SerializeField] public FishingLineCursor LineCursor { get; private set; }
        [field: SerializeField] public FishingCatchCursor CatchCursor { get; private set; }
    }
}