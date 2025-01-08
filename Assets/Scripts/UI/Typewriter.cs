using UnityEngine;
using TMPro;
using System.Collections;

namespace Scripts.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class Typewriter : MonoBehaviour
    {
        private TMP_Text textBox;

        private int currentCharacterID;

        private void Awake()
        {
            textBox = GetComponent<TMP_Text>();
        }

        public void SetText(string text)
        {
            textBox.text = text;
            textBox.maxVisibleCharacters = 0;
            currentCharacterID = 0;

            StartCoroutine(TypeText());
        }

        private IEnumerator TypeText()
        {
            yield return null; //Wait for a frame for the text to be processed by TMP_Text

            TMP_TextInfo textInfo = textBox.textInfo;

            while (currentCharacterID < textInfo.characterCount)
            {
                char character = textInfo.characterInfo[currentCharacterID].character;
                textBox.maxVisibleCharacters++;
                yield return new WaitForSeconds(0.02f);
                currentCharacterID++;
            }
        }
    }
}
