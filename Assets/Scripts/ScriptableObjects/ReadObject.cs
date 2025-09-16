using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "ReadObject", menuName = "Readable Objects/ReadObject")]
    public class ReadObject : ScriptableObject {
        [TextArea] public string text;
    }
}
