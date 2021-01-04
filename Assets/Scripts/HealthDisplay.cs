using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite heartSprite;
    [SerializeField] int oneHeartValue = 8;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        var health = player.GetHealth();
        var count = health / oneHeartValue;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < count;
        }
    }
}
