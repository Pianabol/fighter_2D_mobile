using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Base Ayarlar")]
    public float attackRange = 3f; 
    
    protected Transform player;      // 'protected' diğer enemi sınıfları için
    protected bool hasAttacked = false; 
    protected bool isVulnerable = false;
    protected SpriteRenderer spriteRenderer; // Rengi değiştirmek için

    protected virtual void Start() // 'virtual' diğer enemiler için.
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnTap += HandleTap;
        }
    }

    protected virtual void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnTap -= HandleTap;
        }
    }

    protected virtual void Update()
    {
        if (player == null || hasAttacked) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < attackRange)
        {
            StartAttack();
        }
    }

    protected virtual void StartAttack()
    {
        hasAttacked = true;
        isVulnerable = true;

        Debug.Log(" Düşman Saldırıyor!");
        spriteRenderer.color = Color.yellow; 
        TimeManager.Instance.TriggerSlowMotion();

        Invoke("MissedOpportunity", 1.5f); 
    }

    // BURASI : Tank düşmanı burayı değiştirecek (Override)
    protected virtual void HandleTap()
    {
        if (isVulnerable)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("⚔️ Düşman Yok Edildi!");
        TimeManager.Instance.StopSlowMotion();
        CancelInvoke("MissedOpportunity");
        Destroy(gameObject);
    }

    protected virtual void MissedOpportunity()
    {
        if (isVulnerable) 
        {
            isVulnerable = false;
            
            // yeni kısm 
            // Oyuncunun canını azalt
            /*
            if (player != null)
            {
                // Oyuncudaki PlayerHealth scriptini bul
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1); // 1 Can götür
                }
            }
            */

            // --- DETAYLI KONTROL ---
            if (player != null)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1); 
                }
                else
                {
                    // Eğer bu yazarsa: Player objesi var ama script eksik/yanlış
                    Debug.LogError("HATA: PlayerHealth scripti Player objesinde bulunamadı!");
                }
            }
            else
            {
                // Eğer bu yazarsa: Player objesini hiç bulamamış
                Debug.LogError("HATA: Player objesi kayıp!");
            }
            // -----------------------
            Debug.Log(" Geç Kaldın!");
            TimeManager.Instance.StopSlowMotion();
            spriteRenderer.color = Color.red; 
        }
    }
}