using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ShooterAR
{
    public class GameOverPanel : MonoBehaviour
    {
        public Text totalScore;
        public Text totalLife;
        public Text totalKill;

        // Use this for initialization
        void Start()
        {

        }

        public void ScoreStats (GameScore.ScoreInfo info)
        {
            totalScore.text = "Total Score: " + info.totalScore.ToString();
            totalLife.text = "Life left: " + info.totalLife.ToString();
            totalKill.text = "Eemy Killed: " + info.totalKill.ToString();
        }
        
        public void OnClickRestartButton()
        {
           GameManager.Instance.uIHandlerGameplay.loadScene.LoadNewScene("GamePlayAR");
        }

        public void OnClickQuitButton()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
