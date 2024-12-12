using UnityEngine;
using UnityEngine.Events;

public class NpcInteract : MonoBehaviour
{
    private bool _isActive;
    private bool _isChatting;
    private float _cooldownTimer;
    private const float Cooldown = 1.5f;

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
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E) && _isActive && _cooldownTimer <= 0)
        {
            _cooldownTimer = Cooldown;
            NpcInteracted?.Invoke();
        }
    }
}
