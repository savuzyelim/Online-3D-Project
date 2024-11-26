using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public GameObject[] playerBodyParts; // Gizlemeniz gereken tüm beden parçalarý
    public PhotonView photonView;

    void Start()
    {
        if (photonView.IsMine)
        {
            // Kendi beden parçalarýnýzý gizleyin
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
