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
    public int ClientTotalCount
    {
        get => clientTotalCount;
        set => clientTotalCount = value;
    }

    private int clientSatisfiedCount;
    public int ClientSatisfiedCount
    {
        get => clientSatisfiedCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ClientManager = this;
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
        if (GameManager.Instance.InPause)
            return;
        
        for (int i = 0; i < maxClients; i++)
        {
            if(clients[i].IsWaiting)
                continue;
            
            if(Random.Range(0.0f, 100.0f) <= clientSpawnChance)
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
        CheckStorage();
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

        CheckStorage();

        if (satisifed)
        {
            clientSatisfiedCount++;
            
            for(int i = 0; i < clients.Length; i++)
                clients[i].UpdateMaxItemIndex();
        }
    }
    
    public void CheckStorage()
    {
        bool hasEnoughSnowball = true;
        
        for (int i = 0; i < activeClients.Count; i++)
        {
            for (int j = 0; j < activeClients[i].Order.Length; j++)
            {
                if (activeClients[i].Order[j] > GameManager.Instance.SnowballAmount[j])
                {
                    hasEnoughSnowball = false;
                    activeClients[i].UpdateItemImage(j, false);
                }
                else
                    activeClients[i].UpdateItemImage(j);
            }

            if (hasEnoughSnowball)
                activeClients[i].OrderCanBeAchieved = true;
            else
                activeClients[i].OrderCanBeAchieved = false;
            
            hasEnoughSnowball = true;
        }
    }
}
