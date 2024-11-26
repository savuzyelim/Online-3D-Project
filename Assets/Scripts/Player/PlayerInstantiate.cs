using UnityEngine;
using Photon.Pun;

public class PlayerInstantiate : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject denemePrefab;
    private Transform spawnPoint;

    private void Awake()
    {
        PlayerInstantiating();
    }

    private void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            Vector3 spawnPosition = spawnPoint.position;
            Quaternion spawnRotation = spawnPoint.rotation;

            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, spawnRotation);
            Instantiate(denemePrefab, spawnPosition, spawnRotation);
        }
        else
        {
            Debug.LogError("PlayerPrefab or PlayerSpawn is not set in PlayerInstantiate.");
        }
    }

    private void FindSpawnPoint()
    {
        GameObject spawnPointObject = GameObject.FindWithTag("spawnpoint");
        if (spawnPointObject != null)
        {
            spawnPoint = spawnPointObject.transform;
        }
        else
        {
            Debug.LogError("No spawn point found with tag 'SpawnPoint'.");
        }
    }

    private void PlayerInstantiating()
    {
        FindSpawnPoint();
        if (spawnPoint != null)
        {
            SpawnPlayer();
        }
        else
        {
            Debug.LogError("Player spawn point not set. Player will not be spawned.");
        }
    }
}
