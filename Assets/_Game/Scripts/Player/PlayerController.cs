using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Ayarlar")]
    public float moveSpeed = 5f; 
    public float jumpForce = 10f; 

    private Rigidbody2D _rb;
    private bool _isHolding = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // GÜNCELLEME: Bağlantıyı 'Start' içinde yapıyoruz.
    // Start, tüm objeler 'Awake' olduktan sonra çalışır, yani InputManager kesin hazırdır.
    private void Start()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnHold += HandleHold;
            InputManager.Instance.OnTap += HandleTap;
            Debug.Log("Player: InputManager ile bağlantı kuruldu! ");
        }
        else
        {
            Debug.LogError("Player: InputManager BULUNAMADI! ");
        }
    }

    // Obje yok olurken abonelikten çıkıyoruz (Hata almamak için)
    private void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnHold -= HandleHold;
            InputManager.Instance.OnTap -= HandleTap;
        }
    }

    // Hareket Kodları 

    private void HandleHold(bool status)
    {
        _isHolding = status;
    }

    private void HandleTap()
    {
        Debug.Log("Player: Zıplıyor!");
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // TEST İÇİN GEÇİCİ  
        // Zıplayınca zaman yavaşlasın
        
    }

    // Zamanı düzeltmek için yardımcı fonksiyon
    private void ResetTime()
    {
        TimeManager.Instance.StopSlowMotion();
    }
    
    private void FixedUpdate()
    {
        if (_isHolding)
        {
            // Sağa git
            _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);
        }
        else
        {
            //(X hızını sıfırla, Y hızı kalsın)
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
    }
}