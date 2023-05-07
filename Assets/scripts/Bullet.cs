using UnityEngine;
public class Bullet : MonoBehaviour
{
    public int damage = 40;// merminin verdiği hasar tek seferde.
    private void Update()
    {
        transform.Translate(Vector3.right*10f*Time.deltaTime);// mermi hareketi.
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        EnemyBody enemyBody= col.GetComponent<EnemyBody>(); 
        if (enemyBody!=null )// enemyBody değişkeni null değilse, yani enemy ile çarpışmışsa
       {
           Destroy(gameObject);//bullet destroy.
           enemyBody.Takedamage(damage); // çarptığı düşmana hasar verilir
       }
       else if (col.CompareTag("Bulletdest")) // eğer mermi çarpıştığı obje bir "Bulletdest" tag'ine sahipse
       {
           Destroy(gameObject);//mermi yok edilir mermi sonsuza kadar gitmesin diye.
       }
    }
}
