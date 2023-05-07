using UnityEngine;
public class WaypointController : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;//waypoints dizisi objelerin hareket edeceği noktaları tutacak.
    private int currentWayPointIndex = 0; // waypoints dizisindeki o anki hedef noktanın index numarasını tutar.
    [SerializeField] private float speed = 2f;//waypoints üzerinde hareket eden objelerin hızını tutacak.
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position,transform.position)<.1f)
        { 
            currentWayPointIndex++;// Dizideki bir sonraki hedef noktaya geçer.
            if (currentWayPointIndex>=waypoints.Length)// Eğer dizi sonuna gelinmişse
            {
                currentWayPointIndex = 0;// Dizinin başına döner.
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position,
            Time.deltaTime*speed); // Objeyi, anlık pozisyonundan, bir sonraki hedef noktaya hareket sağlayarak ilerletir.
    }
}//class
