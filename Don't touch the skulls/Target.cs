using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Chương trình quản lý các đối tượng được sinh ra
public class Target : MonoBehaviour
{
    //Rigidbody của đối tượng
    private Rigidbody targetRb;
    // Biến để lấy GameManager.cs
    private GameManager gameManager;
    //Biến hệ thống hạt
    public ParticleSystem explosionParticle;
    //Giới hạn tốc độ của vật thể
    private float minSpeed = 12;
    private float maxSpeed = 16;
    //Giới hạn mô-men xoắn trên một vật thể
    private float maxTorque = 10;
    //Giới hạn phạm vi sinh sản
    private float xRange = 4;
    private float ySpwanpos = -2;
    //Đặt điểm cho đối tượng
    public int pointValue;
    // Biến để lấy CursorTrail.cs
    private CursorTrail cursorTrail;

    // Start is called before the first frame update
    void Start()
    {
        // Lấy GameManager.cs
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // Lấy CursorTrail.cs
        cursorTrail = GameObject.Find("Main Camera").GetComponent<CursorTrail>();
        // Lấy Rigidbody của đổi tượng
        targetRb = GetComponent<Rigidbody>();
        // Quản lý chuyển động của đối tượng
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        //Quản lý mô-men xoắn đối tượng
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        //Điểm bắt đầu cho các đối tượng sinh sản
        transform.position = RandomSpawnPos();
    }

    //Các Phương thức quản lý chuyển động của đối tượng
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    //Phương thức quản lý mô-men xoắn của đối tượng
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    //Phương thức điểm bắt đầu sinh sản đối tượng
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpwanpos);
    }

    //Phương thức click và kéo chuột
    public void OnMouseOver()
    {
        // Nếu isGameActive trong gameManager.cs là true và chuột trái được nhấn
        if (gameManager.isGameActive && Input.GetMouseButton(0))  
        {
            // Phá hủy vật thể
            Destroy(gameObject);
            //Thêm hệ thống hạt vào một đối tượng
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            //Cập nhật điểm
            gameManager.UpdateScore(pointValue);
            //Cập nhật tốc độ sinh sản ở chế độ vô tận
            if ((gameManager.score > 0) &&
                                       (gameManager.isEndlessModeActive) && (gameManager.spawnRate > 0.3f))
            {
                gameManager.spawnRate -= 0.01f;
            }
        }
    }

    //Phương thức tự động hủy đối tượng và đặt trạng thái trò chơi
    //Tương tác của vật thể và cảm biến
    private void OnTriggerEnter(Collider orther)
    {
        // Phá hủy vật thể
        Destroy(gameObject);
        // Khi vật thể không phải là đối tượng xấu và số mạng lớn hơn 0
        if ((!gameObject.CompareTag("Bad")) && (gameManager.lives > 0))
        {
            // Cập nhật số mạng
            gameManager.UpdateLives(1);
            //Thực hiện trò chơi kết thúc số mạng về 0
            if (gameManager.lives == 0)
            {
                if (gameManager.isEndlessModeActive)
                {
                    gameManager.EndlessModeGameOver();
                } else
                gameManager.GameOver();
            }
        }
    }
}
