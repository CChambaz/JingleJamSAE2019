using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private int maxClients;
    [SerializeField] private float clientSpawnChance;
    [SerializeField] private float clientMinWaitingTime;
    [SerializeField] private float clientMaxWaitingTime;

    [SerializeField] private Vector2 clientHalfSize;
    [SerializeField] private float clientMaxY;
    [SerializeField] private float clientDisableX;
    [SerializeField] private float clientEnableX;
    
    private Client[] clients;
    private int activeClients;
    private int clientTotalCount;
    private int clientSatisfiedCount;

    // Start is called before the first frame update
    void Start()
    {
        clients = new Client[maxClients];

        Vector3 nextPos = new Vector3(transform.position.x + clientDisableX, clientMaxY);
        
        for (int i = 0; i < maxClients; i++)
        {
            clients[i] = Instantiate(clientPrefab, nextPos, Quaternion.identity, transform).GetComponent<Client>();
            clients[i].Index = i;
            nextPos.y -= i * clientHalfSize.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i < maxClients; i++)
        {
            if(clients[i].IsWaiting)
                continue;
            
            SpawnClient(i);
        }*/
    }

    void SpawnClient(int index)
    {
        activeClients++;

        // Actually spawn the client
        clients[index].WaitingFor = Random.Range(clientMinWaitingTime, clientMaxWaitingTime);
        clients[index].StartWaiting();
        
        Transform clientTransform = clients[index].GetComponent<Transform>();
        clientTransform.position = new Vector3(clientEnableX, clientMaxY - (activeClients * clientHalfSize.y));
        
        clientTotalCount++;
    }

    public void DespawnClient(int index, bool satisifed)
    {
        Transform clientTransform = clients[index].GetComponent<Transform>();
        clientTransform.position = new Vector3(clientDisableX, clientMaxY - (activeClients * clientHalfSize.y));

        if (satisifed)
            clientSatisfiedCount++;

        activeClients--;
    }
}
