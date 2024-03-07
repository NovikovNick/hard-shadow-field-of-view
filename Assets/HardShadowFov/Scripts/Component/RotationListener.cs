using UnityEngine;

namespace m8t
{
    public class RotationListener : MonoBehaviour
    {
        [SerializeField] private float angle;
        [SerializeField] private float angleOffset;

        public void OnLookAt(Vector2 dir)
        {
            if (!enabled)
            {
                return;
            }

            transform.localScale = new Vector3(
            transform.localScale.x,
            dir.x < 0 ? -1 : 1,
            transform.localScale.z);

            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}

