using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDrawGizmos()
     {
         Gizmos.color = Color.red;
         Gizmos.DrawWireCube(transform.position, new Vector3(1.5f, 1.5f, 1.5f));
     }
}
