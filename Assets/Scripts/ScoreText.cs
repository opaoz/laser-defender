using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] string prefix = "";

    TextMeshProUGUI text;
    Status status;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        status = FindObjectOfType<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = prefix + status.GetScore().ToString();
    }
}
