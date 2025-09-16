using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "ReadObject", menuName = "Readable Objects/ReadObject")]
    public class ReadObject : ScriptableObject {
        public string text;
    }
}
