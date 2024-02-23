using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;


namespace m8t
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private PlatformerMovement movement;

        void Start()
        {
            Assert.IsNotNull(movement);

            StartCoroutine(StartAI());
        }

        private IEnumerator StartAI()
        {
            int dir = 1;
            while (true)
            {
                yield return new WaitForSeconds(2f);

                for (int i = 0; i < 100; i++)
                {
                    movement.AddInput(dir, false);
                    yield return null;
                }

                movement.AddInput(0, true);
                yield return null;

                yield return new WaitForSeconds(0.5f);

                movement.AddInput(0, true);
                yield return null;

                dir = -dir;
            }
        }
    }
}
