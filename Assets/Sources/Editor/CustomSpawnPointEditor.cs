using Sources.Client.Presentation.Views.SpawnPoints;
using UnityEditor;
using UnityEngine;

namespace Sources.Editor
{
    [CustomEditor(typeof(SpawnPointBase))]
    public class CustomSpawnPointEditor : UnityEditor.Editor
    {
        public class SpawnPointEditor
        {
            [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
            public static void RemderCustomGizmo(SpawnPointBase spawnPoint, GizmoType gizmo)
            {
                Gizmos.color = spawnPoint.Color;
                Gizmos.DrawSphere(spawnPoint.Position, spawnPoint.Size);
            }
        }
    }
}