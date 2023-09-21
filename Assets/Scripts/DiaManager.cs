using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaManager : MonoBehaviour
{

    public TextMeshProUGUI text;
    private Queue<string> sentences;
    public static DiaManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNxtSentence();
        }
    }
    internal void StartDia(Dialouge dia)
    {
        Time.timeScale = 0;
        GameManager.Instance.player.canMove = false;

        sentences.Clear();

        foreach (var sentence in sentences) 
        {
            sentences.Enqueue(sentence);
        }

        DisplayNxtSentence();
    }

    public void DisplayNxtSentence()
    {
        if(sentences.Count == 0)
        {
            EndDia();
            return;
        }

        var sentence = sentences.Dequeue();
        text.text = sentence;
    }

    public void EndDia()
    {
         
    }
}
