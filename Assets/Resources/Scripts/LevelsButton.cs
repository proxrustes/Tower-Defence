using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsButton : MonoBehaviour
{
    public string scene;

    private BoxCollider2D collider;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Mouse0) && MyPhys.MouseRaycast(collider))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
