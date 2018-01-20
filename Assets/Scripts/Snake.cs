using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public class Snake : MonoBehaviour
    {
        // Current Movement Direction
        // (by default it moves to the right)
        Vector2 dir = Vector2.right;

        // Keep Track of Tail
        List<Transform> tail = new List<Transform>();

        // Did the snake eat something?
        bool ate = false;

        // Tail Prefab
        public GameObject tailPrefab;

        public GameObject gameManagerObject;
        private GameManager gameManager;

        // Use this for initialization
        IEnumerator Start()
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();

            // Start function WaitAndPrint as a coroutine
            yield return StartCoroutine("StartMoving");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                dir = Vector2.right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                dir = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                dir = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                dir = Vector2.down;
            }
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            // Food?
            if (coll.tag == "Food")
            {
                // Get longer in next Move call
                ate = true;

                // Remove the Food
                Destroy(coll.gameObject);

                gameManager.UpdateScore();
            }
            // Collided with Tail or Border
            else if (coll.tag == "Border" || coll.tag == "Tail")
            {
                gameManager.EndGame();
            }
        }

        void Move()
        {
            // Save current position (gap will be here)
            Vector2 v = transform.position;

            // Move head into new direction (now there is a gap)
            transform.Translate(dir);

            // Ate something? Then insert new Element into gap
            if (ate)
            {
                // Load Prefab into the world
                GameObject g = Instantiate(tailPrefab, v, Quaternion.identity);

                // Keep track of it in our tail list
                tail.Insert(0, g.transform);

                // Reset the flag
                ate = false;
            }
            // Do we have a Tail?
            else if (tail.Count > 0)
            {
                // Move last Tail Element to where the Head was
                tail.Last().position = v;

                // Add to front of list, remove from the back
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }

        IEnumerator StartMoving()
        {
            while (!gameManager.GameOver)
            {
                if (tail.Count > 0)
                {
                    float speed = (float)tail.Count / 200;
                    yield return new WaitForSeconds(.1f - speed);
                }
                else
                {
                    yield return new WaitForSeconds(.1f);

                }

                Move();
            }
        }
    }
}
