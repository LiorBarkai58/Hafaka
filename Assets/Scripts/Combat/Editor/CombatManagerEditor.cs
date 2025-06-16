// Put this script inside an "Editor" folder
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntityCombatManager), true)]
public class EntityCombatManagerEditor : Editor
{
    private SerializedProperty maxHealth;
    private EntityCombatManager manager;

    private void OnEnable()
    {
        manager = (EntityCombatManager)target;
        maxHealth = serializedObject.FindProperty("maxHealth");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Entity Combat Settings", EditorStyles.boldLabel);

        EditorGUILayout.Slider(maxHealth, 1f, 500f, new GUIContent("Max Health"));

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(true);
        float normalizedHealth = manager.CurrentHealth / manager.MaxHealth;
        Rect rect = GUILayoutUtility.GetRect(18, 18);
        EditorGUI.ProgressBar(rect, normalizedHealth, $"Current Health: {manager.CurrentHealth:0}/{manager.MaxHealth}");
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        // Test Damage Input
        if (Application.isPlaying)
        {
            EditorGUILayout.LabelField("Test Tools", EditorStyles.boldLabel);
            if (GUILayout.Button("Take 10 Damage"))
            {
                manager.TakeDamage(new DamageDealtArgs { damage = 10 });
            }

            // Force the inspector to repaint every frame in play mode
            Repaint();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
