using EditorTool.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    GameObject cubePrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            ObjectRegistry.Instance.SpawnObject(cubePrefab);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
     
        
    }
}
