using Sources.Client.Presentation.Views.SpawnPoints;
using UnityEditor;
using UnityEngine;

namespace Sources.Editor
{
    [CustomEditor(typeof(SpawnerBase))]
    public class CustomSpawnPointEditor : UnityEditor.Editor
    {
        public class SpawnPointEditor
        {
            [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
            public static void RemderCustomGizmo(SpawnerBase spawner, GizmoType gizmo)
            {
                Gizmos.color = spawner.Color;
                Gizmos.DrawSphere(spawner.Position, spawner.Size);
            }
        }
    }
}