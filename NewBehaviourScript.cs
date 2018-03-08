using UnityEngine;
using System.Collections;
using System.Resources;
using System.IO;
using System;
using System.Linq; // used for Sum of array

public class NewBehaviourScript : MonoBehaviour
{
    #region Variables
        
    int ttype = 1;//izberi 1 ili 2 za razlichno ocvetqvane
    //public float height2;
    
    #endregion

    public void LoadTerrainScript(string fileName)
    {
        //string fileName = "assets/elevation.bhm";
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
                if (intData[i] > 0 && minValue > intData[i])
                    minValue = intData[i];
                if (maxValue < intData[i])
                    maxValue = intData[i];
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
            terrainData.size = new Vector3(1000, 400, 1000);
            GameObject terrain = Terrain.CreateTerrainGameObject(terrainData);
            //Instantiate(terrain, Vector3.zero, Quaternion.identity);
            //Instantiate(terrain, new Vector3 (1000, 0, 0), Quaternion.identity);
            //Instantiate(terrain, new Vector3(1000, 0, 1000), Quaternion.identity);
            //Instantiate(terrain, new Vector3(0, 0, 1000), Quaternion.identity);
            
        }
    }

    public void GeoTexturing()
    {        
        //Terrain terrain = GetComponent<Terrain>();
        Terrain terrain = Terrain.activeTerrain;

        SplatPrototype[] mytextures = new SplatPrototype[4];

        mytextures[0] = new SplatPrototype();
        mytextures[0].texture = (Texture2D)Resources.Load("Brown", typeof(Texture2D));
        if (mytextures[0].texture == null)
        {
            Debug.Log("Loading 1st texture failed");
        }
        if (mytextures[0].texture != null)
        {
            mytextures[0].tileOffset = new Vector2(0, 0);
            mytextures[0].tileSize = new Vector2(15, 15);
        }

        mytextures[1] = new SplatPrototype();
        mytextures[1].texture = (Texture2D)Resources.Load("Yellow", typeof(Texture2D));
        if (mytextures[1].texture == null)
        {
            Debug.Log("Loading 2nd texture failed");
        }
        if (mytextures[1].texture != null)
        {
            mytextures[1].tileOffset = new Vector2(0, 0);
            mytextures[1].tileSize = new Vector2(15, 15);
        }

        mytextures[2] = new SplatPrototype();
        mytextures[2].texture = (Texture2D)Resources.Load("Green", typeof(Texture2D));
        if (mytextures[2].texture == null)
        {
            Debug.Log("Loading 3rd texture failed");
        }
        if (mytextures[2].texture != null)
        {
            mytextures[2].tileOffset = new Vector2(0, 0);
            mytextures[2].tileSize = new Vector2(15, 15);
        }

        mytextures[3] = new SplatPrototype();
        mytextures[3].texture = (Texture2D)Resources.Load("Low", typeof(Texture2D));
        if (mytextures[3].texture == null)
        {
            Debug.Log("Loading 4th texture failed");
        }
        if (mytextures[0].texture != null && mytextures[1].texture != null && mytextures[2].texture != null && mytextures[3].texture != null)
        {
            mytextures[3].tileOffset = new Vector2(0, 0);
            mytextures[3].tileSize = new Vector2(15, 15);

            terrain.terrainData.splatPrototypes = mytextures;
        }
    }

    public void WorldTexturing()
    {
        //Terrain terrain = GetComponent<Terrain>();
        Terrain terrain = Terrain.activeTerrain;

        SplatPrototype[] mytextures = new SplatPrototype[4];

        mytextures[0] = new SplatPrototype();
        mytextures[0].texture = (Texture2D)Resources.Load("White", typeof(Texture2D));
        if (mytextures[0].texture == null)
        {
            Debug.Log("Load 1 Texture Fail");
        }
        if (mytextures[0].texture != null)
        {
            mytextures[0].tileOffset = new Vector2(0, 0);
            mytextures[0].tileSize = new Vector2(15, 15);
        }

        mytextures[1] = new SplatPrototype();
        mytextures[1].texture = (Texture2D)Resources.Load("Yellow", typeof(Texture2D));
        if (mytextures[1].texture == null)
        {
            Debug.Log("Load 2 Texture Fail");
        }
        if (mytextures[1].texture != null)
        {
            mytextures[1].tileOffset = new Vector2(0, 0);
            mytextures[1].tileSize = new Vector2(15, 15);
        }

        mytextures[2] = new SplatPrototype();
        mytextures[2].texture = (Texture2D)Resources.Load("Red", typeof(Texture2D));
        if (mytextures[2].texture == null)
        {
            Debug.Log("Load 3 Texture Fail");
        }
        if (mytextures[2].texture != null)
        {
            mytextures[2].tileOffset = new Vector2(0, 0);
            mytextures[2].tileSize = new Vector2(15, 15);
        }

        mytextures[3] = new SplatPrototype();
        mytextures[3].texture = (Texture2D)Resources.Load("RockBlocky0010_2_S", typeof(Texture2D));
        if (mytextures[3].texture == null)
        {
            Debug.Log("Load 4 Texture Fail");
        }
        if (mytextures[0].texture != null && mytextures[1].texture != null && mytextures[2].texture != null && mytextures[3].texture != null)
        {
            mytextures[3].tileOffset = new Vector2(0, 0);
            mytextures[3].tileSize = new Vector2(15, 15);

            terrain.terrainData.splatPrototypes = mytextures;
        }
    }

    public void SetGeoTextures()
    {
        // Get the attached terrain component
        //Terrain terrain = GetComponent<Terrain>();
        Terrain terrain = Terrain.activeTerrain;


        // Get a reference to the terrain data
        TerrainData terrainData = terrain.terrainData;

        //terrainData.size = new Vector3(1000, 600, 1000);
        //GameObject terrain = Terrain.CreateTerrainGameObject(terrainData);


        // Splatmap data is stored internally as a 3d array of floats, so declare a new empty array ready for your custom splatmap data:
        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                // Normalise x/y coordinates to range 0-1 
                float y_01 = (float)y / (float)terrainData.alphamapHeight;
                float x_01 = (float)x / (float)terrainData.alphamapWidth;

                // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
                float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapHeight), Mathf.RoundToInt(x_01 * terrainData.heightmapWidth));
                //height2 = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapHeight), Mathf.RoundToInt(x_01 * terrainData.heightmapWidth));

                // Calculate the normal of the terrain (note this is in normalised coordinates relative to the overall terrain dimensions)
                Vector3 normal = terrainData.GetInterpolatedNormal(y_01, x_01);

                //float mheight = Toin normal;

                // Calculate the steepness of the terrain
                float steepness = terrainData.GetSteepness(y_01, x_01);

                // Setup an array to record the mix of texture weights at this point
                float[] splatWeights = new float[terrainData.alphamapLayers];

                // CHANGE THE RULES BELOW TO SET THE WEIGHTS OF EACH TEXTURE ON WHATEVER RULES YOU WANT

                // Texture[0] has constant influence
                splatWeights[0] = 0.5f;

                // Texture[1] is stronger at lower altitudes
                //splatWeights[1] = Mathf.Clamp01(((3*terrainData.heightmapHeight)/4) - height);
                splatWeights[1] = height*Mathf.Clamp01(terrainData.heightmapHeight - ((height * 115) / 100));

                // Texture[2] stronger on flatter terrain
                // Note "steepness" is unbounded, so we "normalise" it by dividing by the extent of heightmap height and scale factor
                // Subtract result from 1.0 to give greater weighting to flat surfaces
                splatWeights[2] = height * Mathf.Clamp01(terrainData.heightmapHeight - ((height * 170) / 100));

                // Texture[3] increases with height but only on surfaces facing positive Z axis 
                splatWeights[3] = height * Mathf.Clamp01(terrainData.heightmapHeight - ((height * 350) / 100));

                // Sum of all textures weights must add to 1, so calculate normalization factor from sum of weights
                float z = splatWeights.Sum();

                // Loop through each terrain texture
                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {

                    // Normalize so that sum of all texture weights = 1
                    splatWeights[i] /= z;

                    // Assign this point to the splatmap array
                    splatmapData[x, y, i] = splatWeights[i];
                }
            }
        }

        // Finally assign the new splatmap to the terrainData:
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }

    public void SetWorldTextures()
    {
        // Get the attached terrain component
        //Terrain terrain = GetComponent<Terrain>();
        Terrain terrain = Terrain.activeTerrain;


        // Get a reference to the terrain data
        TerrainData terrainData = terrain.terrainData;

        // Splatmap data is stored internally as a 3d array of floats, so declare a new empty array ready for your custom splatmap data:
        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                // Normalise x/y coordinates to range 0-1 
                float y_01 = (float)y / (float)terrainData.alphamapHeight;
                float x_01 = (float)x / (float)terrainData.alphamapWidth;

                // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
                float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapHeight), Mathf.RoundToInt(x_01 * terrainData.heightmapWidth));

                // Calculate the normal of the terrain (note this is in normalised coordinates relative to the overall terrain dimensions)
                Vector3 normal = terrainData.GetInterpolatedNormal(y_01, x_01);

                // Calculate the steepness of the terrain
                float steepness = terrainData.GetSteepness(y_01, x_01);

                // Setup an array to record the mix of texture weights at this point
                float[] splatWeights = new float[terrainData.alphamapLayers];

                // CHANGE THE RULES BELOW TO SET THE WEIGHTS OF EACH TEXTURE ON WHATEVER RULES YOU WANT

                // Texture[0] has constant influence
                splatWeights[0] = 0.5f;
                //splatWeights[0] = new SplatPrototype();
                //splatWeights[0].texture = (Texture2D)Resources.Load("Terrain Assets/Terrain Textures/Grass (Hill)");

                // Texture[1] is stronger at lower altitudes
                splatWeights[1] = Mathf.Clamp01((terrainData.heightmapHeight - height));

                // Texture[2] stronger on flatter terrain
                // Note "steepness" is unbounded, so we "normalise" it by dividing by the extent of heightmap height and scale factor
                // Subtract result from 1.0 to give greater weighting to flat surfaces
                splatWeights[2] = 1.0f - Mathf.Clamp01(steepness * steepness / (terrainData.heightmapHeight / 5.0f));

                // Texture[3] increases with height but only on surfaces facing positive Z axis 
                splatWeights[3] = height * Mathf.Clamp01(normal.z);

                // Sum of all textures weights must add to 1, so calculate normalization factor from sum of weights
                float z = splatWeights.Sum();

                // Loop through each terrain texture
                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {

                    // Normalize so that sum of all texture weights = 1
                    splatWeights[i] /= z;

                    // Assign this point to the splatmap array
                    splatmapData[x, y, i] = splatWeights[i];
                }
            }
        }

        // Finally assign the new splatmap to the terrainData:
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }

    // Use this for initialization
    void Start()
    {
        LoadTerrainScript("assets/elevation.bhm");

        Terrain terrain = Terrain.activeTerrain;
        TerrainData terrainData = terrain.terrainData;
        Debug.Log(terrainData.heightmapHeight);
        Debug.Log(terrainData.heightmapScale);

        //for (int y = 0; y < terrainData.alphamapHeight; y++)
        //{
        //    for (int x = 0; x < terrainData.alphamapWidth; x++)
        //    {
        //        // Normalise x/y coordinates to range 0-1 
        //        float y_01 = (float)y / (float)terrainData.alphamapHeight;
        //        float x_01 = (float)x / (float)terrainData.alphamapWidth;

        //        // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
        //        height2 = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapHeight), Mathf.RoundToInt(x_01 * terrainData.heightmapWidth));
        //    }
        //}
        

        
        if (ttype == 1)
        { 
            GeoTexturing();
            Debug.Log("You chose geographic textures.");
            SetGeoTextures();
            Debug.Log("It is done.");
            //Debug.Log(height2);
        }
        else if (ttype == 2)
        {
            WorldTexturing();
            Debug.Log("You chose world view textures.");
            SetWorldTextures();
            Debug.Log("It is done.");
        }
        else
        { Debug.Log("Which textures to choose?"); }

        //Debug.Log(height2);

    }

    //Update is called once per frame
    void Update()
    {

    }
}

