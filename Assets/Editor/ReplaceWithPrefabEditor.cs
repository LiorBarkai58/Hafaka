using UnityEngine;
using UnityEditor;

public class ReplaceWithPrefabEditor : EditorWindow
{
    GameObject prefabToUse;
    bool fixNegativeScale = false;

    [MenuItem("Tools/Replace With Prefab")]
    static void Init()
    {
        ReplaceWithPrefabEditor window = (ReplaceWithPrefabEditor)EditorWindow.GetWindow(typeof(ReplaceWithPrefabEditor));
        window.titleContent = new GUIContent("Replace With Prefab");
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Replace Selected Objects", EditorStyles.boldLabel);

        prefabToUse = (GameObject)EditorGUILayout.ObjectField("Prefab", prefabToUse, typeof(GameObject), false);
        fixNegativeScale = EditorGUILayout.Toggle("Convert -1 Scale to 1", fixNegativeScale);

        GUI.enabled = prefabToUse != null && Selection.gameObjects.Length > 0;
        if (GUILayout.Button("Replace Selected"))
        {
            ReplaceSelectedObjects();
        }
        GUI.enabled = true;
    }

    void ReplaceSelectedObjects()
    {
        if (prefabToUse == null)
        {
            Debug.LogError("No prefab selected!");
            return;
        }

        GameObject[] selectedObjects = Selection.gameObjects;

        Undo.RegisterCompleteObjectUndo(selectedObjects, "Replace With Prefab");

        foreach (GameObject obj in selectedObjects)
        {
            Vector3 pos = obj.transform.position;
            Quaternion rot = obj.transform.rotation;
            Vector3 originalScale = obj.transform.lossyScale;

            if (fixNegativeScale)
            {
                if (Mathf.Approximately(originalScale.x, -1f)) originalScale.x = 1f;
                if (Mathf.Approximately(originalScale.y, -1f)) originalScale.y = 1f;
                if (Mathf.Approximately(originalScale.z, -1f)) originalScale.z = 1f;
            }

            GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefabToUse, obj.scene);
            newObj.transform.position = pos;
            newObj.transform.rotation = rot;
            newObj.transform.SetParent(obj.transform.parent);
            newObj.transform.SetSiblingIndex(obj.transform.GetSiblingIndex());

            newObj.transform.localScale = originalScale;

            Undo.DestroyObjectImmediate(obj);
        }
    }
}
