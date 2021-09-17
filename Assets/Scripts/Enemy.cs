using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _moveSpeed = 1f;

    private int _currentHealth;
    
    public Vector3 TargetPosition { get; private set; }
    public int CurrentPathIndex { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void MoveToTarget ()
    {
        transform.position = Vector3.MoveTowards (transform.position, TargetPosition, _moveSpeed * Time.deltaTime);
    }

    public void SetTargetPosition (Vector3 targetPosition)
    {
        TargetPosition = targetPosition;

        // Mengubah rotasi dari enemy
        Vector3 distance = TargetPosition - transform.position;

        if (Mathf.Abs (distance.y) > Mathf.Abs (distance.x))
        {
            // Menghadap atas
            if (distance.y > 0)
            {
                transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 90f));
            }

            // Menghadap bawah
            else
            {
                transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, -90f));
            }
        }

        else
        {
            // Menghadap kanan (default)
            if (distance.x > 0)
            {
                transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
            }

            // Menghadap kiri
            else
            {
                transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 180f));
            }
        }
    }

    // Menandai indeks terakhir pada path
    public void SetCurrentPathIndex (int currentIndex)
    {
        CurrentPathIndex = currentIndex;
    }
    
    // Fungsi ini terpanggil sekali setiap kali menghidupkan game object yang memiliki script ini
    private void OnEnable ()
    {
        _currentHealth = _maxHealth;
    }
    
    public void ReduceEnemyHealth (int damage)
    {
        _currentHealth -= damage;
         AudioPlayer.Instance.PlaySFX ("hit-enemy");
        if (_currentHealth <= 0)
        {
			//Kode Tambahan
			int totalEnemy = PlayerPrefs.GetInt("totalEnemy");
            totalEnemy--;
            Debug.Log(totalEnemy);
            PlayerPrefs.SetInt("totalEnemy", totalEnemy);
            gameObject.SetActive (false);
            AudioPlayer.Instance.PlaySFX ("enemy-die");
        }
    }
}
