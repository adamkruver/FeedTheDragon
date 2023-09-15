using Sources.Client.Infrastructure.Builders.Apps;
using UnityEngine;

namespace Sources.Client.App
{
    [DefaultExecutionOrder(-1)]
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Bootstrapper");
            
            AppCore appCore = FindObjectOfType<AppCore>() ?? new AppCoreBuilder().Build();
        }
    }
}