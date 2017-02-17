using UnityEditor;
using UnityEngine;
using System.IO;

namespace CustomEditorTools
{
    public static class CustomEditorTools
    {
        #region uGUI Tools
        [MenuItem("CustomTools/uGUI/Anchors to Corners %[")]
        static void AnchorsToCorners()
        {
            foreach (Transform transform in Selection.transforms)
            {
                RectTransform t = transform as RectTransform;
                RectTransform pt = Selection.activeTransform.parent as RectTransform;

                if (t == null || pt == null) return;

                Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                                                    t.anchorMin.y + t.offsetMin.y / pt.rect.height);
                Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                                                    t.anchorMax.y + t.offsetMax.y / pt.rect.height);

                t.anchorMin = newAnchorsMin;
                t.anchorMax = newAnchorsMax;
                t.offsetMin = t.offsetMax = new Vector2(0, 0);
            }
        }

        [MenuItem("CustomTools/uGUI/Corners to Anchors %]")]
        static void CornersToAnchors()
        {
            foreach (Transform transform in Selection.transforms)
            {
                RectTransform t = transform as RectTransform;

                if (t == null) return;

                t.offsetMin = t.offsetMax = new Vector2(0, 0);
            }
        }

        [MenuItem("CustomTools/uGUI/Mirror Horizontally Around Anchors %;")]
        static void MirrorHorizontallyAnchors()
        {
            MirrorHorizontally(false);
        }

        [MenuItem("CustomTools/uGUI/Mirror Horizontally Around Parent Center %:")]
        static void MirrorHorizontallyParent()
        {
            MirrorHorizontally(true);
        }

        static void MirrorHorizontally(bool mirrorAnchors)
        {
            foreach (Transform transform in Selection.transforms)
            {
                RectTransform t = transform as RectTransform;
                RectTransform pt = Selection.activeTransform.parent as RectTransform;

                if (t == null || pt == null) return;

                if (mirrorAnchors)
                {
                    Vector2 oldAnchorMin = t.anchorMin;
                    t.anchorMin = new Vector2(1 - t.anchorMax.x, t.anchorMin.y);
                    t.anchorMax = new Vector2(1 - oldAnchorMin.x, t.anchorMax.y);
                }

                Vector2 oldOffsetMin = t.offsetMin;
                t.offsetMin = new Vector2(-t.offsetMax.x, t.offsetMin.y);
                t.offsetMax = new Vector2(-oldOffsetMin.x, t.offsetMax.y);

                t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);
            }
        }

        [MenuItem("CustomTools/uGUI/Mirror Vertically Around Anchors %'")]
        static void MirrorVerticallyAnchors()
        {
            MirrorVertically(false);
        }

        [MenuItem("CustomTools/uGUI/Mirror Vertically Around Parent Center %\"")]
        static void MirrorVerticallyParent()
        {
            MirrorVertically(true);
        }

        static void MirrorVertically(bool mirrorAnchors)
        {
            foreach (Transform transform in Selection.transforms)
            {
                RectTransform t = transform as RectTransform;
                RectTransform pt = Selection.activeTransform.parent as RectTransform;

                if (t == null || pt == null) return;

                if (mirrorAnchors)
                {
                    Vector2 oldAnchorMin = t.anchorMin;
                    t.anchorMin = new Vector2(t.anchorMin.x, 1 - t.anchorMax.y);
                    t.anchorMax = new Vector2(t.anchorMax.x, 1 - oldAnchorMin.y);
                }

                Vector2 oldOffsetMin = t.offsetMin;
                t.offsetMin = new Vector2(t.offsetMin.x, -t.offsetMax.y);
                t.offsetMax = new Vector2(t.offsetMax.x, -oldOffsetMin.y);

                t.localScale = new Vector3(t.localScale.x, -t.localScale.y, t.localScale.z);
            }
        }
        #endregion uGUI Tools

        #region Clear Tools
        [MenuItem("CustomTools/Clear/PlayerPrefs Clear")]
        static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("CustomTools/Clear/Cache clear")]
        static void CleanCache()
        {
            Caching.CleanCache();
        }

        [MenuItem("CustomTools/Clear/Data path clear")]
        static void CleanDatapath()
        {
            string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
                Debug.Log("Deleted file :" + filePath);
            }
        }

        [MenuItem("CustomTools/Clear/DeleteEmptyFolders")]
        public static void DeleteEmptyFolders()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + "/");
            SearchForEmptyFolder(dirInfo);
            AssetDatabase.Refresh();
        }

        [MenuItem("CustomTools/Clear/Editor/CreateMaterial")]
        public static void CreateAssetBunldes()
        {
            string[] guids = Selection.assetGUIDs;
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                string extension = System.IO.Path.GetExtension(path);
                string materialPath = path.Substring(0, path.Length - extension.Length) + ".mat";
                var material = new Material(Shader.Find("Unlit With Shadows"));
                material.mainTexture = AssetDatabase.LoadAssetAtPath<Texture>(path);
                AssetDatabase.CreateAsset(material, materialPath);
            }
        }

        //********** Private methods **********//

        /// <summary>
        /// Search for an empty folder and deletes it.
        /// </summary>
        /// <param name="dirInfo">Dir info.</param>
        static void SearchForEmptyFolder(DirectoryInfo dirInfo)
        {
            DirectoryInfo[] dirInfos = dirInfo.GetDirectories("*.*");
            if (dirInfos.Length != 0)
            {
                foreach (DirectoryInfo tempDirInfo in dirInfos)
                {
                    SearchForEmptyFolder(tempDirInfo);
                }
            }

            AssetDatabase.Refresh();

            if (dirInfo.GetDirectories("*.*").Length == 0)
            {
                if (dirInfo.GetFiles("*.*").Length == 0)
                {
                    UnityEditor.FileUtil.DeleteFileOrDirectory(dirInfo.FullName);
                    bool result = UnityEditor.FileUtil.DeleteFileOrDirectory(dirInfo.FullName + ".meta");
                    Debug.Log(result + "Deleted Dir : " + dirInfo.FullName);
                    return;
                }
            }
        }
        #endregion Clear Tools
    }
}
