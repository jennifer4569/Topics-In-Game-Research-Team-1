using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CollectableManager : MonoBehaviour
{
    // public GameObject collectablePrefab;
    public Collectable collectablePrefab;
    public bool isLevel = true;

    // Start is called before the first frame update
    void Start()
    {
        loadCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            saveCollectables();
        }
    }

    void loadCollectables()
    {
        if(File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveAllCollectableData data = (SaveAllCollectableData)bf.Deserialize(file);
            file.Close();

            foreach(SaveCollectableData collectableData in data.allCollectables)
            {
                //load in collectables from file
                Collectable c = Instantiate(collectablePrefab);
                c.LoadSaveData(collectableData);
            }
        }
        else
        {
            //load in new collectables
        }
    }

    void saveCollectables()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveAllCollectableData data = new SaveAllCollectableData();

        data.allCollectables = new List<SaveCollectableData>();
        Collectable[] collectables = FindObjectsOfType<Collectable>();
        foreach(Collectable collectable in collectables)
        {
            data.allCollectables.Add(collectable.GetSaveData());
        }

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
class SaveAllCollectableData
{
    public List<SaveCollectableData> allCollectables;
}
