using UnityEngine;
using UnityEditor;

namespace BUDDYWORKS.AvatarDynamics
{  
    public class PrefabSpawner : MonoBehaviour
    {
        // VRCF Prefab definitions
        static string prefabAD_VRCF = "3f33503981c19274aae0d177a7b628f9";
        static string VRCF_Path = "Packages/com.vrcfury.vrcfury";

        // Toolbar Menu
        [MenuItem("BUDDYWORKS/Avatar Dynamics/Spawn Prefab... [VRCFury]", false, 0)]
        [MenuItem("GameObject/BUDDYWORKS/Avatar Dynamics/Spawn Prefab... [VRCFury]", false, 0)]
        private static void SpawnPE()
        {
            SpawnPrefab(prefabAD_VRCF);
            EditorUtility.DisplayDialog("BUDDYWORKS Avatar Scene", "Prefab spawned!\nMake sure to adjust the position of the contacts inside the prefab!", "Done");
        }

        // Enable or disable menu items dynamically

        [MenuItem("BUDDYWORKS/Avatar Dynamics/Spawn Prefab... [VRCFury]", true)]
        [MenuItem("GameObject/BUDDYWORKS/Avatar Dynamics/Spawn Prefab... [VRCFury]", true)]
        private static bool ValidateSpawnAD()
        {
            return AssetDatabase.IsValidFolder(VRCF_Path) != false;
        }

        // Prefab Spawner
        private static void SpawnPrefab(string guid)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError("Prefab with GUID " + guid + " not found.");
                return;
            }

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            GameObject selectedObject = Selection.activeGameObject;

            if (prefab == null)
            {
                Debug.LogError("Failed to load prefab with GUID " + guid + " at path " + prefabPath);
                return;
            }

            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            if (selectedObject != null)
            {
                instantiatedPrefab.transform.parent = selectedObject.transform;
            }

            if (instantiatedPrefab != null)
            {
                EditorGUIUtility.PingObject(instantiatedPrefab);
            }
            else
            {
                Debug.LogError("Failed to instantiate prefab with GUID " + guid);
            }
        }
    }
}