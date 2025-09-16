using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "ReadObject", menuName = "Scriptable Objects/ReadObject")]
    public class ReadObject : ScriptableObject {
        public string text;
    }
}
