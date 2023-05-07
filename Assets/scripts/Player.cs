using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    private EnemyController _enemyController;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 7.5f;
    [SerializeField] private Animator anim;
    public AudioSource applecrunchsound, applecrunchextrasound, playerdeathsound,enemydeathsound;
    [SerializeField]  GameObject BulletPrefab;
    [SerializeField]  Transform BulletSpawn;
    [SerializeField]  Transform BulletTile;
    private enum MovementState {idle, run,jump,fall}//// Karakter animasyon durumu.

    void Start()
    { 
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (GameManager.isRestart)
        {
          _gameManager.startPanel.SetActive(false);
          _gameManager.bestScoreText.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (!GameManager.isStart)// Oyun başlamadıysa input işlenmez.
            return;
        PlayerShoot();//oyuncunun atış edebilmesi.
    }
    private void FixedUpdate()
    {
        if (!GameManager.isStart )// Oyun başlamadıysa input işlenmez.
            return;
        float h = Input.GetAxis("Horizontal");//Oyuncunun hareket edebilmesi sağlanır.
        Move(h);
        PlayerTurn(h);
        PlayerAnim(h);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Apple"))// Oyuncu apple çarptığında puanı arttırılır.
        {
            applecrunchsound.Play();
            Destroy(col.gameObject,0.2f);
            _gameManager.addPoints(5);
        }
        else if (col.CompareTag ("AppleExtra"))// Oyuncu AppleExtra yediğinde daha fazla puan kazanır.
        {
            applecrunchextrasound.Play();
            Destroy(col.gameObject,0.2f);
            _gameManager.addPoints(15);
        }
        else if (col.CompareTag("Enemy"))// Oyuncu düşmanla çarpıştığında düşman öldürülür ve puan kazanılır.
        {
            enemydeathsound.Play();
            _enemyController = col.GetComponentInParent<EnemyController>();
            _enemyController.killenemy();
           _gameManager.addPoints(20);
        }
        else if (col.CompareTag("End"))// Seviye sonuna ulaşıldığında bir sonraki seviyeye geçilir.
        {
            PlayerPrefs.SetInt("lastPlayedSceneIndex", SceneManager.GetActiveScene().buildIndex);
            _gameManager.NextLevel();
        }
        else if (col.CompareTag("Finish"))// Oyuncu bitiş çizgisine ulaştığında oyun yeniden başlatılır.
        {
            _gameManager.BackToStart();//karakter level10daki finish tag'ine sahip olan objeye çarptıgında ilk sahneye atar oyun yeniden baslar.
            if (GameManager.isStart)
            {
                _gameManager.startPanel.SetActive(false);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Death")
        {
            KillPlayer(collision.gameObject);//death tag'ine sahip olan enemy'in bodysine çarptıgımızda karakterimiz olucek.
            _gameManager.ReduceLives();//oyuncunun kalan can hakkını düşürür.
        }
    }
    #region hareket islemleri
    void Move(float h)
    {
        rb.velocity = new Vector2(h*speed,rb.velocity.y);// float h değişkenine fixedupdateki horizontal input axisi tanımlıyoruz.
    }
    #endregion
    #region karakter yon islemi
    void PlayerTurn(float h)//Bu bölüm oyuncunun yönünü değiştirir.
    {
        if (h > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (h < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    #endregion
    #region animasyon işlemleri 
    void PlayerAnim(float h)// Bu bölüm, animasyonu yönetir ve oyuncunun hareket durumuna göre animasyonu değiştirir.
    {
        MovementState state;
        if (h != 0)
            state = MovementState.run;//parametrelerde kostumu zıpliyormu dustumu kullanılmıyor orda durmalarının sebebi silemiyorum buglu silinmiyor.
        else
            state = MovementState.idle;
        if (rb.velocity.y > .1f)
            state = MovementState.jump;
        else if (rb.velocity.y < -.1f)
            state = MovementState.fall;
        
        anim.SetInteger("state",(int)state); // animasyon durumunu günceller.
    }
    #endregion
    #region PlayerShoot
    void PlayerShoot() // Bu bölüm oyuncu ateş ederken kullanılır. "P" tuşuna basarak mermi oluşturulur.
    {
        if (Input.GetKeyDown(KeyCode.P))
        { 
            Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity, BulletTile);
        }
    }
    #endregion
    #region olme işlemi
    public void KillPlayer(GameObject p)//oyuncunun ölümünü tetiklenir.
    {
            playerdeathsound.Play();
            Destroy(gameObject,0.5f);
            anim.SetTrigger("Death");
    }
    #endregion

}
