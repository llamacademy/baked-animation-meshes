using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

public class AnimatedMeshEditorWindow : EditorWindow
{
    [MenuItem("Tools/Animated Mesh Creator")]
    public static void CreateEditorWindow()
    {
        EditorWindow window = GetWindow<AnimatedMeshEditorWindow>();
        window.titleContent = new GUIContent("Animated Mesh Editor");
    }

    private GameObject AnimatedModel;
    private int AnimationFPS = 30;
    private string Name;
    private bool Optimize = false;
    private bool DryRun = false;

    private const string BASE_PATH = "Assets/Animated Models/";

    private void OnGUI()
    {
        GameObject newAnimatedModel = EditorGUILayout.ObjectField("Animated Model", AnimatedModel, typeof(GameObject), true) as GameObject;
        if (newAnimatedModel != AnimatedModel)
        {
            Name = newAnimatedModel.name + " animations";
        }

        Animator animator = newAnimatedModel == null ? null : newAnimatedModel.GetComponentInChildren<Animator>();
        AnimatedModel = newAnimatedModel;

        Name = EditorGUILayout.TextField("Name", Name);
        AnimationFPS = EditorGUILayout.IntSlider("Animation FPS", AnimationFPS, 1, 100);
        Optimize = EditorGUILayout.Toggle("Optimize", Optimize);
        DryRun = EditorGUILayout.Toggle("Dry Run", DryRun);

        GUI.enabled = newAnimatedModel != null && animator.runtimeAnimatorController != null;
        if (GUILayout.Button("Generate ScriptableObjects"))
        {
            if (newAnimatedModel == null)
            {
                return;
            }

            GenerateFolderPaths(BASE_PATH + Name);
            EditorCoroutineUtility.StartCoroutine(GenerateModels(animator, DryRun), this);
            GenerateModels(animator, DryRun);
        }
        GUI.enabled = true;
        if (GUILayout.Button("Clear progress bar"))
        {
            EditorUtility.ClearProgressBar();
        }
    }

    private void GenerateFolderPaths(string FullPath)
    {
        string[] requiredFolders = FullPath.Split("/");
        string path = string.Empty;
        for (int i = 0; i < requiredFolders.Length; i++)
        {
            path += requiredFolders[i];
            if (!AssetDatabase.IsValidFolder(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }
    }

    private IEnumerator GenerateModels(Animator Animator, bool DryRun)
    {
        AnimatedMeshScriptableObject scriptableObject = CreateInstance<AnimatedMeshScriptableObject>();
        scriptableObject.AnimationFPS = AnimationFPS;

        Debug.Log($"Found {Animator.runtimeAnimatorController.animationClips.Length} clips. Creating SO with name \"{Name}\" with Animation FPS {AnimationFPS}");
        int clipIndex = 1;

        string parentFolder = "Assets/Animated Models/" + Name + "/";

        foreach (AnimationClip clip in Animator.runtimeAnimatorController.animationClips)
        {
            Debug.Log($"Processing clip {clipIndex}: \"{clip.name}\". Length: {clip.length:N4}.");
            EditorUtility.DisplayProgressBar("Processing Animations", $"Processing animation {clip.name} ({clipIndex} / {Animator.runtimeAnimatorController.animationClips.Length})", clipIndex / (float)Animator.runtimeAnimatorController.animationClips.Length);

            List<Mesh> meshes = new();
            AnimatedMeshScriptableObject.Animation animation = new();
            animation.Name = clip.name;
            float increment = 1f / AnimationFPS;
            Animator.Play(clip.name);

            for (float time = increment; time < clip.length; time += increment)
            {
                Debug.Log($"Processing {clip.name} frame {(time):N4}");
                Animator.Update(increment);
                if (DryRun)
                {
                    yield return new WaitForSeconds(increment);
                }
                foreach (SkinnedMeshRenderer skinnedMeshRenderer in AnimatedModel.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    Mesh mesh = new Mesh();
                    skinnedMeshRenderer.BakeMesh(mesh, true);

                    if (Optimize)
                    {
                        mesh.Optimize(); // maybe saves
                    }

                    if (!DryRun)
                    {
                        if (!AssetDatabase.IsValidFolder(parentFolder + clip.name))
                        {
                            Debug.Log("Path doesn't exist for clip. Creating folder: " + parentFolder + clip.name);
                            System.IO.Directory.CreateDirectory(parentFolder + clip.name);
                        }
                        AssetDatabase.CreateAsset(mesh, parentFolder + clip.name + $"/{time:N4}.asset");
                    }
                    meshes.Add(mesh);
                }
            }
            Debug.Log($"Setting {clip.name} to have {meshes.Count} meshes");
            animation.Meshes = meshes;
            scriptableObject.Animations.Add(animation);
            clipIndex++;
        }

        EditorUtility.ClearProgressBar();

        if (!DryRun)
        {
            Debug.Log($"Creating asset with {scriptableObject.Animations.Count} animations and {scriptableObject.Animations.Sum((item) => item.Meshes.Count)} meshes");
            EditorUtility.SetDirty(scriptableObject);
            AssetDatabase.CreateAsset(scriptableObject, BASE_PATH + Name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
