using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform weaponSpawnPoint;

    void Start()
    {
        // Silah prefab'ini doðru dönüþ açýsýyla instantiate et
        InstantiateWeapon();
    }

    private void InstantiateWeapon()
    {
        // Silah prefab'ini belirli bir dönüþ açýsýyla instantiate et
        Quaternion weaponRotation = Quaternion.Euler(0f, 180f, 0f); // Burada istediðiniz dönüþ açýsýný belirtin
        Instantiate(weaponPrefab, weaponSpawnPoint.position, weaponRotation, weaponSpawnPoint);
    }
}

