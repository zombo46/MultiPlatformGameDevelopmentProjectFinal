using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2TreeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject logCamera;

    [SerializeField] private GameObject logCameraUI;
    public Terrain terrain;
    public GameObject treePrefab;
    public int numberOfTrees = 1000;
    public GameObject truePlayer;


    public float spawnHeight = 500f;
    public float minDistanceBetweenTrees = 5f;
    public float minY = 10f;
    public int maxAttempts = 1000;
    public LayerMask obstacleMask;
    public string treeLayerName = "OxygenCollectable";

    private int treesSpawned = 0;
    private int treeLayerIndex = 0;
    private int treeLayerMask = 0;

    void Start()
    {
        if (!terrain || !treePrefab)
        {
            Debug.LogError("OxygenSpawner: Terrain or OxygenPrefab not assigned.");
            return;
        }

        // Resolve layer index and mask, validate existence
        treeLayerIndex = LayerMask.NameToLayer(treeLayerName);
        if (treeLayerIndex < 0)
        {
            Debug.LogError($"OxygenSpawner: Layer '{treeLayerName}' not defined.");
            treeLayerIndex = 0;
        }

        SpawnTree();
    }

    void SpawnTree()
    {
        int attempts = 0;

        while (treesSpawned < numberOfTrees && attempts < maxAttempts * numberOfTrees)
        {
            attempts++;

            float x = Random.Range(0, terrain.terrainData.size.x);
            float z = Random.Range(0, terrain.terrainData.size.z);
            float y = terrain.SampleHeight(new Vector3(x, 0f, z)) + terrain.transform.position.y;


            Vector3 position = new Vector3(x, y, z);

            bool touchesObstacle = Physics.CheckSphere(position, minDistanceBetweenTrees, obstacleMask);
            if (touchesObstacle)
            {
                continue;
            }

            // Check if there are existing oxygen objects nearby using the cached mask
            if (treeLayerMask != 0 && Physics.CheckSphere(position, minY, treeLayerMask))
            {
                continue;
            }

            GameObject treeInstance = Instantiate(treePrefab, position, Quaternion.identity);
            treeInstance.GetComponentInChildren<OxygenTopUp>().Player = truePlayer;
            treeInstance.GetComponentInChildren<Photographable>().logCamera = logCamera;
            treeInstance.GetComponentInChildren<Photographable>().logCameraUI = logCameraUI;
            treeInstance.layer = treeLayerIndex;
            treesSpawned++;
        }
    }
}
