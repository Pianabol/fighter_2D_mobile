using UnityEngine;

public class TankEnemy : Enemy
{
    [Header("Tank Ayarları")]
    public int health = 2; // Bu düşmanın 2 canı var!

    protected override void Start()
    {
        base.Start(); 
        damagePower = 2; // Tank 2 can götürsün
    }
    // Babasındaki HandleTap fonksiyonunu eziyoruz (Override)
    protected override void HandleTap()
    {
        if (isVulnerable)
        {
            health--; // Canı 1 azalt

            if (health <= 0)
            {
                // can bittiyse kök enemy sınıfından die çağır.
                base.Die(); 
            }
            else
            {
                // Canı bitmediyse ölme, sadece efekt ver
                Debug.Log(" Tank Hasar Aldı ama Yıkılmadı!");
                
                // Oyuncuya "Daha bitmedi" hissi vermek için rengi turuncu 
                spriteRenderer.color = new Color(1f, 0.5f, 0f); // Turuncu
                
                // ÖNEMLİ: Saldırı süresini (MissedOpportunity) biraz uzatalım ki 2. vuruş için zaman olsun
                CancelInvoke("MissedOpportunity");
                Invoke("MissedOpportunity", 0.5f); // 0.5 saniye daha ek süre
            }
        }
    }
}