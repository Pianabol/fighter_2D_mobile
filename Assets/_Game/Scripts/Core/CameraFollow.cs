using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef (Player)
    public Vector3 offset = new Vector3(2, 0, -10); // Kamera ile oyuncu arasındaki mesafe

    void LateUpdate()
    {
        if (target != null)
        {
            // Kameranın yeni pozisyonu: Oyuncunun pozisyonu + aradaki mesafe
            // Sadece X (yatay) ekseninde takip etsin, zıplayınca (Y) kamera oynamasın diyorsan:
            Vector3 newPosition = new Vector3(target.position.x + offset.x, offset.y, offset.z);
            
            // Eğer zıplayınca da takip etsin istiyorsan üstteki satırı sil, alttakini aç:
            // Vector3 newPosition = target.position + offset;

            transform.position = newPosition;
        }
    }
}