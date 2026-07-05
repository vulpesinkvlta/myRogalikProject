using Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GamePlay
{
    public class SinChoiceView : MonoBehaviour, ISinChoiceView
    {
        [SerializeField] private GameObject _root;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _refuseButton;

        private Action _onAccept;
        private Action _onRefuse;

        private ISinChoiceUIService _uiService;

        [Inject]
        public void Construct(ISinChoiceUIService uiService)
        {
            _uiService = uiService;
            _uiService.Register(this);
        }

        private void Awake()
        {
            _acceptButton.onClick.AddListener(Accept);
            _refuseButton.onClick.AddListener(Refuse);

            Hide(); 
        }

        private void OnDestroy()
        {
            if (_uiService != null)
                _uiService.Unregister(this);

            _acceptButton.onClick.RemoveListener(Accept);
            _refuseButton.onClick.RemoveListener(Refuse);
        }
        public void Show(SinsConfig sin, Action onAccept, Action onRefuse)
        {
            _onAccept = onAccept;
            _onRefuse = onRefuse;

           _title.text = sin.Name;  
           _description.text = sin.Description;
    
           _root.SetActive(true);
        }

        private void Accept()
        {
            Hide();
            Action callback = _onAccept;
            ClearCallbacks();
            callback?.Invoke();
        }

        private void Refuse()
        {
            Hide();
            Action callback = _onRefuse;
            ClearCallbacks();
            callback?.Invoke();
        }

        private void ClearCallbacks()
        {
            _onAccept = null;
            _onRefuse = null;
        }

        public void Hide()
        {
            _root.SetActive(false);
        }
    }
}
