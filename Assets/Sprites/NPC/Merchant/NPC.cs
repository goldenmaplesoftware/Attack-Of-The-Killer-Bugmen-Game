using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]private GameObject dialogue;

    public void ActivateDialogue() 
    {
        dialogue.SetActive(true);
    }

    public bool dialogueActive()
    {
        return dialogue.activeInHierarchy;
    }

}
