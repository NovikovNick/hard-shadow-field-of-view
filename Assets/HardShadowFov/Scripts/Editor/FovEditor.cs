using UnityEditor;
using UnityEngine;

namespace m8t
{
    [CustomEditor(typeof(FieldOfViewComponent))]
    public class FovEditor : Editor
    {
        private void OnSceneGUI()
        {
            var fov = (IEditorableFoV)target;
            var center = fov.CircleSectorCenter();
            var radius = fov.CircleSectorRadius();
            var angle = fov.CircleSectorAngleDegree();
            var dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * radius;

            Handles.color = Color.white;
            Handles.DrawWireArc(center, Vector3.forward, Vector3.up, angle, radius);
            Handles.DrawLine(center, center + Vector3.up * radius);
            Handles.DrawLine(center, center + dir);
        }
    }
}
