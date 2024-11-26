using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public GameObject[] playerBodyParts; // Gizlemeniz gereken t�m beden par�alar�
    public PhotonView photonView;

    void Start()
    {
        if (photonView.IsMine)
        {
            // Kendi beden par�alar�n�z� gizleyin
            HideOwnBody();
        }
    }

    void HideOwnBody()
    {
        foreach (GameObject part in playerBodyParts)
        {
            if (part != null)
            {
                part.SetActive(false);
            }
        }
    }
}
