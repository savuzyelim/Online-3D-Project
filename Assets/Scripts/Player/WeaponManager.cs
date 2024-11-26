using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform weaponSpawnPoint;

    void Start()
    {
        // Silah prefab'ini do�ru d�n�� a��s�yla instantiate et
        InstantiateWeapon();
    }

    private void InstantiateWeapon()
    {
        // Silah prefab'ini belirli bir d�n�� a��s�yla instantiate et
        Quaternion weaponRotation = Quaternion.Euler(0f, 180f, 0f); // Burada istedi�iniz d�n�� a��s�n� belirtin
        Instantiate(weaponPrefab, weaponSpawnPoint.position, weaponRotation, weaponSpawnPoint);
    }
}

