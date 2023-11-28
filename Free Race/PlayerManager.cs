using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Khiến cho ô tô tự quay lại điểm checkpoint khi rơi xuống nước
    private PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
    }

    private void OnTriggerEnter(Collider orther)
    {
        if (orther.tag == "Water")
        {
            pauseMenu.ReturnCheckpoint();
        }
    }
}
