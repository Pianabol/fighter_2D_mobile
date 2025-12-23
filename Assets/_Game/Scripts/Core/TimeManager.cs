using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Singleton: Her yerden erişebilmek için
    public static TimeManager Instance { get; private set; }

    [Header("Slow Motion Ayarları")]
    public float slowMotionFactor = 0.1f; // Zaman ne kadar yavaşlasın? (0.1 = 10 saniye sürüyor.)
    public float slowMotionDuration = 2f; // Gerçek dünyada ne kadar sürsün?

    private float _defaultFixedDeltaTime; // Oyunun orijinal fizik hızı

    private void Awake()
    {
        // Singleton Kurulumu
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Orijinal fizik hesaplama hızını kaydediyoruz
        _defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Bu fonksiyonu çağırdığımızda zaman yavaşlayacak
    public void TriggerSlowMotion()
    {
        Time.timeScale = slowMotionFactor;
        // Fizik motorunu da yavaşlatmak zorundayız yoksa karakter titrer
        Time.fixedDeltaTime = Time.timeScale * 0.02f; 
        Debug.Log(" ZAMAN YAVAŞLADI!"); 
    }

    // Bu fonksiyonu çağırdığımızda zaman normale dönecek
    public void StopSlowMotion()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = _defaultFixedDeltaTime;
        Debug.Log(" ZAMAN NORMAL!");
    }
}