using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
public class GameManager : MonoBehaviour
{
    [SerializeField] Button Continue;
    [SerializeField] GameObject Mute, unmute;
    [SerializeField] private Image[] PlayerLives;
    [SerializeField] private int playerLife = 3;
    [SerializeField] public Text scoreText,lastScoreText,bestScoreText;
    public GameObject restartPanel, startPanel;
    public static bool isRestart = false;
    [SerializeField] int score = 0;
    public int Score
    {                                 
        get => score;
        set
        {
            score = value;//puanı güncelleyecek.
            PlayerPrefs.SetInt("score", score);//puanı oyuncunun cihazında saklar.
            scoreText.text = score.ToString();// puanı ekran gösterir.
        }
    }
    [SerializeField] int bestscore = 0;
    public int BestScore
    {
        get => bestscore;
        set
        {
            bestscore = value;// en yüksek puanı güncelleyecek.
            PlayerPrefs.SetInt("BestScore", bestscore);// en yüksek puanı oyuncunun cihazında saklayacak.
            bestScoreText.text = "Best Score: " + bestscore;// en yüksek puanı ekranda gösterecek.
        }
    }
    public static bool isStart = false;
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    
    private void Awake()
    {
        playerLife = PlayerPrefs.GetInt("PlayerLife", 3);// oyuncunun can sayısı oyuncunun cihazında saklı ise onu getir
        BestScore = PlayerPrefs.GetInt("BestScore", 0);// en yüksek puan oyuncunun cihazında saklı ise onu getir
        Score = PlayerPrefs.GetInt("score");// puan oyuncunun cihazında saklı ise onu getir
        if(Continue)//button 
            if (BestScore > 0 || Score > 0 || playerLife != 3)// Oyuncunun yüksek puanı, mevcut puanı veya 3'ten az canı varsa, "devam et" düğmesi true olur.
                Continue.interactable = true;
        
        if (playerLife == 0)// oyuncunun can sayısı sıfır ise
        {
            PlayerPrefs.SetInt("PlayerLife", 3); // oyuncunun can sayısını yeniden 3 yap
            Score = 0;// puanı sıfırla
            playerLife = 3;// oyuncunun can sayısını yeniden 3 yap
        }
        for (int i = 0; i < 3; i++)// oyuncunun can göstergelerini göster veya gizle
        {
            if(i<playerLife)
                PlayerLives[i].gameObject.SetActive(true);// Oyuncunun kalan canlarına göre doğru sayıda can göstergesini gösterir.
            else
                PlayerLives[i].gameObject.SetActive(false);// Kalan can göstergelerini gizler.
        }
    }
    private void Start()
    {
        SpawnPlayer(); //başlangıçta player spawn olması için.
    }

    #region degerleriResetleme//quitgame ve restartGame de calısıyor.
    private void ResetValues()
    {
        PlayerPrefs.SetInt("PlayerLife", 3); // Oyuncunun hayatını 3'e sıfırlar
    }
    #endregion
    #region ScoreEkleme
    public void addPoints(int point)
    {
        Score += point;  // Oyuncunun puanına puan ekler.
        if (bestscore < score)// Oyuncunun mevcut puanı en iyi puanından yüksekse, en iyi puanı güncellenir.
            BestScore = score;
    }
    #endregion
    #region PlayerHealth
    public void ReduceLives()
    {
        playerLife--;// Oyuncunun kalan canını azaltır.
        PlayerPrefs.SetInt("PlayerLife", playerLife);// Yeni can sayısını PlayerPrefs'e kaydeder.
        PlayerLives[playerLife].gameObject.SetActive(false);
        if (playerLife < 1 )
        {
            isStart = false;
            restartPanel.SetActive(true);
            startPanel.SetActive(false);
            lastScoreText.text = "Last Score: " + score;// lastscore metnini oyuncunun son skoru ile günceller.
            PlayerPrefs.SetInt("lastPlayedSceneIndex", SceneManager.GetActiveScene().buildIndex);// Geçerli sahnenin dizinini PlayerPrefs'e kaydeder.
            return;
        }
        StartCoroutine(Delay());//  gecikmeden sonra respawn.
    }
    #endregion
    #region yenidenBaslatma
    public void RestartGame()
    {
        isStart = true;
        startPanel.SetActive(false);
        restartPanel.SetActive(false);
        isRestart = true;
        ResetValues();// Oyuncunun hayatını 3'e sıfırlar.
        Score = 0;// Oyuncunun puanını 0'a sıfırlar.
        PlayerPrefs.SetInt("lastPlayedSceneIndex",0);// Oynatılan son sahnenin indeksini 0 (ilk sahne) olarak ayarlar.
        SceneManager.LoadScene("level1");//can  hakkımız hangi levelde biterse bitsin ilk level'e atacak.
    }
    #endregion
    #region SpawnveReSpawnIslemleri
    void SpawnPlayer()
    {
        Instantiate(player, spawnPoint.position, quaternion.identity);
    }
   public void ReSpawnPlayer()
    {
        Instantiate(player, spawnPoint.position, quaternion.identity);
    }
   IEnumerator Delay()
   {
       yield return new WaitForSeconds(1f);//respawn için delay.
       ReSpawnPlayer();
   }
   #endregion
    #region cikis islemi
    public void QuitGame()
    {
        Application.Quit();
        ResetValues();//quit buttona tıklanıldıgında değerleri resetleyecek.
        PlayerPrefs.Save();
    }
    #endregion
    #region FinishLinetoLevel1
    public void BackToStart()
    {
        SceneManager.LoadScene(0);// ilk sahneyi yükler.
        isStart = false;
        startPanel.SetActive(true);
    }
    #endregion
    #region levelgecme
    public void NextLevel()
    {
        isStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//sonraki leveli  yüklemek için.
    }
    #endregion
    #region StartIslemleri
    public void PlayGame()
    {
        isStart = true;
        startPanel.SetActive(false);
        Score = 0;
    }
    #endregion
    #region KapatmaIslemleri
    private void OnDisable()
    {
        PlayerPrefs.SetInt("lastPlayedSceneIndex", SceneManager.GetActiveScene().buildIndex);// Oyun devre dışı bırakıldığında son oynanan sahne indeksini kaydetme işlevi.
    }
    #endregion
   
    


   
        
}