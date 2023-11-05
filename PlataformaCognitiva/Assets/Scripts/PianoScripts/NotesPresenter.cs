using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class NotesPresenter : MonoBehaviour
{
    public List<string> randomGeneratedNotes;

    public List<Text> Notes1Level = new List<Text>();
    public List<Text> Notes2Level = new List<Text>();
    public List<Text> Notes3Level = new List<Text>();
    public List<Text> Notes4Level = new List<Text>();
    public List<Text> Notes5Level = new List<Text>();
    public List<Text> Notes6Level = new List<Text>();

    public NotesGenerator notesGenerator;
    public KeysController kc; 

    string[] notesArray1 = new string[] { "DO 1", "RE 1", "MI 1", "FA 1", "SOL 1", "LA 1", "SI 1" };
    string[] bemolArray1 = new string[] { "DO#1", "RE#1", "FA#1", "SOL#1", "LA#1" };
    string[] notesArray2 = new string[] { "DO 2", "RE 2", "MI 2", "FA 2", "SOL 2", "LA 2", "SI 2" };
    string[] bemolArray2 = new string[] { "DO#2", "RE#2", "FA#2", "SOL#2", "LA#2" };

    string[] completeNotesArray, bemolArray, notesAndBemolArray;

    public List<string> generatedNotes = new List<string>();

    void Awake()
    {
        completeNotesArray = new string[notesArray1.Length + notesArray2.Length];
        notesArray1.CopyTo(completeNotesArray, 0);
        notesArray2.CopyTo(completeNotesArray, notesArray1.Length);

        bemolArray = new string[bemolArray1.Length + bemolArray2.Length];
        bemolArray1.CopyTo(bemolArray, 0);
        bemolArray2.CopyTo(bemolArray, bemolArray1.Length);

        notesAndBemolArray = new string[completeNotesArray.Length + bemolArray.Length];
        completeNotesArray.CopyTo(notesAndBemolArray, 0);
        bemolArray.CopyTo(notesAndBemolArray, completeNotesArray.Length);
        
        generatedNotes = notesGenerator.getLettersForPassword(completeNotesArray, 3);

        foreach (var item in Notes1Level)
        {
            item.gameObject.SetActive(true);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        kc.keysManager(generatedNotes);
        Notes1Level[0].text = generatedNotes[0];
        Notes1Level[1].text = generatedNotes[1];
        Notes1Level[2].text = generatedNotes[2];
        //Notes6Level[3].text = generatedNotes[3];
        //Notes6Level[4].text = generatedNotes[4];
        //Notes6Level[5].text = generatedNotes[5];
        //Notes6Level[6].text = generatedNotes[6];
        //Notes6Level[7].text = generatedNotes[7];
    }

    public void NotesLevel1()
    {
        generatedNotes = notesGenerator.getLettersForPassword(completeNotesArray, 3);

        foreach (var item in Notes1Level)
        {
            item.gameObject.SetActive(true);
        }

        foreach (var item in Notes2Level)
        {
            item.gameObject.SetActive(false);
        }

        kc.keysManager(generatedNotes);
        Notes1Level[0].text = generatedNotes[0];
        Notes1Level[1].text = generatedNotes[1];
        Notes1Level[2].text = generatedNotes[2];

    }

    public void NotesLevel2()
    {
        generatedNotes = notesGenerator.getLettersForPassword(notesAndBemolArray, 4);

        foreach (var item in Notes1Level)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in Notes2Level)
        {
            item.gameObject.SetActive(true);
        }

        kc.keysManager(generatedNotes);
        Notes2Level[0].text = generatedNotes[0];
        Notes2Level[1].text = generatedNotes[1];
        Notes2Level[2].text = generatedNotes[2];
        Notes2Level[3].text = generatedNotes[3];

    }

    public void NotesLevel3()
    {

        generatedNotes = notesGenerator.getLettersForPassword(notesAndBemolArray, 5);

        foreach (var item in Notes2Level)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in Notes3Level)
        {
            item.gameObject.SetActive(true);
        }

        kc.keysManager(generatedNotes);
        Notes3Level[0].text = generatedNotes[0];
        Notes3Level[1].text = generatedNotes[1];
        Notes3Level[2].text = generatedNotes[2];
        Notes3Level[3].text = generatedNotes[3];
        Notes3Level[4].text = generatedNotes[4];

    }

    public void NotesLevel4()
    {
        generatedNotes = notesGenerator.getLettersForPassword(notesAndBemolArray, 6);

        foreach (var item in Notes3Level)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in Notes4Level)
        {
            item.gameObject.SetActive(true);
        }

        kc.keysManager(generatedNotes);
        Notes4Level[0].text = generatedNotes[0];
        Notes4Level[1].text = generatedNotes[1];
        Notes4Level[2].text = generatedNotes[2];
        Notes4Level[3].text = generatedNotes[3];
        Notes4Level[4].text = generatedNotes[4];
        Notes4Level[5].text = generatedNotes[5];
    }

    public void NotesLevel5()
    {
        generatedNotes = notesGenerator.getLettersForPassword(notesAndBemolArray, 7);

        foreach (var item in Notes4Level)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in Notes5Level)
        {
            item.gameObject.SetActive(true);
        }

        kc.keysManager(generatedNotes);
        Notes5Level[0].text = generatedNotes[0];
        Notes5Level[1].text = generatedNotes[1];
        Notes5Level[2].text = generatedNotes[2];
        Notes5Level[3].text = generatedNotes[3];
        Notes5Level[4].text = generatedNotes[4];
        Notes5Level[5].text = generatedNotes[5];
        Notes5Level[6].text = generatedNotes[6];
    }

    public void NotesLevel6()
    {
        generatedNotes = notesGenerator.getLettersForPassword(notesAndBemolArray, 8);

        foreach (var item in Notes5Level)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in Notes6Level)
        {
            item.gameObject.SetActive(true);
        }

        kc.keysManager(generatedNotes);
        Notes6Level[0].text = generatedNotes[0];
        Notes6Level[1].text = generatedNotes[1];
        Notes6Level[2].text = generatedNotes[2];
        Notes6Level[3].text = generatedNotes[3];
        Notes6Level[4].text = generatedNotes[4];
        Notes6Level[5].text = generatedNotes[5];
        Notes6Level[6].text = generatedNotes[6];
        Notes6Level[7].text = generatedNotes[7];
    }

    public void resetLevel6NotesLabels()
    {
        foreach (var item in Notes6Level)
        {
            item.gameObject.SetActive(false);
        }
    }
}



