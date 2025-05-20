#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;

[CreateAssetMenu(fileName = "AnimationStateExtractor", menuName = "Animation/StateExtractor")]
public class AnimationStateExtractor : ScriptableObject
{
    public AnimatorController controller;
    public string outputPath = "Assets/AnimationStates"; // Customize path

    [ContextMenu("Extract Animation States")]
    public void ExtractStates()
    {
        if (controller == null) return;

        
        string safeControllerName = controller.name.Replace(" ", "_");


        if (!AssetDatabase.IsValidFolder(outputPath))
            Directory.CreateDirectory(outputPath);

        if (!AssetDatabase.IsValidFolder($"{outputPath}/{safeControllerName}"))
            Directory.CreateDirectory($"{outputPath}/{safeControllerName}");
        foreach (var layer in controller.layers)
        {
            foreach (var state in layer.stateMachine.states)
            {
                AnimationStateSO stateSO = ScriptableObject.CreateInstance<AnimationStateSO>();
                stateSO.stateName = state.state.name;
                stateSO.hash = Animator.StringToHash($"{layer.name}.{state.state.name}");
                // Optional: get clip duration if assigned
                if (state.state.motion is AnimationClip clip)
                    stateSO.duration = clip.length;

                string safeName = state.state.name.Replace(" ", "_");
                AssetDatabase.CreateAsset(stateSO, $"{outputPath}/{safeControllerName}/{safeName}.asset");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Animation states extracted.");
    }
}
#endif
