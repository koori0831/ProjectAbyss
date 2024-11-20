using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

public class NpcInteract : MonoBehaviour
{

    [SerializeField] private TMP_Text _baseNpcText;
    [SerializeField] private DialogSO _baseNpcDialog;

    private bool _isActive;
    private bool _isChatting;

    public UnityEvent NpcInteracted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isActive && !_isChatting)
        {
            _isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isActive)
        {
            _isActive = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //&& _isActive)
        { 
            if (!ChatManager.Instance.endText)

            ChatManager.Instance.StartChat(_baseNpcText, GetDialog(_baseNpcDialog.JobDialogList), 0.2f);
            NpcInteracted.Invoke();
        }
    }

    public string GetDialog(List<string> dialogs)
    {
        string dialog = dialogs[Random.Range(0, dialogs.Count)];
        return dialog;
    }
}
