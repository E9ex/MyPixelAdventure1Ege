using UnityEngine;
public class EnemyController : MonoBehaviour
{ 
    public  void killenemy()
    {
        // idleEnemydeki head'in tag=Enemy'dir.Eğer Enemy tag'ine etkileşim olursa head idleEnemy'nin child i oldugu için parent'ını burda oldurecek.
        Destroy(transform.parent.gameObject);
    }
}
