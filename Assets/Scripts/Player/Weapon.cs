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
        // Sadece yerel oyuncunun silah� aktif olsun
        if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
            return;
        }

        // Kameray� bu objenin parent'�nda bul
        cam = GetComponentInParent<Camera>();

        // E�er parent'�nda kamera bulunamazsa, sahnedeki ana kameray� bul
        if (cam == null)
        {
            cam = Camera.main;
        }

        // E�er hala kamera bulunamazsa, hata mesaj� g�ster
        if (cam == null)
        {
            Debug.LogError("Kamera bulunamad�! L�tfen sahnede bir ana kamera oldu�undan emin olun veya kameray� manuel olarak atay�n.");
        }
    }

    void Update()
    {
        // E�er silah devre d��� b�rak�ld�ysa, hi�bir i�lem yapma
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
            Debug.LogError("Kamera atanmam��!");
            return;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            Debug.Log("Hit point: " + hit.point);

            // VFX'i do�ru pozisyonda instantiate et
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
