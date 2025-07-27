using UnityEngine;
using UnityEditor;

namespace Boss.BossAttacks.Editor
{
    [CustomEditor(typeof(BossAbility), true)]
    public class BossAbilityEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI(); // Draw the default inspector

            // Add a button in the inspector
            BossAbility bossAbility = (BossAbility)target;

            GUILayout.Space(10); // Add some space for better layout

            if (GUILayout.Button("Activate Ability"))
            {
                // Call the Activate method
                bossAbility.Activate();
                Debug.Log($"{bossAbility.name} ability activated!");
            }
        }
    }
}