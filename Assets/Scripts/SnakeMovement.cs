using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SnakeMovement : MonoBehaviour
{
    public int boardWidth = 40;
    public int boardHeight = 20;

    public TextMeshProUGUI scoreText;

    public GameObject gameOverPanel;

    public TextMeshProUGUI finalScoreText;

    public List<Vector2Int> body = new List<Vector2Int>();

    public Vector2Int direccion = Vector2Int.right;
    private Queue<Vector2Int> directionQueue = new Queue<Vector2Int>();
    public float moveInterval = 0.15f;
    private float timer = 0f;

    public GameObject segmentPrefab;
    private List<GameObject> segmentObjects = new List<GameObject>();

    public GameObject foodPrefab;
    private Vector2Int foodPosition;
    private GameObject foodObject;

    private bool isGameOver;

    private int score = 0;


    void Start()
    {
        body.Add(new Vector2Int(5, 5));
        body.Add(new Vector2Int(4, 5));
        body.Add(new Vector2Int(3, 5));
        PlaceFood();
    }
    void UpdateRotation()
    {
        float angle = 0f;

        if (direccion == Vector2Int.right) angle = 0f;
        else if (direccion == Vector2Int.up) angle = 90f;
        else if (direccion == Vector2Int.left) angle = 180f;
        else if (direccion == Vector2Int.down) angle = 270f;
        else if (direccion == Vector2Int.down) angle = 270f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void RotateSegment(GameObject segment, Vector2Int current, Vector2Int previous)
    {
        Vector2Int diff = previous - current;

        float angle = 0f;

        if (diff.x != 0)
        {
            angle = 0f;
        }
        else if (diff.y != 0)
        {
            angle = 90f;
        }

        segment.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        HandleInput();
        timer += Time.deltaTime;

        if (timer >= moveInterval)
        {
            timer = 0f;
            Move();
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExtiGame()
    {
        Application.Quit();
    }


    void Move()
    {
        if (directionQueue.Count > 0)
        {
            direccion = directionQueue.Dequeue();
            UpdateRotation();
        }

        Vector2Int newHeadPosition = body[0] + direccion;

        if (newHeadPosition.x <0 || newHeadPosition.x > boardWidth || newHeadPosition.y <0 || newHeadPosition.y > boardHeight)
        {
            GameOver();
            return;
        }

        if (body.Contains(newHeadPosition))
        {
            GameOver();
            return;
        }

        bool hasEaten = newHeadPosition == foodPosition;
        body.Insert(0, newHeadPosition);
        if (!hasEaten)
        {
            body.RemoveAt(body.Count -1);
        }
        else
        {
            PlaceFood();
            score++;
            scoreText.text = "Score: " + score;
        }

        transform.position = new Vector3(body[0].x, body[0].y, 0);

        UpdateVisuals();
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Your score: " + score;
    }

    void HandleInput()
    {
        if (directionQueue.Count >= 2) return;

        Vector2Int lastDirection = directionQueue.Count > 0 ? directionQueue.Last() : direccion;
        Vector2Int newDirection = lastDirection;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame) newDirection = Vector2Int.up;
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) newDirection = Vector2Int.left;
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) newDirection = Vector2Int.right;
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) newDirection = Vector2Int.down;

        if (newDirection != lastDirection && newDirection + lastDirection != Vector2Int.zero)
        {
            directionQueue.Enqueue(newDirection);
        }
    }

    void UpdateVisuals()
    {
        while (segmentObjects.Count < body.Count - 1)
        {
            GameObject newSegment = Instantiate(segmentPrefab);
            segmentObjects.Add(newSegment);
        }

        for (int i = 1; i < body.Count; i++)
        {
            segmentObjects[i - 1].transform.position = new Vector3(body[i].x, body[i].y, 0);
            RotateSegment(segmentObjects[i - 1], body[i], body[i - 1]);
        }
    }

    void PlaceFood()
    {
        Vector2Int newFoodPosition;

        do
        {
            int randomX = Random.Range(0, boardWidth + 1);
            int randomy = Random.Range(0, boardHeight + 1);
            newFoodPosition = new Vector2Int(randomX, randomy);
        }
        while (body.Contains(newFoodPosition));

        foodPosition = newFoodPosition;

        if (foodObject == null)
        {
            foodObject = Instantiate(foodPrefab);
        }

        foodObject.transform.position = new Vector3(foodPosition.x, foodPosition.y, 0);
    }
}
