using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class TwoPersonDialogueView : DialogueViewBase
{
    [Header("Objects")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI currentDialogueText;
    [SerializeField] private SpeakerName speakerNameLeft;
    [SerializeField] private SpeakerName speakerNameRight;
    private Character currentCharacter;
    private Character lastCharacter;
    private LocalizedLine currentLine;
    private LocalizedLine lastLine;

    [Header("Characters")]
    [SerializeField] private List<Character> availableCharacters = new List<Character>();

    [Header("Lines")]
    [SerializeField] private bool typeOutLines;
    private float typingDelay;
    [SerializeField] private float normalTypingDelay;
    [SerializeField] private float spedUpTypingDelay;
    [SerializeField] private float waitSecAfterSpeedUp;
    private bool typingSpedUp;
    private bool isTyping;

    [Header("Options")]
    [SerializeField] private OptionView optionViewPrefab;
    [SerializeField] private Transform optionViewParent;
    private List<OptionView> optionViews = new List<OptionView>();
    Action<int> OnOptionSelected;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UserRequestedViewAdvancement();
        }
    }

    [YarnCommand("fade-out-and-in")]
    public void FadeOutAndIn(float time)
    {
        StartCoroutine(FadeToNextNode(time));
    }

    private IEnumerator FadeToNextNode(float time)
    {
        yield return StartCoroutine(Effects.FadeAlpha(canvasGroup, 1, 0, time));

        currentDialogueText.text = "";
        speakerNameLeft.RemoveName();
        speakerNameRight.RemoveName();
        currentCharacter = null;
        lastCharacter = null;
        currentLine = null;
        lastLine = null;

        yield return StartCoroutine(Effects.FadeAlpha(canvasGroup, 0, 1, 0.5f));
    }

    public override void DialogueStarted()
    {
        currentDialogueText.text = "";
        speakerNameLeft.RemoveName();
        speakerNameRight.RemoveName();
        SetOpacityOverTime(0, 1, 0.5f);
    }

    public override void DialogueComplete()
    {
        currentCharacter = null;
        lastCharacter = null;
        currentLine = null;
        lastLine = null;
        speakerNameLeft.StopSpeaking();
        speakerNameRight.StopSpeaking();
        SetOpacityOverTime(1, 0, 2);
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        //dismiss if dialogueview not active
        if (gameObject.activeInHierarchy == false)
        {
            onDialogueLineFinished();
            return;
        }

        SetCurrentAndLast(dialogueLine);
        SetSpeakerName();

        if (typeOutLines)
        {
            typingSpedUp = false;
            typingDelay = normalTypingDelay;
            StartCoroutine(TypeOutString(currentDialogueText, currentLine.TextWithoutCharacterName.Text));
        } else
        {
            currentDialogueText.text = currentLine.TextWithoutCharacterName.Text;
        }
    }

    private void SetCurrentAndLast(LocalizedLine newLine)
    {
        lastLine = currentLine;
        currentLine = newLine;

        lastCharacter = currentCharacter;
        currentCharacter = null;
        foreach (Character character in availableCharacters)
        {
            if (character.characterName == currentLine.CharacterName)
            {
                currentCharacter = character;
                break;
            }
        }
    }

    private void SetSpeakerName()
    {
        if (currentCharacter == null)
        {
            currentDialogueText.color = Color.white;
            speakerNameLeft.StopSpeaking();
            speakerNameRight.StopSpeaking();
            return;
        }

        if (speakerNameLeft.character == null)
        {
            speakerNameLeft.SetName(currentCharacter);
        }
        else if (speakerNameRight.character == null && speakerNameLeft.character != currentCharacter)
        {
            speakerNameRight.SetName(currentCharacter);
        }

        if (speakerNameLeft.character == currentCharacter)
        {
            currentDialogueText.color = currentCharacter.textColor;
            speakerNameLeft.StartSpeaking();
            speakerNameRight.StopSpeaking();
            return;
        }

        if (speakerNameRight.character == currentCharacter)
        {
            currentDialogueText.color = currentCharacter.textColor;
            speakerNameRight.StartSpeaking();
            speakerNameLeft.StopSpeaking();
            return;
        }
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (isTyping && !typingSpedUp)
        {
            SpeedUpTyping();
        }
        else if(!isTyping)
        {
            onDialogueLineFinished?.Invoke();
        }
    }


    public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
    {
        speakerNameLeft.StopSpeaking();
        speakerNameRight.StopSpeaking();
        currentDialogueText.text = "";

        // If we don't already have enough option views, create more
        while (dialogueOptions.Length > optionViews.Count)
        {
            var optionView = CreateNewOptionView();
            optionView.gameObject.SetActive(false);
        }

        // Set up all of the option views
        int optionViewsCreated = 0;

        for (int i = 0; i < dialogueOptions.Length; i++)
        {
            var optionView = optionViews[i];
            var option = dialogueOptions[i];

            if (option.IsAvailable == false)
            {
                // Don't show this option.
                continue;
            }

            optionView.gameObject.SetActive(true);

            optionView.Option = option;

            // The first available option is selected by default
            if (optionViewsCreated == 0)
            {
                optionView.Select();
            }

            optionViewsCreated += 1;
        }

        // Note the delegate to call when an option is selected
        OnOptionSelected = onOptionSelected;

        OptionView CreateNewOptionView()
        {
            var optionView = Instantiate(optionViewPrefab);
            optionView.transform.SetParent(optionViewParent, false);
            optionView.transform.SetAsLastSibling();

            optionView.OnOptionSelected = OptionViewWasSelected;
            optionViews.Add(optionView);

            return optionView;
        }

        void OptionViewWasSelected(DialogueOption option)
        {
            StartCoroutine(OptionViewWasSelectedInternal(option));

            IEnumerator OptionViewWasSelectedInternal(DialogueOption selectedOption)
            {
                DisableOptionView();
                OnOptionSelected(selectedOption.DialogueOptionID);
                yield return null;
            }
        }
    }

    private void DisableOptionView()
    {
        // Hide all existing option views
        foreach (var optionView in optionViews)
        {
            optionView.gameObject.SetActive(false);
        }
    }

    public override void UserRequestedViewAdvancement()
    {
        requestInterrupt();
    }

    private IEnumerator TypeOutString(TextMeshProUGUI text, string type)
    {
        isTyping = true;
        for (int i = 0; i < type.Length; i++)
        {
            text.text = type.Substring(0, i);
            yield return new WaitForSeconds(typingDelay);
        }

        text.text = type;

        if (typingSpedUp)
        {
            yield return new WaitForSeconds(waitSecAfterSpeedUp);
        }
        isTyping = false;
    }

    private void SpeedUpTyping()
    {
        if (!typingSpedUp && isTyping)
        {
            typingSpedUp = true;
            typingDelay = spedUpTypingDelay;
        }
    }

    private void SetOpacityOverTime(float from, float to, float time)
    {
        StartCoroutine(Effects.FadeAlpha(canvasGroup, from, to, time));
    }
}
