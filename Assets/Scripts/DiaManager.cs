using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaManager : MonoBehaviour
{

    public TextMeshProUGUI text;
    private Queue<string> sentences;
    public static DiaManager Instance;

    public Animator animator;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            DisplayNxtSentence();
        }
    }
    public void StartDia(Dialouge dia)
    {
        animator.SetBool("IsOpen", true);
        //Time.timeScale = 0;
        GameManager.Instance.player.canMove = false;

        sentences.Clear();

        foreach (var sentence in dia.sentences) 
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
        StopAllCoroutines();
        StartCoroutine(TypeSent(sentence));
    }

    IEnumerator TypeSent (string sentence)
    {
        text.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
    }

    public void EndDia()
    {
        animator.SetBool("IsOpen", false);

        GameManager.Instance.player.canMove = true;

    }
}
