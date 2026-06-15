using Animation;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public bool destroyOnKill = false;
    public float startLife = 10f;
    [SerializeField] public float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }
    
    protected void ResetLife()
    {
        _currentLife = startLife;
    }
    protected virtual void Kill()
    {
        if(destroyOnKill) Destroy(gameObject, 1f);
        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(float f)
    {
        _currentLife -= f;
        if (_currentLife <= 0)
        {
            Kill();
        }
        OnDamage?.Invoke(this);
    }
}

