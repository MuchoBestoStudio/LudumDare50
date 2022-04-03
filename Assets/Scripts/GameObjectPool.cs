using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    private List<GameObject> pool;
    private GameObject prefab;

    public GameObjectPool(GameObject prefab, int initialSize)
    {
        pool = new List<GameObject>();
        this.prefab = prefab;

        for(int i = 0;i <= initialSize; i++)
        {
            Grow();
        }
    }

    public GameObject Rescue()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        
        return Grow();
    }

    private GameObject Grow()
    {
        GameObject newObj = GameObject.Instantiate(prefab,Vector2.zero,Quaternion.identity,null);
        newObj.SetActive(false);
        pool.Add(newObj);

        return newObj;
    }
}