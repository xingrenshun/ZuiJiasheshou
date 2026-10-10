using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

public class EditBarTool : MonoBehaviour
{
    [MenuItem("Tools/删除游戏存档")]
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        string path = Application.persistentDataPath;

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
            Debug.Log("已清空持久化数据目录: " + path);
        }
    }


    [MenuItem("Tools/移除无效脚本")]
    public static void RemoveAllMissingScripts()
    {
        if (!EditorUtility.DisplayDialog("警告",
            "此操作将删除项目中所有物体上的无效脚本，包括场景和预制体。无法撤销，请确保已备份！继续吗？",
            "继续", "取消"))
        {
            return;
        }

        try
        {
            EditorUtility.DisplayProgressBar("处理中", "正在扫描场景和预制体...", 0);

            // 处理所有场景
            string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");
            for (int i = 0; i < sceneGuids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(sceneGuids[i]);
                EditorUtility.DisplayProgressBar("处理场景", path, (float)i / sceneGuids.Length);
                Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                ProcessScene(scene);
                EditorSceneManager.SaveScene(scene);
            }

            // 处理所有预制体
            string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
            for (int i = 0; i < prefabGuids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(prefabGuids[i]);
                EditorUtility.DisplayProgressBar("处理预制体", path, (float)i / prefabGuids.Length);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null)
                {
                    ProcessPrefab(prefab);
                    EditorUtility.SetDirty(prefab);
                }
            }

            AssetDatabase.SaveAssets();
            EditorUtility.ClearProgressBar();
            Debug.Log("已删除所有无效脚本！");
        }
        catch (System.Exception e)
        {
            EditorUtility.ClearProgressBar();
            Debug.LogError($"操作失败: {e.Message}");
        }
    }

    private static void ProcessScene(Scene scene)
    {
        GameObject[] roots = scene.GetRootGameObjects();
        foreach (GameObject root in roots)
        {
            RemoveMissingFromGameObject(root);
        }
    }

    private static void ProcessPrefab(GameObject prefab)
    {
        foreach (Transform child in prefab.transform)
        {
            RemoveMissingFromGameObject(child.gameObject);
        }
        RemoveMissingFromGameObject(prefab);
    }

    private static void RemoveMissingFromGameObject(GameObject go)
    {
        // Unity 2019+ 推荐方法
        int count = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        if (count > 0)
        {
            Debug.Log($"删除了 {go.name} 上的 {count} 个无效脚本", go);
        }

        // 递归处理子物体
        foreach (Transform child in go.transform)
        {
            RemoveMissingFromGameObject(child.gameObject);
        }
    }
}