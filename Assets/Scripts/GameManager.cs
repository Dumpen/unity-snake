using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject GameOverUI;
        public int Score { get; set; }
        public bool GameOver { get; private set; }

        public void SetGameOver()
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
