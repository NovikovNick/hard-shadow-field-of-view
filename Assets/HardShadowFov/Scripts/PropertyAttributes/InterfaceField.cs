using UnityEngine;

namespace m8t
{
    public class InterfaceField : PropertyAttribute
    {

        public System.Type requiredType { get; private set; }

        public InterfaceField(System.Type type)
        {
            requiredType = type;
        }
    }
}
