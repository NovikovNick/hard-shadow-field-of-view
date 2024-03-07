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
            var rotation = Quaternion.AngleAxis(-angle / 2, Vector3.forward) * fov.Rotation();
            var dir = Quaternion.AngleAxis(angle, Vector3.forward) * rotation * radius;

            Handles.color = Color.white;
            Handles.DrawWireArc(center, Vector3.forward, rotation, angle, radius);
            Handles.DrawLine(center, center + rotation * radius);
            Handles.DrawLine(center, center + dir);
        }
    }
}
