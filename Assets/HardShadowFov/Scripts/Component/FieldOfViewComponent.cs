using UnityEngine;
using UnityEngine.Assertions;

namespace m8t
{
    public class FieldOfViewComponent : MonoBehaviour, IEditorableFoV
    {
        [Header("--------- General Settings --------- ")]
        [SerializeField] private int rayCount = 3;
        [SerializeField, Range(0.0f, 360.0f)] private float fovAngle = 360f;
        [SerializeField] private float width = 2f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private bool drawDebug = false;

        // adjacent components
        private MeshFilter meshFilter;

        void Start()
        {
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
            Vector3 center3d = transform.position;
            Vector2 center = new Vector2(center3d.x, center3d.y);
            Vector2 aim = new Vector2(transform.right.x, transform.right.y);

            Vector3[] vertices = meshFilter.mesh.vertices;
            vertices[0] = new Vector3(0, 0, 0);
            aim = Rotate(aim, -fovAngle / 2);
            for (int i = 1; i <= rayCount; ++i)
            {
                var hit = Physics2D.Raycast(center, aim, width, layerMask);
                Vector3 point = hit.collider != null
                    ? new Vector3(hit.point.x, hit.point.y, 0)
                    : center3d + new Vector3(aim.x, aim.y, 0) * width;

                if (drawDebug)
                {
                    Debug.DrawLine(center3d, point, Color.red);
                }

                vertices[i] = transform.worldToLocalMatrix.MultiplyPoint(point);
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

        public Vector3 Rotation()
        {
            return transform.right;
        }
    }

    public interface IEditorableFoV
    {
        Vector3 CircleSectorCenter();

        float CircleSectorRadius();

        float CircleSectorAngleDegree();

        Vector3 Rotation();
    }
}
