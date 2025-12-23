using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Singleton: Oyundaki tek Input yöneticisi bu olacak
    public static InputManager Instance { get; private set; }

    // Hareketleri diğer scriptlere haber verecek(Events)
    public delegate void TapAction();
    public event TapAction OnTap;

    public delegate void HoldAction(bool isHolding);
    public event HoldAction OnHold;

    public delegate void MoveInput(Vector2 position);
    public event MoveInput OnMove;

    private GameControls _gameControls; 

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

        // Kontrolcü sınıfını başlat
        _gameControls = new GameControls();

        // 1. DOKUNMA (TAP) ALGILAMA
        
        _gameControls.Player.Tap.performed += context => 
        {
            Debug.Log("Input: TAP Algılandı!");  
            OnTap?.Invoke(); 
        };

        // 2. BASILI TUTMA (HOLD) ALGILAMA
        
        _gameControls.Player.TouchPress.started += context => 
        {
            Debug.Log("Input: HOLD Başladı");
            OnHold?.Invoke(true); 
        };
        
        _gameControls.Player.TouchPress.canceled += context => 
        {
            Debug.Log("Input: HOLD Bitti");
            OnHold?.Invoke(false); 
        };

        // 3. POZİSYON TAKİBİ
        // Parmağın her hareketinde yeni pozisyonu alacağız
        _gameControls.Player.TouchPosition.performed += context =>
        {
            Vector2 touchPos = context.ReadValue<Vector2>();
            OnMove?.Invoke(touchPos);
        };
    }
 
    private void OnEnable()
    {
        _gameControls.Enable();
    }

     
    private void OnDisable()
    {
        _gameControls.Disable();
    }
}