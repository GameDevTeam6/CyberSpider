using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InputManager : MonoBehaviour
{
    public TMP_Text puzzleText; // Display the puzzle
    public TMP_InputField editor; // Player fills in the blank here
    public Button submitButton;
    public GameObject puzzlePanel; // GameObject containing all puzzle elements
    private bool puzzleSolved = false;
    [SerializeField] Canvas mainCanvas; // Assign your main canvas in the inspector
    [SerializeField] Camera mainCamera; // Assign your main camera in the inspector
    [SerializeField] float offset = 2.0f; // Offset to determine how far above the player the panel should appear
    private int currentPuzzleIndex = 0;
    private bool wasLastPuzzleSolved = false; // To track if the last puzzle was solved correctly
    public event Action OnPuzzleSolved;

    private string[] puzzles = {
        "def square(num):\n    return ______",
        "def concat_strings(str1, str2):\n    return _______"
    };

    private string[] answers = {
        "num * num",
        "str1 + str2"
    };


    private void Start()
    {
        // Ensure mainCamera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // This will get the camera with the tag "MainCamera"
            if (mainCamera == null)
            {
                Debug.LogError("Main camera not found. Please assign it manually in the inspector.");
            }
        }

        // Ensure mainCanvas is assigned
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
            if (mainCanvas == null)
            {
                Debug.LogError("Main canvas not found. Please assign it manually in the inspector.");
            }
        }

        puzzlePanel.SetActive(false); // Make sure the puzzle UI is hidden on start
    }

    public void ShowNextPuzzle()
    {
        if (currentPuzzleIndex < puzzles.Length)
        {
            puzzleText.text = puzzles[currentPuzzleIndex];
            editor.text = ""; // Clear previous input
        }
        else
        {
            Debug.Log("All puzzles solved!");
            puzzlePanel.SetActive(false); // Hide the puzzle UI when all puzzles are done
            // Handle completion logic here, if any
        }
    }

    public void OpenInputField(Action onSuccess)
    {
        if (currentPuzzleIndex <= puzzles.Length) // Check if there's another puzzle
        {
            Debug.Log("Showing puzzle panel");
            PositionPuzzlePanelAbovePlayer();
            puzzlePanel.SetActive(true); // Display the puzzle UI
            ShowNextPuzzle(); // Show the next puzzle
        }
        else
        {
            puzzlePanel.SetActive(false); // Hide the puzzle UI if no more puzzles
        }

        submitButton.onClick.AddListener(() => {
            if (editor.text == answers[currentPuzzleIndex])
            {
                Debug.Log("Correct!");
                puzzlePanel.SetActive(false);
                onSuccess?.Invoke();
                currentPuzzleIndex++;
            }
            else
            {
                Debug.Log("Incorrect, try again.");
            }
        });
    }


    Vector3 WorldToCanvasPosition(Canvas canvas, Camera camera, Vector3 worldPosition)
    {
        Vector3 screenPosition = camera.WorldToScreenPoint(worldPosition);
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, camera, out Vector2 localPoint);
        return localPoint;  // Note this is now a Vector2, which is often what's used for RectTransform positions
    }

    public void PositionPuzzlePanelAbovePlayer()
    {
        // Find the player GameObject. Ensure your player GameObject has the "Player" tag.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.LogError("Player object not found. Make sure it's tagged correctly.");
            return;
        }

        // Get the world position right above the player.
        Vector3 worldPositionAbovePlayer = player.transform.position + new Vector3(0, offset, 0);

        // Convert that to a position on the canvas.
        Vector2 canvasPos = WorldToCanvasPosition(mainCanvas, mainCamera, worldPositionAbovePlayer);

        // Set the position of the puzzlePanel's RectTransform to this position.
        RectTransform puzzlePanelRectTransform = puzzlePanel.GetComponent<RectTransform>();
        puzzlePanelRectTransform.anchoredPosition = canvasPos;
    }

}
