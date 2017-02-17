using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ShooterAR
{
    public class LoadScene : MonoBehaviour
    {
        public Transform indicator;
        public string levelName;

        public void LoadNewScene (string sceneName)
        {
            levelName = sceneName;
            gameObject.SetActive(true);
        }

        IEnumerator RotateLoadingImage ()
        {
            while (true)
            {
                if (indicator != null)
                {
                    indicator.transform.Rotate(0, 0, (400 * Time.deltaTime));
                }

                yield return null;
            }
        }

        IEnumerator Start()
        {
            StartCoroutine(RotateLoadingImage());

            AsyncOperation async = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
            yield return async;
        }
    }
}
