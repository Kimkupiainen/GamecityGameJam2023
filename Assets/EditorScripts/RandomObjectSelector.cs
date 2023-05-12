using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public class RandomizeModels : EditorWindow
{
    public string modelsFolder = "Assets/Models";

    [MenuItem("Tools/Randomize Models")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(RandomizeModels));
    }

    void OnGUI()
    {
        GUILayout.Label("Randomize Models", EditorStyles.boldLabel);

        modelsFolder = EditorGUILayout.TextField("Models Folder", modelsFolder);

        if (GUILayout.Button("Randomize Selected Objects"))
        {
            GameObject[] selectedObjects = Selection.gameObjects;

            foreach (GameObject obj in selectedObjects)
            {
                string[] modelFiles = Directory.GetFiles(modelsFolder, "*.fbx");
                string randomModelFile = modelFiles[Random.Range(0, modelFiles.Length)];

                GameObject modelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(randomModelFile);
                GameObject modelInstance = GameObject.Instantiate(modelPrefab, obj.transform.position, obj.transform.rotation, obj.transform);
            }
        }
    }
}
#endif