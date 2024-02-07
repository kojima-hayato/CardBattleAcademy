using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Warp_KB : MonoBehaviour
{
    public CinemachineVirtualCamera camera;

    StringBuilder sb;

    GameObject warpTarget;
    string collisionName, targetName;

    bool isAfterWarp = false;

    // Start is called before the first frame update
    void Start()
    {
        sb = new StringBuilder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAfterWarp)
        {
            collisionName = collision.gameObject.name;

            if (collisionName.StartsWith("Warp"))
            {
                sb = new StringBuilder(collisionName);

                if (collisionName.EndsWith("1"))
                {
                    sb[sb.Length - 1] = '2';
                }
                else if (collisionName.EndsWith("2"))
                {
                    sb[sb.Length - 1] = '1';
                }

                targetName = sb.ToString();

                warpTarget = GameObject.Find(targetName);

                if (warpTarget != null)
                {
                    camera.transform.position = warpTarget.transform.position;
                    transform.position = warpTarget.transform.position;

                    isAfterWarp = true;
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isAfterWarp)
        {
            if (targetName == collision.gameObject.name)
            {
                collisionName = "";
                sb = new StringBuilder();
                targetName = "";
                isAfterWarp = false;
            }
        }
    }
}
