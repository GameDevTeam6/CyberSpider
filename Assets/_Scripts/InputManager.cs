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
    public bool isInteractingWithInputField = false;

    private string[] puzzles = {
        "def add(a, b):\n    return ________",
        "def reverse_string(s):\n    return ________",
        "def get_middle_char(s):\n    return s[______]",
        "def is_even(num):\n    return ________",
        "def list_sum(lst):\n    return ________",
        "def multiply(a, b):\n    return ________",
        "def greet(name):\n    return 'Hello, ' + ________",
        "def find_max(lst):\n    return ________",
        "def list_length(lst):\n    return ________",
        "def divide(a, b):\n    if b != 0:\n        return ________\n    else:\n        return 'Undefined'"
    };

    private string[] answers = {
        "a + b",
        "s[::-1]",
        "len(s) // 2",
        "num % 2 == 0",
        "sum(lst)",
        "a * b",
        "name + '!'",
        "max(lst)",
        "len(lst)",
        "a / b"
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
                return; // Exit out of the Start method if the main camera isn't found.
            }
        }

        // Ensure mainCanvas is assigned
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
            if (mainCanvas == null)
            {
                Debug.LogError("Main canvas not found. Please assign it manually in the inspector.");
                return; // Exit out of the Start method if the main canvas isn't found.
            }
        }

        // Make sure the puzzle UI is hidden on start
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
            isInteractingWithInputField = false;
        }
        else
        {
            Debug.LogError("Puzzle Panel not assigned. Please assign it in the inspector.");
        }
        TextMeshProUGUI buttonText = submitButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Submit";

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
            isInteractingWithInputField = false;
        }
    }

    public void OpenInputField(Action onSuccess)
    {
        if (currentPuzzleIndex < puzzles.Length) // Check if there's another puzzle
        {
            Debug.Log("Showing puzzle panel");
            PositionPuzzlePanelAbovePlayer();
            puzzlePanel.SetActive(true); // Display the puzzle UI
            ShowNextPuzzle(); // Show the next puzzle
            isInteractingWithInputField = true;
        }
        else
        {
            puzzlePanel.SetActive(false); // Hide the puzzle UI if no more puzzles
            isInteractingWithInputField = false;
        }

        submitButton.onClick.AddListener(() => {
            if (editor.text == answers[currentPuzzleIndex])
            {
                Debug.Log("Correct!");
                puzzlePanel.SetActive(false);
                isInteractingWithInputField = false;
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }

        // Get the world position right above the player.
        Vector3 worldPositionAbovePlayer = player.transform.position + new Vector3(0, offset, 0);

        // Get the screen position right above the player.
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPositionAbovePlayer);

        // Convert screen position to a position usable for RectTransform anchoredPosition.
        Vector2 anchoredPosition = new Vector2(screenPosition.x / mainCanvas.scaleFactor - (mainCanvas.pixelRect.width / 2),
                                               screenPosition.y / mainCanvas.scaleFactor - (mainCanvas.pixelRect.height / 2));

        // Get the RectTransform of the puzzlePanel and adjust its properties.
        RectTransform puzzlePanelRectTransform = puzzlePanel.GetComponent<RectTransform>();
        if (puzzlePanelRectTransform == null)
        {
            Debug.LogError("RectTransform not found on puzzlePanel.");
            return;
        }

        // Adjust the pivot point if necessary (e.g., to position the bottom of the panel at the calculated position).
        puzzlePanelRectTransform.pivot = new Vector2(0.5f, 0);

        // Set the position of the puzzlePanel's RectTransform to this position.
        puzzlePanelRectTransform.anchoredPosition = anchoredPosition;
    }


}
