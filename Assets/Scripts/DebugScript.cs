using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{

    public bool collisionDisabled = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {

        if (Input.GetKey(KeyCode.L))
        {

            GetComponent<CollisionHandler>().LoadNextLevel();
        }

        else if (Input.GetKey(KeyCode.C))

            {

            collisionDisabled = !collisionDisabled; // toggle collision on or off

        }
    }
}
