using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class OgmoEntity
{
    public float x;
    public float y;
    public string name;
}

[System.Serializable]
public class OgmoLayer
{
    public string name;
    public int[] data;
    public OgmoEntity[] entities;
    public int gridCellsX;
    public int gridCellWidth;
}


[System.Serializable]
public class OgmoLevel
{
    public OgmoLayer[] layers;
}

public class Placer : MonoBehaviour
{
    public TextAsset testLevel;
    public GameObject[] tiles;
    public GameObject pac;


    // Start is called before the first frame update
    void Start()
    {
        var level = JsonUtility.FromJson<OgmoLevel>(testLevel.text);
        var tileLayer = level.layers.Where(a => a.name == "tiles").First();
        var entityLayer = level.layers.Where(a => a.name == "entities").First();

        for (var i = 0; i < tileLayer.data.Length; i++)
        {
            var tileNumber = tileLayer.data[i];
            var prefab = tiles[tileNumber];

            var x = i % tileLayer.gridCellsX;
            var y = -i / tileLayer.gridCellsX;

            Instantiate(tiles[0], transform.position + new Vector3(x, y), Quaternion.identity);
            Instantiate(prefab, transform.position + new Vector3(x, y), Quaternion.identity);

        }


        foreach (var e in entityLayer.entities)
        {
            if (e.name == "Pac")
            {
                var newPosition = transform.position + new Vector3(e.x/entityLayer.gridCellWidth, -e.y/entityLayer.gridCellWidth);
                Instantiate(pac, newPosition, Quaternion.identity);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
