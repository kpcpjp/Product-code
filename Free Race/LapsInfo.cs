using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapsInfo : MonoBehaviour
{
    public TextMeshProUGUI lapsText;

    // Update is called once per frame
    void Update()
    {
        // Hiển thị số vòng đua còn lại hiện tại
        lapsText.text = "ラップ:\n" + CheckPointSingle.count + " / " + DifficultyManager.numberRound;
    }
}
