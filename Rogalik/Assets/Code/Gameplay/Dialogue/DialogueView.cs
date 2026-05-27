using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class DialogueView : MonoBehaviour, IDialogueView
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _speakerText;
        [SerializeField] private TMP_Text _bodyText;
        [SerializeField] private Button _nextButton;

        private DialogueConfig _currentDialogue;
        private Action _onCompleted;
        private int _currentLineIndex;

        private void Awake()
        {
            _nextButton.onClick.AddListener(NextLine);
            Hide();
        }

        private void OnDestroy()
        {
            _nextButton.onClick.RemoveListener(NextLine);
        }

        public void Show(DialogueConfig dialogue, Action onCompleted)
        {
            _currentDialogue = dialogue;
            _onCompleted = onCompleted;
            _currentLineIndex = 0;

            _root.SetActive(true);
            
            ShowCurrentLine();
        }

        public void Hide()
        {
            _root.SetActive(false);
        }
        private void NextLine()
        {
            _currentLineIndex++;

            if (_currentDialogue == null || _currentLineIndex >= _currentDialogue.Lines.Length)
            {
                CompleteDialogue();
                return;
            }

            ShowCurrentLine();
        }

        private void ShowCurrentLine()
        {
            if(_currentDialogue == null || _currentDialogue.Lines.Length == 0)
            {
                CompleteDialogue();
                return;
            }

            DialogueLine line = _currentDialogue.Lines[_currentLineIndex];
            _bodyText.text = line.Text;
        }

        private void CompleteDialogue()
        {
            Hide();
            Action completed = _onCompleted;
            _onCompleted = null;
            _currentDialogue = null;
            completed?.Invoke();
        }
    }
}
