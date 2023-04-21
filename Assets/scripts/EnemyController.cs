using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    
    public  void killenemy()
    {
        Destroy(transform.parent.gameObject);
    }
}
