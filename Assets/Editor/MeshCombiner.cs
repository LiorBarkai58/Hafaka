using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MeshCombiner : MonoBehaviour
{
    [MenuItem("Tools/Combine Selected Meshes %#c")]
    static void CombineMeshes()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("Please select at least one GameObject.");
            return;
        }

        foreach (GameObject rootObj in selectedObjects)
        {
            MeshFilter[] meshFilters = rootObj.GetComponentsInChildren<MeshFilter>();
            Dictionary<Material, List<CombineInstance>> materialToMeshMap = new Dictionary<Material, List<CombineInstance>>();

            foreach (MeshFilter mf in meshFilters)
            {
                Renderer rend = mf.GetComponent<Renderer>();
                if (rend == null || rend.sharedMaterial == null || mf.sharedMesh == null) continue;

                Material mat = rend.sharedMaterial;

                if (!materialToMeshMap.ContainsKey(mat))
                    materialToMeshMap[mat] = new List<CombineInstance>();

                CombineInstance ci = new CombineInstance();
                ci.mesh = mf.sharedMesh;
                ci.transform = mf.transform.localToWorldMatrix;

                materialToMeshMap[mat].Add(ci);
            }

            GameObject combinedParent = new GameObject(rootObj.name + "_Combined");
            combinedParent.transform.position = rootObj.transform.position;

            foreach (var kvp in materialToMeshMap)
            {
                Material mat = kvp.Key;
                List<CombineInstance> list = kvp.Value;

                Mesh combinedMesh = new Mesh();
                combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                combinedMesh.CombineMeshes(list.ToArray(), true, true);

                // ✨ Ask user where to save the mesh
                string path = EditorUtility.SaveFilePanelInProject(
                    "Save Combined Mesh",
                    rootObj.name + "_" + mat.name + "_Combined.asset",
                    "asset",
                    "Choose where to save the combined mesh asset"
                );

                if (string.IsNullOrEmpty(path))
                {
                    Debug.LogWarning("Mesh save cancelled.");
                    continue;
                }

                AssetDatabase.CreateAsset(combinedMesh, path);
                AssetDatabase.SaveAssets();

                GameObject combinedChild = new GameObject("Mesh_" + mat.name);
                combinedChild.transform.parent = combinedParent.transform;

                MeshFilter mf = combinedChild.AddComponent<MeshFilter>();
                mf.sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);

                MeshRenderer mr = combinedChild.AddComponent<MeshRenderer>();
                mr.sharedMaterial = mat;
            }

            Debug.Log($"Combined mesh created and saved manually for: {rootObj.name}");
        }
    }
}
