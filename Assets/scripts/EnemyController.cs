using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private AudioSource enemydeathsound;
    public void killenemy()
    {
        Destroy(transform.parent.gameObject);
        enemydeathsound.Play();

        
    }
}
