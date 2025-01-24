using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Gases : MonoBehaviour
{
    [SerializeField] private Transform poolContainer;
    [SerializeField] private List<GameObjectPool> objectPools;

    private void Start()
    {
        objectPools = new List<GameObjectPool>();
    }

    private void OnEnable()
    {
        GameEvents.SpawnGasPool.AddListener(Listener_SpawnPool);
    }

    private void OnDisable()
    {
        GameEvents.SpawnGasPool.RemoveListener(Listener_SpawnPool);
    }

    private void Listener_SpawnPool(PoolAbleObject prefab,  SheepController origin)
    {
        if (objectPools == null) objectPools = new List<GameObjectPool>();
        var targetPool = objectPools.Find(pool => pool.objectPrefab == prefab);

        // Debug.Log($"{name} Spawn Effects  [{prefab.name}]  ");
        if (targetPool == null)
        {
            var poolParent = new GameObject();
            poolParent.name = "Pool -" + prefab.name;
            SetParentOrigin(poolParent.transform, poolContainer);
            var newObjectPool = new GameObjectPool();
            newObjectPool.Initialize(poolParent.transform, prefab);
            objectPools.Add(newObjectPool);
            targetPool = newObjectPool;
        }

        var newSpawn = targetPool.pool.Get();
        newSpawn.transform.position = origin.GetGasSpawnPos();

        var gasController = newSpawn.GetComponent<GasController>();
        if (gasController != null)
        {
            gasController.SetupGasFromPlayer(origin); 
        }
        else
        {
            Debug.LogWarning($"Gas Controller not found in {newSpawn}" , newSpawn.gameObject);
        }
        
        newSpawn.Activate(targetPool.poolParent);
        
        gasController.LaunchGas();
    }

    public void SetParentOrigin(Transform self, Transform parent)
    {
        self.SetParent(parent);
        self.localPosition = Vector3.zero;
        self.localRotation = Quaternion.identity;
    }
}