using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class NameInput : MonoBehaviour
{
    public int[] intToLetters = new int[3];
    public TMP_Text[] texts = new TMP_Text[3];

    public void NumberIncrease(int index)
    {
        intToLetters[index]++;
        NumberToLetter(index);
    }
    
    public void NumberDecrease(int index)
    {
        intToLetters[index]--;
        if (intToLetters[index] < 0)
            intToLetters[index] = 25;
        NumberToLetter(index);
    }

    public void NumberToLetter(int index)
    {
        string letter = ((char)(intToLetters[index] % 26 + 65)).ToString();
        texts[index].SetText(letter);
    }

    public string GetName()
    {
        string letters = "";
        for (int i = 0; i < intToLetters.Length; i++)
        {
            letters += ((char)(intToLetters[i] % 26 + 65)).ToString();
        }
        return letters;
    }
}
