using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor {
    public override void OnInspectorGUI()
    {
        PlayerController playerController = (PlayerController)target;
        
        // Draw the default inspector
        DrawDefaultInspector();
        
        if(GUILayout.Button("Reset state")){
            playerController.SetDefaultState();
        }
        
    }
}