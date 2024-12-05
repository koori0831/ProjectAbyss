using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;


public class NpcInteract : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.E) && _isActive)
        { 
            NpcInteracted?.Invoke();
        }
    }

    
}
