using UnityEngine;

namespace Sources.Client.Utils
{
    public class LayerConstants
    {
        public const string Interactable = "Interactable";
        public const string TransparentFX = "TransparentFX";
        
        public static int InteractableMask => 1 << LayerMask.NameToLayer(Interactable);
        public static int TransparentFXMask => 1 << LayerMask.NameToLayer(TransparentFX);
    }
}