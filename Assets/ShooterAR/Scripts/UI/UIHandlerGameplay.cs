using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ShooterAR
{
    public class UIHandlerGameplay : MonoBehaviour
    {
        public Canvas mainUICanvas;
        public Image[] playerLife;
        public Text scoreText;
        public GameOverPanel gameOverPanel;
        public GameObject warning;
        public LoadScene loadScene;

        void Awake ()
        {
        }

	    // Use this for initialization
	    void Start () {
           
        }

        public void GameOver ()
        {
            warning.SetActive(false);
            gameOverPanel.gameObject.SetActive(true);
            gameOverPanel.ScoreStats(GameManager.Instance.GameScore.GetScoreDetail());
        }

        public void UpdateScore(int totalScore)
        {
            scoreText.text = "Score: " + totalScore.ToString();
        }
		   
        public void UpdatePlayerLife(int life)
        {
            for (int i = 0; i < playerLife.Length; i++)
            {
                playerLife[i].enabled = (i < life);
            }
        }
    }
}
