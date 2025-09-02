using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace oxi
{
    public class DialogueSystem : MonoBehaviour
    {
        public static DialogueSystem Instance { get; set; }
        public GameObject dialoguePanel;
        [HideInInspector] public List<string> dialogueLines = new List<string>();
        private NPC npc;

        Button continueButton;
        TextMeshProUGUI dialogueText, nameText;
        int dialogueIndex;

        private void Awake()
        {
            continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
            dialogueText = dialoguePanel.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<TextMeshProUGUI>();
            continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
            dialoguePanel.SetActive(false);

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void AddNewDialogue(string[] lines, NPC npc)
        {
            dialogueIndex = 0;
            dialogueLines = new List<string>(lines.Length);
            dialogueLines.AddRange(lines);
            this.npc = npc;

            CreateDialogue();
        }

        public void CreateDialogue()
        {
            dialogueText.text = dialogueLines[dialogueIndex];
            nameText.text = npc.npcName;
            dialoguePanel.SetActive(true);
        }

        public void ContinueDialogue()
        {
            if (dialogueIndex < dialogueLines.Count - 1)
            {
                dialogueIndex++;
                dialogueText.text = dialogueLines[dialogueIndex];
            }
            else
            {
                dialoguePanel.SetActive(false);
            }
        }
    }
}
