using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Collectable : MonoBehaviour
{
    CollectableManager collectableManager;
    bool isCollected = false;
    bool isReturned = false;

    // Start is called before the first frame update
    void Start()
    {
        collectableManager = FindObjectOfType<CollectableManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
       
        if(collectableManager.isLevel && other.tag == "Player")
        {
            isCollected = true;
            gameObject.GetComponent<Renderer>().enabled = false; //hides mesh for now since we cannot destroy it
            // Destroy(gameObject);
        }
    }

    public void LoadSaveData(SaveCollectableData data)
    {
        gameObject.name = data.name;
        GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>(data.meshName);
        GetComponent<MeshRenderer>().material = Resources.Load<Material>(data.materialName);
        float[] pos = data.position;
        gameObject.transform.position = new Vector3(pos[0], pos[1], pos[2]);
        float[] rot = data.rotation;
        gameObject.transform.rotation = new Quaternion(rot[0], rot[1], rot[2], rot[3]);
        float[] scale = data.scale;
        gameObject.transform.localScale = new Vector3(scale[0], scale[1], scale[2]);
        isCollected = data.isCollected;
        isReturned = data.isReturned;
        if(isCollected)
        {
            gameObject.GetComponent<Renderer>().enabled = false; //hides mesh for now since we cannot destroy it
        }
    }

    public SaveCollectableData GetSaveData()
    {
        SaveCollectableData data = new SaveCollectableData();
        data.name = gameObject.name;
        data.meshName = GetComponent<MeshFilter>().sharedMesh.name;
        data.materialName = GetComponent<MeshRenderer>().material.name.Replace("(Instance)", "").Trim();
        Vector3 pos = gameObject.transform.position;
        data.position = new float[] {pos.x, pos.y, pos.z};
        Quaternion rot = gameObject.transform.rotation;
        data.rotation = new float[] {rot.x, rot.y, rot.z, rot.w};
        Vector3 scale = gameObject.transform.localScale;
        data.scale = new float[] {scale.x, scale.y, scale.z};
        data.isCollected = isCollected;
        data.isReturned = isReturned;
        return data;
    }
}

[Serializable]
public class SaveCollectableData
{
    public string name;
    public string meshName;
    public string materialName;
    public float[] position;
    public float[] rotation;
    public float[] scale;
    public bool isCollected;
    public bool isReturned;
}