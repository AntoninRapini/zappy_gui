using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : InventoryItem
{
    void Start ()
    {
        id = 0;
        int rand = Random.Range(1, 4);
        GameObject child = GameObject.Instantiate(Resources.Load("GameObjects/Food/Prefab/Food" + rand) as GameObject);
        if (child != null)
        {
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            child.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
