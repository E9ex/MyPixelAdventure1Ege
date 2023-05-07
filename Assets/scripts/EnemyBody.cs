using UnityEngine;
public class EnemyBody : MonoBehaviour
{
    public int health = 100; 
    public void Takedamage(int damage)
    {
        health -=damage ;
        if (health <= 0)
        {
            Die();// body'e gelen mermiler ile 0'a ulaşırsa enemy'in canı öldür.
            GameManager manager = FindObjectOfType<GameManager>();
            manager.Score += 8;// ve Score 8 Puan ekle.
        }
    } 
    public void Die()
    {
        Destroy(transform.parent.gameObject);
    }
}
