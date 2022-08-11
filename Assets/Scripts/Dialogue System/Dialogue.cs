using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialagoueSystem
{
public class Dialogue : MonoBehaviour
{
        public bool finished { get; protected set; }
    protected IEnumerator WriteText(string input, Text textHolder,float delay,float delayBetweenLines)
    {
            
            for (int i = 0; i < input.Length; i++) 
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitUntil(() => Input.GetKey(KeyCode.KeypadEnter)); ///Change mapping later
            finished = true;
    }
}
}