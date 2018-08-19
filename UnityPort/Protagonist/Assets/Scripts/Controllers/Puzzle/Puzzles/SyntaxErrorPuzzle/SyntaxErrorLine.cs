using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SyntaxErrorLine : MonoBehaviour
{
    public GameObject letter;
    List<SyntaxErrorLetter> letters = new List<SyntaxErrorLetter>();

    int letterIndex = 0;
    // what/where to create
    string line;
    Vector3 position;
    Vector2 size;
    // timing on when to create these letters
    float timerSeconds = 0f;
    float duration;
    // which letters end the puzzle
    List<int> correct;
    public void Initialize(string line, Vector3 position, Vector2 size, float initialDelay, float letterDelay, List<int> correct)
    {
        this.line = line.ToUpper();
        this.position = position;
        this.size = size;
        duration = letterDelay;
        timerSeconds = duration - initialDelay;
        this.correct = correct;
    }

    void Update()
    {
        timerSeconds += GameTime.deltaTime;
        if (timerSeconds >= duration && letterIndex < line.Length)
        {
            timerSeconds = 0f;
            // if letter is valid, create it
            if ('A' <= line[letterIndex] && line[letterIndex] <= 'Z')
            {
                SyntaxErrorLetter letterBehavior = Instantiate(letter).GetComponent<SyntaxErrorLetter>();
                letterBehavior.Initialize(line[letterIndex].ToString(),
                    position + new Vector3(letterIndex * letterBehavior.GetSize().x * size.x, 0f), size, correct.Contains(letterIndex));
                letters.Add(letterBehavior);
            }
            letterIndex++;
        }
    }

    public List<SyntaxErrorLetter> GetLetters()
    {
        return letters;
    }
}
