using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Collections;

public class Menu : MonoBehaviour
{

    #region Variables

    public bool showA = true;
    public bool showB = true;
    bool ispath = false;
    bool txerror = false;
    string path;
    string extension;
            
    public Rect Box;
    public string selectedItem = "Draw style A";

    private bool editing = false;

    bool paused = true;

    #endregion

    void Start()
    {

        FileSelect();
    }
    
    void OnGUI()
    {
        if (paused)
        {
            #region Buttons

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            string[] options = new string[]
        {
        "2", "3", "4", "5",
        };

            Box.height = 75;
            Box.width = 150;
            Box.x = 200;
            Box.y = 200;
            if (showA && GUI.Button(Box, selectedItem))
            {
                editing = true;
            }

            if (showA && editing)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (GUI.Button(new Rect(Box.x, (Box.height * x) + Box.y + Box.height, Box.width, Box.height), options[x]))
                    {
                        selectedItem = options[x];
                        editing = false;

                        TextureSelect();

                        if (txerror == false)
                        {
                            showA = false;
                            showB = true;
                            PlayerPrefs.SetString("File", path);
                            PlayerPrefs.SetInt("Type", 1);
                            PlayerPrefs.SetInt("Colours", Int32.Parse(selectedItem));

                            Application.LoadLevel(1);
                            paused = togglePause();
                        }
                        else
                        {
                            txerror = false;
                        }
                    }
                }
            }

            GUI.Label(new Rect(500, 100, 200, 75), "Choose draw style!");

            if (showB && GUI.Button(new Rect(370, 200, 150, 75), "Draw style B"))
            {
                showA = true;
                showB = false;
                PlayerPrefs.SetString("File", path);
                PlayerPrefs.SetInt("Type", 2);
                Application.LoadLevel(1);
                paused = togglePause();
            }

            if (showA == false && GUI.Button(new Rect(Box.x, Box.y, Box.width, Box.height), "Resume"))
            {
                paused = togglePause();
            }

            if (showB == false && GUI.Button(new Rect(370, 200, 150, 75), "Resume"))
            {
                paused = togglePause();
            }

            if (GUI.Button(new Rect(540, 200, 150, 75), "Choose another map"))
            {
                ispath = false;
                FileSelect();
            }

            if (GUI.Button(new Rect(710, 200, 150, 75), "Quit"))
            {
                PlayerPrefs.DeleteAll();
                Application.Quit();
                Debug.Log("Krai");
            }

            #endregion
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void FileSelect()
    {
        extension = null;
        while (ispath == false && extension != "bhm")
        {
            EditorUtility.DisplayDialog("Select Heightmap", "Please select Heightmap file!", "Ok");
            path = EditorUtility.OpenFilePanel("Select Heightmap", "", "bhm");
            extension = path.Substring(path.IndexOf('.') + 1);

            if (path.Length != 0 && extension == "bhm")
            {
                ispath = true;
                Debug.Log(path);
            }

            if (path.Length == 0 && Box.height == 75)
            {
                //hahaha
                ispath = true;
                //PlayerPrefs.DeleteAll();
                //Application.Quit();
                //Debug.Log("pauza");
            }
            
            if (path.Length == 0 && Box.height != 75)
            {
                ispath = true;
                PlayerPrefs.DeleteAll();
                Application.Quit();
                Debug.Log("Krai");
            }
        }
    }

    public void TextureSelect()
    {
        string[] texExt = new string[Int32.Parse(selectedItem)];
        string[] tpath = new string[Int32.Parse(selectedItem)];

        for (int i = 0; i < Int32.Parse(selectedItem); i++)
        {
            texExt[i] = null;
            while (txerror == false && texExt[i] != "png")
            {
                EditorUtility.DisplayDialog("Select Texture #" + (i + 1), "Please select Texture file #" + (i + 1), "Ok");
                tpath[i] = EditorUtility.OpenFilePanel("Select Texture#" + (i + 1), "", "png");
                texExt[i] = tpath[i].Substring(tpath[i].IndexOf('.') + 1);

                if (tpath[i].Length != 0 && texExt[i] == "png")
                {
                    PlayerPrefs.SetString("Texture" + i, tpath[i]);
                    Debug.Log(i);
                    Debug.Log(tpath[i]);
                }

                if (tpath[i].Length == 0)
                {
                    txerror = true;
                    i = Int32.Parse(selectedItem) + 1;
                    PlayerPrefs.DeleteAll();
                }
                
        }
    }
}

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    bool togglePause()
    {
        if (Time.timeScale != 0f && paused == true)
        {
            Time.timeScale = 1f;
            return (false);
        }
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
            return (true);
        }
        else
        {
            Time.timeScale = 1f;
            return (false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("`"))
            paused = togglePause();
    }

}


