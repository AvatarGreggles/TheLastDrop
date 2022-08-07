using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    [SerializeField] Text dialogText;
    public int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public static DialogManager Instance { get; private set; }

    bool shouldGoToNextLine = false;

    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    Action onDialogFinished;

    int currentLine = 0;

    public bool IsShowing { get; private set; }
    public bool IsTyping { get; private set; }


    public IEnumerator ShowDialog(Dialog dialog, Action onFinished = null)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        IsShowing = true;
        this.dialog = dialog;
        onDialogFinished = onFinished;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleNextLine()
    {
        shouldGoToNextLine = true;
    }


    public void HandleUpdate()
    {
        if (shouldGoToNextLine && !IsTyping)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                IsShowing = false;
                dialogBox.SetActive(false);
                onDialogFinished?.Invoke();
                OnCloseDialog?.Invoke();
            }
        }

    }

    public IEnumerator TypeDialog(string line)
    {
        IsTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        IsTyping = false;
        shouldGoToNextLine = false;
    }

    public void CloseDialog()
    {
        IsShowing = false;
        dialogBox.SetActive(false);
        onDialogFinished?.Invoke();
        OnCloseDialog?.Invoke();
    }

}
