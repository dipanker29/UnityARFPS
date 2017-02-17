using UnityEngine;
using System.Collections;

namespace ShooterAR
{
    public class GameScore
    {
        public struct ScoreInfo
        {
            public int totalScore;
            public int totalKill;
            public int totalLife;
        }

        public int PlayerLife
        {
            get { return playerLife; }
        }

        public int TotalScore
        {
            get { return totalScore; }
        }

        private int playerLife = 3;
        private int totalScore = 0;
        private int totalKill = 0;
        
        public void EnemyKilled ()
        {
            totalKill++;
            totalScore += 2;
        }

        public bool IsGameOver ()
        {
            playerLife--;
            UpdateScore(-5);
            if (playerLife <= 0)
            {
                //GameOver.....
                playerLife = 0;
                return true;
            }

            return false;
        }

        private void UpdateScore(int score)
        {
            totalScore += score;
            if (totalScore < 0)
            {
                totalScore = 0;
            }
        }

        public ScoreInfo GetScoreDetail ()
        {
            ScoreInfo info;
            info.totalScore = TotalScore;
            info.totalKill = totalKill;
            info.totalLife = PlayerLife;

            return info;
        }
    }
}
