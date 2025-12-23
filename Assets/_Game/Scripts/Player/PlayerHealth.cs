using UnityEngine;
using UnityEngine.SceneManagement; // Bölüm yeniden başlatmak için

public class PlayerHealth : MonoBehaviour
{
    [Header("Can Ayarları")]
    public int maxHealth = 3; // Toplam 3 can 
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;  //oyun başı
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        //Ne çok hata verdi be
        // Singleton (Instance) yerine, sahnedeki UIManager'ı zorla bulup getiriyoruz.
        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null)
        {
            ui.UpdateHealth(currentHealth);
        }
        else
        {
            Debug.LogError("HATA: Sahnede UIManager bulunamadı!");
        }

        Debug.Log($" HASAR ALDIN! Kalan Can: {currentHealth}");

        // Ekran sallantısı veya kırmızı efekt ilerde buraya eklenecek

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(" OYUNCU ÖLDÜ!");
        // Şimdilik sahneyi baştan başlatalım (Basit Game Over)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}