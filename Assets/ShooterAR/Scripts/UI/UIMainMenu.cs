using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ShooterAR
{
    public class UIMainMenu : MonoBehaviour
    {
        public LoadScene loadScenePanel;
        // Use this for initialization
        void Start() {

        }

        public void OnClickPlayGameButton()
        {
            loadScenePanel.LoadNewScene("GamePlayAR");
        }

        public void OnClickPlaySampleButton()
        {
            loadScenePanel.LoadNewScene("Sample");
        }
    }
}
