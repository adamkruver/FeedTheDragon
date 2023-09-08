using UnityEngine;

namespace Sources.Client.Presentation.Views.Ingredients
{
    public class IngredientView : MonoBehaviour
    {
        public void SetParent(Transform parent) =>
            transform.SetParent(parent, false);
        
        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);
    }
}