using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private float _magneticPower = 1f, _radius = 1.5f;
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private int _maxDetectCount = 10;

    private Collider2D[] _collectArray;

    private void Awake()
    {
        _collectArray = new Collider2D[_maxDetectCount];
    }

    private void FixedUpdate()
    {
        int count = Physics2D.OverlapCircle(transform.position, _radius, _contactFilter, _collectArray);
        for(int i=0; i<count;i++)
        {
            if (_collectArray[i].TryGetComponent(out Collectable collectable))
            {
                collectable.Collect(transform, _magneticPower);
            }
        }
    }
}
