using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviourPunCallbacks
{
    public int damage;
    public float fireRate;
    private float nextFire;

    [Header("vfx")]
    public GameObject _hitVFX;

    private Camera cam;

    void Start()
    {
        // Sadece yerel oyuncunun silahý aktif olsun
        if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
            return;
        }

        // Kamerayý bu objenin parent'ýnda bul
        cam = GetComponentInParent<Camera>();

        // Eðer parent'ýnda kamera bulunamazsa, sahnedeki ana kamerayý bul
        if (cam == null)
        {
            cam = Camera.main;
        }

        // Eðer hala kamera bulunamazsa, hata mesajý göster
        if (cam == null)
        {
            Debug.LogError("Kamera bulunamadý! Lütfen sahnede bir ana kamera olduðundan emin olun veya kamerayý manuel olarak atayýn.");
        }
    }

    void Update()
    {
        // Eðer silah devre dýþý býrakýldýysa, hiçbir iþlem yapma
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
        if (Input.GetButton("Fire1") && nextFire <= 0)
        {
            nextFire = 1 / fireRate;
            Fire();
        }
    }

    void Fire()
    {
        if (cam == null)
        {
            Debug.LogError("Kamera atanmamýþ!");
            return;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            Debug.Log("Hit point: " + hit.point);

            // VFX'i doðru pozisyonda instantiate et
            GameObject hitVFXInstance = PhotonNetwork.Instantiate("hitVFX", hit.point, Quaternion.identity);
            if (hitVFXInstance != null)
            {
                Debug.Log("VFX instantiated at " + hit.point);
            }
            else
            {
                Debug.LogError("VFX instantiation failed!");
            }

            PlayerHealth playerHealth = hit.transform.gameObject.GetComponent<PlayerHealth>();
            PhotonView photonView = hit.transform.gameObject.GetComponent<PhotonView>();

            if (playerHealth != null && photonView != null)
            {
                photonView.RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing.");
        }
    }
}
