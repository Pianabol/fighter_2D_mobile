using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Ayarlar")]
    public GameObject groundPrefab; // Zemin kalıbımız
    public Transform player;        // Oyuncumuz nerede?
    
    public int initialSegments = 5; // Başlangıçta kaç parça olsun?
    public float groundWidth = 10f; // Bir zemin parçasının genişliği (Unity birim cinsinden)
    
    private List<GameObject> _segments = new List<GameObject>(); // Sahnedeki zeminlerin listesi
    private float _spawnX = 0f; // Sıradaki parçanın konulacağı X koordinatı

    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        // Oyun başlarken ilk zemini ve devamını döşe
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnSegment();
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Oyuncu, üretilen son parçaya yaklaştı mı? (Sonsuz döngü kontrolü)
        // Eğer oyuncunun konumu, son parçanın biraz gerisindeyse yeni parça üret.
        if (player.position.x > _spawnX - (initialSegments * groundWidth / 1.5f))
        {
            SpawnSegment();
            DeleteOldSegment();
        }
    }

    private void SpawnSegment()
    {
        // Yeni bir zemin yarat
        GameObject newSeg = Instantiate(groundPrefab);
        
        // Konumunu ayarla (X sürekli artıyor, Y ve Z sabit)
        // Şu anki zemin Y: -3.6 falan olabilir, o yüzden prefab'ın kendi Y'sini koruyacağız.
        Vector3 newPos = new Vector3(_spawnX, -4.0f, 0); // NOT: -3.5f senin zemin yüksekliğin olmalı
        newSeg.transform.position = newPos;

        // Listeye ekle
        _segments.Add(newSeg);

        // Bir sonraki parçanın konumu için X'i kaydır
        _spawnX += groundWidth;
    }

    private void DeleteOldSegment()
    {
        // En arkada kalan parçayı yok et (Performans için)
        GameObject oldSeg = _segments[0];
        _segments.RemoveAt(0);
        Destroy(oldSeg);
    }
}