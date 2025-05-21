using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public TMP_InputField inputField;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive, awaitingInput;

    private Movement playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<Movement>();
        inputField.gameObject.SetActive(false);
    }

    public bool canInteract()
    {
        return !isDialogueActive;
    }

    public void interact()
    {
        if (dialogueData == null || awaitingInput)
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;

        foreach (var alt in dialogueData.alternateDialogues)
        {
            if (StoryState.Instance.HasFlag(alt.requiredFlag))
            {
                dialogueData.dialogueLines = alt.lines;
                dialogueData.requiresInput = alt.requiresInput;
                dialogueData.expectedAnswers = alt.expectedAnswers;
                break;
            }
        }

        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.requiresInput.Length > dialogueIndex && dialogueData.requiresInput[dialogueIndex])
        {
            awaitingInput = true;
            inputField.gameObject.SetActive(true);
            inputField.text = "";
            inputField.ActivateInputField();

            if (playerController != null)
                playerController.enabled = false;

            yield break;
        }

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    void Update()
    {
        if (awaitingInput && Input.GetKeyDown(KeyCode.Return))
        {
            string userAnswer = inputField.text.Trim().ToUpperInvariant();
            string expected = dialogueData.expectedAnswers[dialogueIndex].Trim().ToUpperInvariant();

            if (userAnswer == expected)
            {
                inputField.gameObject.SetActive(false);
                if (playerController != null)
                    playerController.enabled = true;

                dialogueText.text = "Raspuns corect. Felicitari.";
                awaitingInput = false;
                StartCoroutine(WaitThenContinue());
            }
            else
            {
                dialogueText.text = "Raspuns gresit. Incearca din nou.";
                inputField.text = "";
                inputField.ActivateInputField();
            }
        }
    }

    IEnumerator WaitThenContinue()
    {
        yield return new WaitForSeconds(dialogueData.autoProgressDelay);
        NextLine();
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        awaitingInput = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        inputField.gameObject.SetActive(false);

        if (playerController != null)
            playerController.enabled = true;

        if (!string.IsNullOrEmpty(dialogueData.onDialogueCompleteFlag))
            StoryState.Instance.SetFlag(dialogueData.onDialogueCompleteFlag);
    }
}
