using System.Globalization;
using TMPro;
using UnityEngine;

namespace UI {
    public class FloatingPopup : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI floatingText;

        public void SetFloatingText(float value) {
            floatingText.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
