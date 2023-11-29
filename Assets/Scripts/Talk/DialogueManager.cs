using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;

    public GameObject yesButton;
    public GameObject noButton;

    private Action<bool> callback;

    private void Start()
    {
        HideDialogue();
    }

    public void ShowDialogue(string message, Action<bool> choiceCallback)
    {
        dialogueText.text = message;
        callback = choiceCallback;

        yesButton.SetActive(true);
        noButton.SetActive(true);
        dialogueBox.SetActive(true);
    }

    public void OnYesClicked()
    {
        callback?.Invoke(true);
        HideDialogue();
    }

    public void OnNoClicked()
    {
        callback?.Invoke(false);
        HideDialogue();
    }

    private void HideDialogue()
    {
        dialogueBox.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }
}
