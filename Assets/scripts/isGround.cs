using UnityEngine;
public class isGround : MonoBehaviour
{
    [SerializeField]  LayerMask layer;// bu değişken 'Raycast' işlemi için kullanılan layer'ı belirler.
    public  bool IsGround = true;// bu değişken zemine dokunulup dokunulmadığını kontrol eder.
    [SerializeField] Rigidbody2D rb;
    public float jumpspeed = 10f;
    void Update()
    {
        if (!GameManager.isStart) // oyun henüz başlamadıysa işlemleri gerçekleştirmez.
            return;

        // Raycast işlemi ile yere temas edilip edilmediğini kontrol eder.
        RaycastHit2D iscollider = Physics2D.Raycast(transform.position, Vector2.down, 0.10f, layer); 

        if (iscollider != null) // raycast işlemi sonucunda bir nesne tespit edildiyse
            IsGround = true;
        else 
            IsGround = false;// zemine dokunulmadığında 'IsGround' değişkeni false değerini alır.

        if (IsGround == true && Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)//doublejump off.
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);// oyuncu zemine dokunuyorsa ve SPACE tuşuna basıldıysa oyuncuyu zıplatabilir.
    }
}

    
    



