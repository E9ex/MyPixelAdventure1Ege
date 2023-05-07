using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveSystem : MonoBehaviour
{
    private GameManager Manager;
    private int lastSceneIndex = 0;// son oynanan sahnenin index numarasını tutar.
    private void Awake()
    {
        Manager = GetComponent<GameManager>();
        lastSceneIndex = PlayerPrefs.GetInt("lastPlayedSceneIndex",0);// Daha önce kaydedilen sahnenin index numarasını 'PlayerPrefs' ile alır. Eğer böyle bir index yoksa 0 değerini atar.
    } 
    public void LoadScene()
    {
        SceneManager.LoadScene(lastSceneIndex);// 'lastSceneIndex' değişkeninde tutulan son kaydedilen sahneyi yükler.
        GameManager.isStart =true; // oyunun başlatıldığını belirten değişkeni true yapar.
    }
}
