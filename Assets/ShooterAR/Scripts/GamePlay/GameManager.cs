using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ShooterAR
{
    public class GameManager : MonoBehaviour
    {
        //k5Y129KB
        //XDS/NEQAKPt90/lCu9oBG8WDlyFlkIOWTlv+WxprJbuywEW0fo74k14EUUK3ygOUntMDuOJBWXZ9krO0X0mYyFuevM4uBjzQhsmssTs3u54eGgjCnr76Kibpd8xxhy6nEZj2e5p+si2ScHnW4e/r3w4zKZoVYvMgO95SHeE1ZQdTYWx0ZWRfX+gefv/hOlzrNevL0RK0t/jwuvxjpFH+QmdvJ7oWQ1PuHTbKmjkiYI9P4JJ4o2saR5YBnnWtuEJt8fGuMHAgAiaF5+lebdLMk/ycrcWWnApUwnuXOTq2jMC1/Zjga9d54kudZJC+oRAAF77hw5Hvkvm3sb2Yu+AUttLzJC+3X/T3tw7ZU+HJHOADN+8UmnQ7ZMe/ISTkIeKq9EV6kzIHXFqflhsh71bERbxfhOypjD2hTBUPwAiR5FuJZ4oUcJqbQ70LnWUSr1Sl+T7zhqDPaA7vlYe4sCq4hWrxljr1gENxuE0uLM1vbuRSte0rHqpwjnETcy/Z4tNk35yBEiWd1l00DVVpx6wZZG8QCA73M3b60bY4V6T/hTA5QhqAg7+8/wTrBk6SzDb0YciaQv1c3IFRIXU7JYrtem4iNu0hD49gE46HvAVvp1u+JCwvsuS30VUvRx50G1ebJgBgAP0aOBYc1pe44ERQed5pRRjtr+xfl8r12UC9DGs=

        #region INIT Instance
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        void InitInstance()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                DestroyImmediate(this);
            }
        }
        #endregion

        public GameObject player;
        public GameScore GameScore { get { return gameScore; } }
        private GameScore gameScore;
        public UIHandlerGameplay uIHandlerGameplay;
        public bool isGameOver = false;
        public bool isInFieldOfView = false;

        void Awake()
        {
            InitInstance();
        }

        // Use this for initialization
        void Start()
        {
            if (gameScore == null)
            {
                gameScore = new GameScore();
            }

            InitHUD();
        }

        public void EnemyKilled ()
        {
            gameScore.EnemyKilled();
            uIHandlerGameplay.UpdateScore(gameScore.TotalScore);
        }

        public void PlayerKilled()
        {
            if (gameScore.IsGameOver())
            {
                //GameOver.....
                isGameOver = true;
                uIHandlerGameplay.UpdateScore(gameScore.TotalScore);
                uIHandlerGameplay.GameOver();
            }
            else
            {
                //Decrese player Life
                uIHandlerGameplay.UpdatePlayerLife(gameScore.PlayerLife);
                uIHandlerGameplay.UpdateScore(gameScore.TotalScore);
            }
        }

        public void OnFieldOfVision (bool enter)
        {
            isInFieldOfView = enter;
            if (enter == false && isGameOver == false)
            {
                uIHandlerGameplay.warning.SetActive(true);
            }
            else
            {
                uIHandlerGameplay.warning.SetActive(false);
            }
        }

        void InitHUD ()
        {
            if (uIHandlerGameplay == null)
                uIHandlerGameplay = FindObjectOfType<UIHandlerGameplay>();

            uIHandlerGameplay.UpdatePlayerLife(gameScore.PlayerLife);
            uIHandlerGameplay.UpdateScore(gameScore.TotalScore);
        }
    }
}
