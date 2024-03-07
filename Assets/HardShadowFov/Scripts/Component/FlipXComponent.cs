using UnityEngine;

namespace m8t
{
    public class FlipXComponent : MonoBehaviour
    {

        public void OnLookAt(Vector2 dir)
        {
            if (!enabled)
            {
                return;
            }
            transform.localScale = new Vector3(
                            dir.x < 0 ? -1 : 1,
                            transform.localScale.y,
                            transform.localScale.z);
        }
    }
}

