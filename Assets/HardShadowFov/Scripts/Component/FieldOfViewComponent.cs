using UnityEngine;
using UnityEngine.Assertions;

namespace m8t
{
    public class FieldOfViewComponent : MonoBehaviour, IEditorableFoV
    {
        [Header("Mandatory injections")]
        [SerializeField, InterfaceField(typeof(IWatcher))]
        private Object _watcher;
        private IWatcher watcher;

        [Header("General Settings")]
        [SerializeField] private int rayCount = 3;
        [SerializeField, Range(0.0f, 360.0f)] private float fovAngle = 360f;
        [SerializeField] private float width = 2f;
        [SerializeField] private float offset = 2f;
        [SerializeField] private LayerMask layerMask;

        // adjacent components
        private MeshFilter meshFilter;

        void Start()
        {
            Assert.IsNotNull(_watcher);
            watcher = _watcher as IWatcher;

            meshFilter = GetComponent<MeshFilter>();
            Assert.IsNotNull(meshFilter);

            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[rayCount + 1];
            Vector2[] uv = new Vector2[rayCount + 1];
            int[] triangles = new int[(rayCount - 1) * 3];

            for (int triangleIndex = 0, i = 0; i < (rayCount - 1); ++i)
            {
                triangles[triangleIndex++] = 0;
                triangles[triangleIndex++] = i + 1;
                triangles[triangleIndex++] = i + 2;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;

            meshFilter.mesh = mesh;
        }

        private void LateUpdate()
        {
            Vector2 center = watcher.Position();
            Vector2 aim = watcher.AimDirection();

            Vector3[] vertices = meshFilter.mesh.vertices;
            var center3d = new Vector3(center.x, center.y, 0);

            vertices[0] = new Vector3(0, 0, 0);
            aim = Rotate(aim, -fovAngle / 2);
            for (int i = 1; i <= rayCount; ++i)
            {
                Vector3 debugPoint;
                Vector3 point;
                var hit = Physics2D.Raycast(center, aim, width, layerMask);
                if (hit.collider != null)
                {
                    debugPoint = new Vector3(hit.point.x, hit.point.y, 0);

                    point = new Vector3(hit.point.x, hit.point.y, 0) - center3d;
                    point += point.normalized * offset;
                }
                else
                {
                    debugPoint = center3d + new Vector3(aim.x, aim.y, 0) * width;
                    point = new Vector3(aim.x, aim.y, 0) * width;
                    point += point.normalized * offset;
                }
                Debug.DrawLine(center3d, debugPoint, Color.red);
                vertices[i] = point;
                aim = Rotate(aim, fovAngle / (rayCount - 1));
            }
            meshFilter.mesh.vertices = vertices;
        }
        private Vector2 Rotate(Vector2 dir, float angle)
        {
            var res = Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(dir.x, dir.y, 0);
            return new Vector3(res.x, res.y, 0);
        }

        public Vector3 CircleSectorCenter()
        {
            return transform.position;
        }

        public float CircleSectorRadius()
        {
            return width;
        }

        public float CircleSectorAngleDegree()
        {
            return fovAngle;
        }
    }

    public interface IWatcher
    {
        Vector2 Position();

        Vector2 AimDirection();
    }

    public interface IEditorableFoV
    {
        Vector3 CircleSectorCenter();

        float CircleSectorRadius();

        float CircleSectorAngleDegree();
    }
}

