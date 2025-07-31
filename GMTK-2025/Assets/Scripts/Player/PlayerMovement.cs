using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PathBehaviour path;
    int nextHex = 1;
    GameObject currentNextHex;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        currentNextHex = path.pathHexObjects[nextHex];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentNextHex.transform.position, moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, currentNextHex.transform.position) <= 0.01f)
        {
            nextHex++;

            if(nextHex >= path.pathHexObjects.Length)
            {
                nextHex = 0;
            }

            currentNextHex = path.pathHexObjects[nextHex];
        }
    }
}
