using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialagoueSystem
{
    public class DialogueLine : Dialogue
    {

        [SerializeField] private string input;
        private Text textHolder;
        ///[SerializeField] private Color textColor;
        ///[SerializeField] private Font textFont;
        [SerializeField] private float delay;
        [SerializeField] private float delayBetweenLines;

        [SerializeField] private Sprite characterImage;
        [SerializeField] private Image imageHolder;

        private IEnumerator lineAppear;


        private void Awake()
        {
            imageHolder.sprite = characterImage;
            imageHolder.preserveAspect = true;

        }

        private void OnEnable()
        {
            ResetLine();
            lineAppear = WriteText(input, textHolder, delay, delayBetweenLines);
            StartCoroutine(lineAppear);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) 
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }
                else
                    finished = true;
            }  
        }

        private void ResetLine() 
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";
            finished = false;
        }

    }
}