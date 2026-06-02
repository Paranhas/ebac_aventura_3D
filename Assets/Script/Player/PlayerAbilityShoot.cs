using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [Header("Prefabs das Armas")]
    public List<GunBase> gunPrefabs;

    [Header("Posição da Arma")]
    public Transform gunPosition;

    private List<GunBase> _guns = new();
    private GunBase _currentGun;
    private int _currentGunIndex;

    protected override void Init()
    {
        base.Init();

        CreateGuns();

        inputs.GamePlay.Shoot.performed += ctx => StartShoot();
        inputs.GamePlay.Shoot.canceled += ctx => CancelShoot();
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            ChangeWeapon(0);

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            ChangeWeapon(1);

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            ChangeWeapon(2);

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
            ChangeWeapon(3);
    }

    private void CreateGuns()
    {
        foreach (var gunPrefab in gunPrefabs)
        {
            GunBase gun = Instantiate(gunPrefab, gunPosition);

            gun.transform.localPosition = Vector3.zero;
            gun.transform.localRotation = Quaternion.identity;

            gun.gameObject.SetActive(false);

            _guns.Add(gun);
        }

        if (_guns.Count > 0)
        {
            ChangeWeapon(0);
        }
    }

    private void ChangeWeapon(int index)
    {
        if (index < 0 || index >= _guns.Count)
            return;

        if (_currentGun != null)
        {
            _currentGun.StopShoot();
            _currentGun.gameObject.SetActive(false);
        }

        _currentGunIndex = index;
        _currentGun = _guns[index];

        _currentGun.gameObject.SetActive(true);

        Debug.Log($"Arma equipada: {_currentGun.name}");
    }

    private void StartShoot()
    {
        _currentGun?.StartShoot();
    }

    private void CancelShoot()
    {
        _currentGun?.StopShoot();
    }
}