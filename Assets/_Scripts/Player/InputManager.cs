using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    [Serializable]
    public struct QuestionData
    {
        public string question;
        public List<string> answers;
        public int correctAnswerIndex;
    }

    public TMP_Text questionText; // For the main question.
    public List<Toggle> answerToggles; // List of Toggles for the answers.
    public TMP_Text feedbackText;
    public Button submitButton;
    public GameObject puzzlePanel;
    private int currentQuestionIndex = 0;


    public List<QuestionData> questionDataList;

    [SerializeField] Canvas mainCanvas;
    [SerializeField] Camera mainCamera;
    [SerializeField] float offset = 2.0f;

    public AudioClip puzzleUnsolvedSound; // This is the PuzzleUnsolved sound effect
    private AudioSource audioSource; // AudioSource to play the sound effect

    private void Start()
    {
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Puzzle Panel not assigned. Please assign it in the inspector.");
        }

        foreach (var toggle in answerToggles)
        {
            toggle.onValueChanged.AddListener(delegate { EnsureSingleToggleCheck(toggle); });
        }

        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void EnsureSingleToggleCheck(Toggle changedToggle)
    {
        if (changedToggle.isOn)
        {
            foreach (var toggle in answerToggles)
            {
                if (toggle != changedToggle) toggle.isOn = false;
            }
        }
    }

    public void PopulateWithRandomQuestions(int numberOfQuestions)
    {
        questionDataList = new List<QuestionData>
        {
            new QuestionData
            {
                question = "Which of these is not a valid Python data type?",
                answers = new List<string>{ "dict", "array", "list", "tuple" },
                correctAnswerIndex = 1
            },
            new QuestionData
            {
                question = "How do you start a comment in Python?",
                answers = new List<string>{ "#", "//", "/*", "--" },
                correctAnswerIndex = 0
            },
            new QuestionData
            {
                question = "Which of the following is not a Python keyword?",
                answers = new List<string>{ "if", "for", "then", "else" },
                correctAnswerIndex = 2
            },
            new QuestionData
            {
                question = "What will be the output of print(bool(0))?",
                answers = new List<string>{ "0", "False", "True", "None" },
                correctAnswerIndex = 1
            },
            new QuestionData
            {
                question = "Which function is used to read user input in Python 3?",
                answers = new List<string>{ "raw_input()", "input()", "get_input()", "read_input()" },
                correctAnswerIndex = 1
            },
            new QuestionData
            {
                question = "What will 'hello'.isalpha() return?",
                answers = new List<string>{ "True", "False", "None", "Error" },
                correctAnswerIndex = 0
            },
            new QuestionData
            {
                question = "Which collection is ordered and mutable in Python?",
                answers = new List<string>{ "List", "Tuple", "Set", "Dictionary" },
                correctAnswerIndex = 0
            },
            new QuestionData
            {
                question = "What will be the type of x after x = 3 + 4j?",
                answers = new List<string>{ "float", "int", "complex", "str" },
                correctAnswerIndex = 2
            },
            new QuestionData
            {
                question = "Which of these is used to define a function in Python?",
                answers = new List<string>{ "func", "start", "def", "function" },
                correctAnswerIndex = 2
            },
            new QuestionData
            {
                question = "Which method is used to get the number of items in a list?",
                answers = new List<string>{ "length()", "len()", "count()", "size()" },
                correctAnswerIndex = 1
            },
            new QuestionData
            {
                question = "What is the result of 'Python'[1]?",
                answers = new List<string>{ "P", "y", "t", "h" },
                correctAnswerIndex = 1
            },
            new QuestionData
            {
                question = "Which operator is used for exponentiation in Python?",
                answers = new List<string>{ "*", "+", "^", "**" },
                correctAnswerIndex = 3
            },
            new QuestionData
            {
                question = "Which of the following is correct about Python?",
                answers = new List<string>{ "It supports functional and structured programming.", "It can run on any platform.", "Both of the above.", "None of the above." },
                correctAnswerIndex = 2
            },
            new QuestionData
            {
                question = "Which function returns the largest item from a list?",
                answers = new List<string>{ "largest()", "bigger()", "max()", "large()" },
                correctAnswerIndex = 2
            },
            new QuestionData
            {
                question = "Which module in Python supports regular expressions?",
                answers = new List<string>{ "regex", "re", "regexpressions", "regular" },
                correctAnswerIndex = 1
            },
            new QuestionData
            {
                question = "Which of these methods will remove the last item from a list?",
                answers = new List<string>{ "remove()", "delete()", "discard()", "pop()" },
                correctAnswerIndex = 3
            },
            new QuestionData
            {
                question = "What is the correct way to create a set in Python?",
                answers = new List<string>{ "set[]", "[]", "{}", "set{}" },
                correctAnswerIndex = 2
            },
            new QuestionData
            {
                question = "Which of the following will give a syntax error?",
                answers = new List<string>{ "print('hello')", "print(hello)", "print(\"hello\")", "print(hello)" },
                correctAnswerIndex = 1
            },
            new QuestionData
            {
                question = "What is the correct file extension for Python files?",
                answers = new List<string>{ ".py", ".python", ".pt", ".pn" },
                correctAnswerIndex = 0
            },
            new QuestionData
            {
                question = "Which method is used to convert a list into a string?",
                answers = new List<string>{ ".to_str()", ".join()", ".convert()", ".stringify()" },
                correctAnswerIndex = 1
            }
        };


    }

    public void ShowNextQuestion()
    {
        if (currentQuestionIndex < questionDataList.Count)
        {
            questionText.text = questionDataList[currentQuestionIndex].question;

            for (int i = 0; i < questionDataList[currentQuestionIndex].answers.Count; i++)
            {
                TMP_Text toggleLabel = answerToggles[i].transform.Find("Label").GetComponent<TMP_Text>();
                if (toggleLabel != null)
                {
                    toggleLabel.text = questionDataList[currentQuestionIndex].answers[i];
                }
            }
            feedbackText.text = ""; // Clear feedback text


            foreach (Toggle toggle in answerToggles)
            {
                toggle.isOn = false;
            }
        }
        else
        {
            Debug.Log("All questions answered!");
            puzzlePanel.SetActive(false); // Hide the puzzle UI when all questions are done
        }
    }


    public void OpenQuestionPanel(Action onSuccess)
    {
        GetComponent<PlayerController>().isSolvingPuzzle = true;
        PopulateWithRandomQuestions(5);
        puzzlePanel.SetActive(true);
        ShowNextQuestion();
        PositionPuzzlePanelAbovePlayer();
        submitButton.onClick.RemoveAllListeners();

        submitButton.onClick.AddListener(() => {
            int selectedAnswerIndex = answerToggles.FindIndex(toggle => toggle.isOn);

            if (selectedAnswerIndex != -1)
            {
                if (selectedAnswerIndex == questionDataList[currentQuestionIndex].correctAnswerIndex)
                {
                    GetComponent<PlayerController>().isSolvingPuzzle = false;
                    feedbackText.text = "Correct!";
                    puzzlePanel.SetActive(false);
                    currentQuestionIndex++;
                    onSuccess?.Invoke();
                }
                else
                {
                    feedbackText.text = "Incorrect, try again.";

                    // Play the PuzzleUnsolved sound effect
                    if (puzzleUnsolvedSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(puzzleUnsolvedSound);
                    }
                }
            }
            else
            {
                feedbackText.text = "Please select an answer.";
            }
        });
    }

    public void PositionPuzzlePanelAbovePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found.");
            return;
        }

        Vector3 worldPositionInFrontOfPlayer = player.transform.position + new Vector3(offset, 0, 0);

        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPositionInFrontOfPlayer);

        puzzlePanel.transform.position = screenPosition;

    }
}
