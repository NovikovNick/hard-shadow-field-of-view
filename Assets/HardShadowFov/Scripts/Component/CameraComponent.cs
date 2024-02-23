using UnityEngine;
using UnityEngine.Assertions;

namespace m8t
{
    interface ICameraObservable
    {
        Vector3 Target();
        float TargetVelocityByX();
    }

    public class CameraComponent : MonoBehaviour
    {
        [Header("Mandatory injections")]
        [SerializeField, InterfaceField(typeof(ICameraObservable))] Object _observable;
        private ICameraObservable observable;

        [Header("General Settings")]
        [SerializeField] float interpolationFactor = 0.0f;
        [SerializeField] bool useFixedUpdate = false;
        [SerializeField] float zDistance = 10.0f;

        [Header("Camera offset for moving target")]
        [SerializeField] float xOffset = 10.0f;
        [SerializeField] float offsetOnVelosity = 2.0f;

        private void Start()
        {
            Assert.IsNotNull(_observable);
            observable = _observable as ICameraObservable;
        }

        void FixedUpdate()
        {
            if (useFixedUpdate)
            {
                Interpolate(Time.fixedDeltaTime);
            }
        }

        void LateUpdate()
        {
            if (!useFixedUpdate)
            {
                Interpolate(Time.deltaTime);
            }
        }
        void Interpolate(float a_DeltaTime)
        {
            var target = observable.Target();
            var xVelocity = observable.TargetVelocityByX();

            if (xVelocity > offsetOnVelosity)
            {
                target.x += xOffset;
            }
            else if (xVelocity < -offsetOnVelosity)
            {
                target.x -= xOffset;
            }

            Vector3 diff = target + Vector3.back * zDistance - transform.position;
            transform.position += diff * interpolationFactor * a_DeltaTime;
        }
    }
}
