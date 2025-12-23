using UnityEngine;
using UnityEngine.UI; // UI işlemleri için  

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elemanları")]
    public Slider healthBar; // Unity'deki Slider'ı buraya bağlayacağız

    private void Awake()
    {
        // Basit Singleton (Sadece bu sahnede yaşayacak)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Can değişince bu fonksiyonu çağıracağız
    public void UpdateHealth(int currentHealth)
    {
        // KONTROL: Eğer elimizdeki HealthBar yok olduysa (Sahne yenilendiği için)
        if (healthBar == null)
        {
            // Sahnedeki "HealthBar" isimli objeyi bul ve Slider'ını al
            GameObject barObj = GameObject.Find("HealthBar");
            if (barObj != null)
            {
                healthBar = barObj.GetComponent<Slider>();
                healthBar.minValue = 0;
                healthBar.maxValue = 3; // Zorla 3 yapıyoruz!
            }
        }

        // Şimdi güvenle güncelleyebiliriz
        if (healthBar != null)
        {
            if (healthBar.maxValue != 3) healthBar.maxValue = 3;

            healthBar.value = currentHealth;
            
            // Debug için konsola yazdıralım
            Debug.Log($"UI GÜNCELLENDİ: {currentHealth} / 3");
        }
        else
        {
            Debug.LogWarning("UIManager: HealthBar sahneden bulunamadı! İsmi 'HealthBar' mı?");
        }
    }
}