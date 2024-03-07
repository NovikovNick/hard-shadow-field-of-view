using UnityEngine;
using UnityEngine.Events;

namespace m8t
{
    public class CharacterEvents : MonoBehaviour
    {
        public static CharacterEvents Instance { get; private set; }

        // Channels
        public UnityEvent<Vector2> OnLookAt;
        public UnityEvent OnDeath;

        public void SendOnLookAt(Vector2 lookAt)
        {
            OnLookAt.Invoke(lookAt);
        }

        public void SendOnDeath()
        {
            OnDeath.Invoke();
        }

    }
}


