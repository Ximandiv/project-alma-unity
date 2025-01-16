using UnityEngine;
using TMPro;
using System.Collections;

namespace Scripts.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class Typewriter : MonoBehaviour
    {
        [SerializeField] private float textDelay = 0.02f;
        private TMP_Text textBox;

        private int currentCharacterIndex;

        private void Awake()
        {
            textBox = GetComponent<TMP_Text>();
        }

        public void SetText(string text)
        {
            textBox.text = text;
            textBox.maxVisibleCharacters = 0;
            currentCharacterIndex = 0;

            StartCoroutine(TypeText());
        }

        private IEnumerator TypeText()
        {
            yield return null; //Wait for a frame for the text to be processed by TMP_Text

            TMP_TextInfo textInfo = textBox.textInfo;

            while (currentCharacterIndex < textInfo.characterCount)
            {
                textBox.maxVisibleCharacters++;
                yield return new WaitForSeconds(textDelay);
                currentCharacterIndex++;
            }
        }
    }
}
