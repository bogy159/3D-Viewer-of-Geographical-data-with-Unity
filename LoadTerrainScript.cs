using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class LoadTerrainScript : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
    
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }

    public void LoadTerrain()
    {
        string fileName = "elevation.bhm";
        using (FileStream stream = new FileStream(fileName, FileMode.Open))
        {
            BinaryReader reader = new BinaryReader(stream);
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();

            int[] intData = new int[width * height];
            int minValue = Int32.MaxValue, maxValue = 0;
            for (int i = 0; i < width * height; i++)
            {
                intData[i] = reader.ReadInt32();
                if (intData[i] < 0)
                    intData[i] = 0;
                if (intData[i] > 0 && minValue > intData[i]) minValue = intData[i];
                if (maxValue < intData[i]) maxValue = intData[i];
            }
            for (int i = 0; i < width * height; i++)
            {
                if (intData[i] >= minValue)
                {
                    intData[i] = intData[i] - minValue;
                }
            }
            maxValue = maxValue - minValue;
            float maxValueF = maxValue;
            int resolution = 2049;
            if (width < resolution)
                resolution = width;
            if (height < resolution)
                resolution = height;

            float[,] data = new float[resolution, resolution];
            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    data[x, y] = intData[y * width + x] / maxValueF;
                }
            }

            TerrainData terrainData = new TerrainData();
            terrainData.heightmapResolution = resolution;
            terrainData.SetHeights(0, 0, data);            
            terrainData.size = new Vector3(1000, 100, 1000);
            GameObject terrain = Terrain.CreateTerrainGameObject(terrainData);
            Instantiate(terrain, Vector3.zero, Quaternion.identity);
            //Instantiate(terrain, new Vector3 (1000, 0, 0), Quaternion.identity);
            //Instantiate(terrain, new Vector3(1000, 0, 1000), Quaternion.identity);
            //Instantiate(terrain, new Vector3(0, 0, 1000), Quaternion.identity);
        }
    }
}
