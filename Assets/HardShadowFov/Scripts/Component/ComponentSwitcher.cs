using System.Collections.Generic;
using UnityEngine;

namespace m8t
{
    public class ComponentSwitcher : MonoBehaviour
    {
        [Header("--------- General Settings --------- ")]
        [SerializeField] private List<Behaviour> componentsToDisable;
        [SerializeField] private List<GameObject> gameObjectsToDeactivate;
        [SerializeField] private List<GameObject> enableCollisions;
        [SerializeField] private List<GameObject> disableCollisions;

        public void Switch()
        {
            foreach (var beh in componentsToDisable)
            {
                beh.enabled = false;
            }

            foreach (var monobeh in gameObjectsToDeactivate)
            {
                monobeh.SetActive(false);
            }

            foreach (var bone in enableCollisions)
            {
                bone.GetComponent<Rigidbody2D>().simulated = true;
                bone.GetComponent<Collider2D>().enabled = true;

                if (bone.GetComponent<HingeJoint2D>() != null)
                {
                    bone.GetComponent<HingeJoint2D>().enabled = true;
                }
            }

            foreach (var bone in disableCollisions)
            {
                bone.GetComponent<Rigidbody2D>().simulated = false;
                bone.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}

