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

    [SerializeField] public int[] snowballValues;
    
    private Client[] clients;
    private List<Client> activeClients = new List<Client>();
    
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
            nextPos.y -= clientHalfSize.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxClients; i++)
        {
            if(clients[i].IsWaiting)
                continue;
            
            SpawnClient(i);
        }
    }

    void SpawnClient(int index)
    {
        // Actually spawn the client
        clients[index].WaitingFor = Random.Range(clientMinWaitingTime, clientMaxWaitingTime);
        clients[index].StartWaiting();
        
        Transform clientTransform = clients[index].GetComponent<Transform>();
        clientTransform.localPosition = new Vector3(clientEnableX, clientMaxY - (activeClients.Count * clientHalfSize.y));
        
        activeClients.Add(clients[index]);
        clientTotalCount++;
    }

    public void DespawnClient(int index, bool satisifed)
    {
        activeClients.Remove(clients[index]);

        for (int i = 0; i < activeClients.Count; i++)
        {
            activeClients[i].transform.localPosition = new Vector3(clientEnableX, clientMaxY - (i * clientHalfSize.y));
        }
        
        Transform clientTransform = clients[index].GetComponent<Transform>();
        clientTransform.localPosition = new Vector3(clientDisableX, clientMaxY);
        
        if (satisifed)
            clientSatisfiedCount++;
    }
}
