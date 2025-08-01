using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject[] dialogues;
    public GameObject dialogueUI;
    private int currentDialogue = 0;
    private float skippingDelay = 0.5f;
    private float timer = 0f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            timer = Time.time + skippingDelay;
            dialogueUI.SetActive(true);
        }
    }

    void Update()
    {
        if (dialogueUI.activeSelf && Time.time >= timer &&
            Input.GetButtonDown("SkipDialogue") && currentDialogue < dialogues.Length)
        {
            timer = Time.time + skippingDelay;

            if (dialogues[currentDialogue].GetComponent<TextWriter>().isTyping)
                FastTrack();
            else
                ChangeDialogue();
        }
    }

    public void FastTrack()
    {
        dialogues[currentDialogue].GetComponent<TextWriter>().FastTrackDialogue();
    }
    public void ChangeDialogue()
    {
        dialogues[currentDialogue].SetActive(false);
        currentDialogue++;

        if (currentDialogue < dialogues.Length)
            dialogues[currentDialogue].SetActive(true);
        else
            dialogueUI.SetActive(false);
    }
}
