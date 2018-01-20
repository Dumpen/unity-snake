using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject GameOverUI;

        public GameObject ScoreTextUI;

        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                ScoreTextUI.GetComponent<Text>().text = Score.ToString();
            }
        }

        public bool GameOver { get; private set; }

        private void Start()
        {
            Score = 0;
        }

        public void UpdateScore()
        {
            Score++;
        }

        public void EndGame()
        {
            GameOver = true;
            GameOverUI.SetActive(true);
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}
