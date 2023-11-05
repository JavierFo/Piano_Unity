using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using UnityEngine.UI;

public class KeysController : MonoBehaviour
{
    public Button[] keys;
    public AudioClip[] Notes;
    public AudioSource pianoAudioSource;
    public Text contadorAciertos_text, contadorErrores, warning, levelNo;

    public int errores = 0;
    public int aciertos = 0, keyIndex = 0, aciertosPorNivel = 0;
    //private bool activateLvl2 = false, activatelvl3 = false, activatelvl4 = false, activatelvl5 = false;// activatelvl6 = false;

    List<string> genNotes;
    int nextKeyIndex = 0, transitionNextKeyIndex = 0;

    private bool nextExercise = false, nextExerciseCoroutine = false;

    public Dictionary<string, Button> pianoKeys = new Dictionary<string, Button>();
    public Dictionary<string, AudioClip> keysAndSounds = new Dictionary<string, AudioClip>();

    enum ExerciseState { gameStarted, rightKeyPressed, errorMade, willStartNextsequence, didStartNextSequence, didStartNextSequenceAfterError }

    private ExerciseState exState = ExerciseState.gameStarted; 

    Dictionary<string, Button> provisionalPianoKeys = new Dictionary<string, Button>();
    //public List<string> generatedNotes = new List<string>();

    public NotesPresenter genNotesP; 

    //public NotesGenerator notesGenerator;
    //// Start is called before the first frame update
    //string[] notesArray1 = new string[] { "DO 1", "RE 1", "MI 1", "FA 1", "SOL 1", "LA 1", "SI 1" };
    //List<string> generatedNotes;

    private void Awake()
    {
        pianoKeys.Add("DO 1", keys[0]);
        pianoKeys.Add("DO#1", keys[1]);
        pianoKeys.Add("RE 1", keys[2]);
        pianoKeys.Add("RE#1", keys[3]);
        pianoKeys.Add("MI 1", keys[4]);
        pianoKeys.Add("FA 1", keys[5]);
        pianoKeys.Add("FA#1", keys[6]);
        pianoKeys.Add("SOL 1", keys[7]);
        pianoKeys.Add("SOL#1", keys[8]);
        pianoKeys.Add("LA 1", keys[9]);
        pianoKeys.Add("LA#1", keys[10]);
        pianoKeys.Add("SI 1", keys[11]);
        pianoKeys.Add("DO 2", keys[12]);
        pianoKeys.Add("DO#2", keys[13]);
        pianoKeys.Add("RE 2", keys[14]);
        pianoKeys.Add("RE#2", keys[15]);
        pianoKeys.Add("MI 2", keys[16]);
        pianoKeys.Add("FA 2", keys[17]);
        pianoKeys.Add("FA#2", keys[18]);
        pianoKeys.Add("SOL 2", keys[19]);
        pianoKeys.Add("SOL#2", keys[20]);
        pianoKeys.Add("LA 2", keys[21]);
        pianoKeys.Add("LA#2", keys[22]);
        pianoKeys.Add("SI 2", keys[23]);

        keysAndSounds.Add("DO 1", Notes[0]);
        keysAndSounds.Add("DO#1", Notes[1]);
        keysAndSounds.Add("RE 1", Notes[2]);
        keysAndSounds.Add("RE#1", Notes[3]);
        keysAndSounds.Add("MI 1", Notes[4]);
        keysAndSounds.Add("FA 1", Notes[5]);
        keysAndSounds.Add("FA#1", Notes[6]);
        keysAndSounds.Add("SOL 1", Notes[7]);
        keysAndSounds.Add("SOL#1", Notes[8]);
        keysAndSounds.Add("LA 1", Notes[9]);
        keysAndSounds.Add("LA#1", Notes[10]);
        keysAndSounds.Add("SI 1", Notes[11]);
        keysAndSounds.Add("DO 2", Notes[12]);
        keysAndSounds.Add("DO#2", Notes[13]);
        keysAndSounds.Add("RE 2", Notes[14]);
        keysAndSounds.Add("RE#2", Notes[15]);
        keysAndSounds.Add("MI 2", Notes[16]);
        keysAndSounds.Add("FA 2", Notes[17]);
        keysAndSounds.Add("FA#2", Notes[18]);
        keysAndSounds.Add("SOL 2", Notes[19]);
        keysAndSounds.Add("SOL#2", Notes[20]);
        keysAndSounds.Add("LA 2", Notes[21]);
        keysAndSounds.Add("LA#2", Notes[22]);
        keysAndSounds.Add("SI 2", Notes[23]);
       

        //generatedNotes = notesGenerator.getLettersForPassword(notesArray1, 3);

    }

    void Start()
    {
        levelNo.text = "1";
        keySounds();
        //keysManager(generatedNotes);
        genNotes = genNotesP.generatedNotes;

        StartCoroutine(ShowNextsequence());

    }

    void Update()
    {
        //if (exState == ExerciseState.rightKeyPressed)
        //{
        //    if (aciertos == 3 || aciertos == 6 || aciertos == 9)
        //    {
        //        keyIndex = 0;
        //        //genNotesP.NotesLevel1();
        //        nextExercise = false;
        //        //genNotes = genNotesP.generatedNotes;
        //        nextExerciseCoroutine = true;
        //        exState = ExerciseState.willStartNextsequence;
        //        StartCoroutine(ShowNextsequence());
        //    }
        //}
    }

    void keySounds() {

        keys[0].onClick.AddListener(() => playKey(0));
        keys[1].onClick.AddListener(() => playKey(1));
        keys[2].onClick.AddListener(() => playKey(2));
        keys[3].onClick.AddListener(() => playKey(3));
        keys[4].onClick.AddListener(() => playKey(4));
        keys[5].onClick.AddListener(() => playKey(5));
        keys[6].onClick.AddListener(() => playKey(6));
        keys[7].onClick.AddListener(() => playKey(7));
        keys[8].onClick.AddListener(() => playKey(8));
        keys[9].onClick.AddListener(() => playKey(9));
        keys[10].onClick.AddListener(() => playKey(10));
        keys[11].onClick.AddListener(() => playKey(11));
        keys[12].onClick.AddListener(() => playKey(12));
        keys[13].onClick.AddListener(() => playKey(13));
        keys[14].onClick.AddListener(() => playKey(14));
        keys[15].onClick.AddListener(() => playKey(15));
        keys[16].onClick.AddListener(() => playKey(16));
        keys[17].onClick.AddListener(() => playKey(17));
        keys[18].onClick.AddListener(() => playKey(18));
        keys[19].onClick.AddListener(() => playKey(19));
        keys[20].onClick.AddListener(() => playKey(20));
        keys[21].onClick.AddListener(() => playKey(21));
        keys[22].onClick.AddListener(() => playKey(22));
        keys[23].onClick.AddListener(() => playKey(23));

    }

    void playKey(int numberOfKey)
    {
        //Debug.Log("inicial");
        pianoAudioSource.PlayOneShot(Notes[numberOfKey]);
    }

    void playKeyDictionary(AudioClip key)
    {
        //Debug.Log("asignado");
        pianoAudioSource.PlayOneShot(key);
    }

    public void keysManager(List<string> generatedNotes) {

        

        foreach (KeyValuePair<string, Button> kvp_ in pianoKeys)
        {
            pianoKeys[kvp_.Key].onClick.AddListener(() => KeysError(kvp_.Key));
        }
        
        pianoKeys[generatedNotes[keyIndex]].onClick.RemoveAllListeners();
        pianoKeys[generatedNotes[keyIndex]].onClick.AddListener(() => playKeyDictionary(keysAndSounds[generatedNotes[keyIndex]]));

        //foreach (var kvp in pianoKeys.Keys)
        //{
        //    foreach (var item in generatedNotes)
        //    {
        //        //if (pianoKeys.ContainsKey(item))
        //        //{
        //        //    pianoKeys[item].image.color = Color.blue;
        //        //    pianoKeys[item].onClick.AddListener(() => KeysSuccess(item));
        //        //}

        //        if (kvp == item)
        //        {
        //            //pianoKeys[kvp.Key].image.color = Color.yellow;
        //            pianoKeys[kvp].onClick.RemoveAllListeners();
        //            pianoKeys[kvp].onClick.AddListener(() => playKeyDictionary(keysAndSounds[kvp]));
        //        }
        //    }

        //}

        //foreach (var item in generatedNotes)
        //{
        //    pianoKeys.Add(item, provisionalPianoKeys[item]);  
        //}
        //if (aciertos == 0) {
        //    pianoKeys[generatedNotes[keyIndex]].image.color = Color.blue;
        //}
        
        pianoKeys[generatedNotes[keyIndex]].onClick.AddListener(() => keyPressed(pianoKeys[generatedNotes[keyIndex]]));

    }

    void resetSecuence() {
        foreach (KeyValuePair<string, Button> kvp_ in pianoKeys)
        {
            pianoKeys[kvp_.Key].onClick.RemoveAllListeners();
        }
        resetColors();
        keySounds();
        keyIndex = 0;
        //genNotesP.NotesLevel1();
        //genNotes = genNotesP.generatedNotes;
        foreach (var value in genNotes)
        {
            Debug.Log(value);
        }
        //keysManager(genNotes);
    }

    void resetColors() {
        foreach (KeyValuePair<string, Button> kvp_ in pianoKeys)
        {
            pianoKeys[kvp_.Key].image.color = Color.white;
        }
        pianoKeys["DO#1"].image.color = Color.black;
        pianoKeys["RE#1"].image.color = Color.black;
        pianoKeys["FA#1"].image.color = Color.black;
        pianoKeys["SOL#1"].image.color = Color.black;
        pianoKeys["LA#1"].image.color = Color.black;
        pianoKeys["DO#2"].image.color = Color.black;
        pianoKeys["RE#2"].image.color = Color.black;
        pianoKeys["FA#2"].image.color = Color.black;
        pianoKeys["SOL#2"].image.color = Color.black;
        pianoKeys["LA#2"].image.color = Color.black;
    }

    void keyPressed(Button button) {

        Debug.Log("keyPressed");
        //int provIndex = 0;
        if (button.CompareTag(genNotes[keyIndex]))
        {
            button.image.color = Color.green;
            //provIndex = keyIndex;
            //button.onClick.RemoveListener(() => keyPressed(button));
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => playKeyDictionary(keysAndSounds[button.tag]));
            button.onClick.AddListener(() => touchedKeyError(button));
            aciertos++;
            aciertosPorNivel++;
            exState = ExerciseState.rightKeyPressed;
            nextExercise = true;
            contadorAciertos_text.text = aciertos.ToString();
            keyIndex++;
            //button.onClick.AddListener(() => playKey(9));
            if (keyIndex < genNotes.Count)
            {
                pianoKeys[genNotes[keyIndex]].onClick.RemoveAllListeners();
                pianoKeys[genNotes[keyIndex]].onClick.AddListener(() => playKeyDictionary(keysAndSounds[genNotes[keyIndex]]));
                pianoKeys[genNotes[keyIndex]].onClick.AddListener(() => keyPressed(pianoKeys[genNotes[keyIndex]]));
                pianoKeys[genNotes[keyIndex]].image.color = Color.blue;
            }

            if (exState == ExerciseState.rightKeyPressed)
            {
                if (aciertosPorNivel == 3 || aciertosPorNivel == 6 || aciertosPorNivel == 9)
                {          
                    keyIndex = 0;
                    foreach (KeyValuePair<string, Button> kvp_ in pianoKeys)
                    {
                        pianoKeys[kvp_.Key].onClick.RemoveAllListeners();
                    }
                    keySounds();
                    genNotesP.NotesLevel1();
                    nextExercise = false;
                    genNotes = genNotesP.generatedNotes;                   
                    nextExerciseCoroutine = true;
                    exState = ExerciseState.willStartNextsequence;
                    StartCoroutine(ShowNextsequence());
                }
                if (aciertosPorNivel == 12 || aciertosPorNivel == 16 || aciertosPorNivel == 20)
                {
                    levelNo.text = "2";
                    keyIndex = 0;
                    foreach (KeyValuePair<string, Button> kvp_ in pianoKeys)
                    {
                        pianoKeys[kvp_.Key].onClick.RemoveAllListeners();
                    }
                    keySounds();
                    genNotesP.NotesLevel2();
                    nextExercise = false;
                    genNotes = genNotesP.generatedNotes;
                    nextExerciseCoroutine = true;
                    exState = ExerciseState.willStartNextsequence;
                    StartCoroutine(ShowNextsequence());
                }
                if (aciertosPorNivel == 24 || aciertosPorNivel == 29 || aciertosPorNivel == 34)
                {
                    levelNo.text = "3";
                    keyIndex = 0;
                    foreach (KeyValuePair<string, Button> kvp_ in pianoKeys)
                    {
                        pianoKeys[kvp_.Key].onClick.RemoveAllListeners();
                    }
                    keySounds();
                    genNotesP.NotesLevel3();
                    nextExercise = false;
                    genNotes = genNotesP.generatedNotes;
                    nextExerciseCoroutine = true;
                    exState = ExerciseState.willStartNextsequence;
                    StartCoroutine(ShowNextsequence());
                }
                if (aciertosPorNivel == 39 || aciertosPorNivel == 45 || aciertosPorNivel == 51)
                {
                    levelNo.text = "4";
                    keyIndex = 0;
                    foreach (KeyValuePair<string, Button> kvp_ in pianoKeys)
                    {
                        pianoKeys[kvp_.Key].onClick.RemoveAllListeners();
                    }
                    keySounds();
                    genNotesP.NotesLevel4();
                    nextExercise = false;
                    genNotes = genNotesP.generatedNotes;
                    nextExerciseCoroutine = true;
                    exState = ExerciseState.willStartNextsequence;
                    StartCoroutine(ShowNextsequence());
                }
                if (aciertosPorNivel == 57 || aciertosPorNivel == 64 || aciertosPorNivel == 71)
                {
                    levelNo.text = "5";
                    keyIndex = 0;
                    foreach (KeyValuePair<string, Button> kvp_ in pianoKeys)
                    {
                        pianoKeys[kvp_.Key].onClick.RemoveAllListeners();
                    }
                    keySounds();
                    genNotesP.NotesLevel5();
                    nextExercise = false;
                    genNotes = genNotesP.generatedNotes;
                    nextExerciseCoroutine = true;
                    exState = ExerciseState.willStartNextsequence;
                    StartCoroutine(ShowNextsequence());
                }
                if (aciertosPorNivel == 78 || aciertosPorNivel == 86 || aciertosPorNivel == 94)
                {
                    levelNo.text = "FIN";
                    keyIndex = 0;
                    foreach (KeyValuePair<string, Button> kvp_ in pianoKeys)
                    {
                        pianoKeys[kvp_.Key].onClick.RemoveAllListeners();
                    }
                    keySounds();
                    genNotesP.NotesLevel6();
                    nextExercise = false;
                    genNotes = genNotesP.generatedNotes;
                    nextExerciseCoroutine = true;
                    exState = ExerciseState.willStartNextsequence;
                    StartCoroutine(ShowNextsequence());
                }

            }
            //if (aciertos == 3)
            //{
            //    keyIndex = 0;
            //    genNotesP.NotesLevel2();
            //    genNotes = genNotesP.generatedNotes;
            //    ShowNextsequence();
            //}
        }
        Debug.Log(exState);
        //else if (button.CompareTag(genNotes[provIndex]))
        //{
        //    Debug.Log(genNotes[provIndex]);
        //    button.onClick.AddListener(() => playKeyDictionary(keysAndSounds[genNotes[provIndex]]));
        //    button.onClick.AddListener(() => KeysError(genNotes[provIndex]));
        //}


        //if (button.name == genNotes[0]) {

        //    Debug.Log("Button NAME YES");

        //}

    }

    void KeysError(string key)
    {
        Debug.Log("KeysError");
        nextExercise = false;
        nextExerciseCoroutine = false;
        exState = ExerciseState.errorMade;
        errores++;
        resetSecuence();
        pianoKeys[key].image.color = Color.red;

        //StartCoroutine(incorrectKeyLabelTransition());
        if (aciertosPorNivel < 12)
        {
            levelNo.text = "1";
            aciertosPorNivel = 0;
            genNotesP.NotesLevel1();
        }
        else if (aciertosPorNivel >= 12 && aciertosPorNivel < 24)
        {
            levelNo.text = "2";
            aciertosPorNivel = 12;
            genNotesP.NotesLevel2();
        }
        else if (aciertosPorNivel >= 24 && aciertosPorNivel < 39)
        {
            levelNo.text = "3";
            aciertosPorNivel = 24;
            genNotesP.NotesLevel3();
        }
        else if (aciertosPorNivel >= 39 && aciertosPorNivel < 57)
        {
            levelNo.text = "4";
            aciertosPorNivel = 39;
            genNotesP.NotesLevel4();
        }
        else if (aciertosPorNivel >= 57 && aciertosPorNivel < 78)
        {
            levelNo.text = "5";
            aciertosPorNivel = 57;
            genNotesP.NotesLevel5();
        }
        else if (aciertosPorNivel >= 78 && aciertosPorNivel < 102)
        {
            levelNo.text = "FIN";
            aciertosPorNivel = 78;
            genNotesP.NotesLevel6();
        }
        else if (aciertosPorNivel == 102)
        {
            levelNo.text = "1";
            aciertosPorNivel = 0;
            genNotesP.resetLevel6NotesLabels();
            genNotesP.NotesLevel1();
        }

        nextExercise = false;
        genNotes = genNotesP.generatedNotes;
        StartCoroutine(ShowNextsequence());

        //contadorErrores.text = errores.ToString();
        //aciertos = 0;
        //if (errores >= 0)
        //{
        //    Debug.Log("ERROR");
        //    nextKeyIndex = 0;
        //    keyIndex = 0;
        //    genNotes = genNotesP.generatedNotes;
        //    StartCoroutine(ShowNextsequence());
        //}
        pianoKeys[key].onClick.RemoveListener(() => KeysError(key));
    }

    void touchedKeyError(Button alreadyPressedKey)
    {
        Debug.Log("touchedKeyError");
        Debug.Log(exState);
        nextExercise = false;
        nextExerciseCoroutine = false;
        exState = ExerciseState.errorMade;
        errores++;
        resetSecuence();
        alreadyPressedKey.image.color = Color.red;

        //StartCoroutine(incorrectKeyLabelTransition());
        if (aciertosPorNivel <= 12)
        {
            levelNo.text = "1";
            aciertosPorNivel = 0;
            genNotesP.NotesLevel1();
        }
        else if (aciertosPorNivel > 12 && aciertosPorNivel <= 24)
        {
            levelNo.text = "2";
            aciertosPorNivel = 12;
            genNotesP.NotesLevel2();
        }
        else if (aciertosPorNivel > 24 && aciertosPorNivel <= 39)
        {
            levelNo.text = "3";
            aciertosPorNivel = 24;
            genNotesP.NotesLevel3();
        }
        else if (aciertosPorNivel > 39 && aciertosPorNivel <= 57)
        {
            levelNo.text = "4";
            aciertosPorNivel = 39;
            genNotesP.NotesLevel4();
        }
        else if (aciertosPorNivel > 57 && aciertosPorNivel <= 78)
        {
            levelNo.text = "5";
            aciertosPorNivel = 57;
            genNotesP.NotesLevel5();
        }
        else if (aciertosPorNivel > 78 && aciertosPorNivel < 102)
        {
            levelNo.text = "FIN";
            aciertosPorNivel = 78;
            genNotesP.NotesLevel6();
        }
        else if (aciertosPorNivel == 102)
        {
            levelNo.text = "1";
            aciertosPorNivel = 0;
            genNotesP.NotesLevel1();
        }

        nextExercise = false;
        genNotes = genNotesP.generatedNotes;
        StartCoroutine(ShowNextsequence());
        
        //contadorErrores.text = errores.ToString();
        //aciertos = 0;
        //alreadyPressedKey.onClick.AddListener(() => playKeyDictionary(keysAndSounds[genNotes[keyIndex]]));
        //if (errores >= 0)
        //{
        //    Debug.Log("ERROR 2");
        //    StartCoroutine(ShowNextsequence());
        //}
        alreadyPressedKey.onClick.RemoveListener(() => touchedKeyError(alreadyPressedKey));
    }

    //IEnumerator incorrectKeyLabelTransition() {
    //    warning.text = "INCORRECTO";
    //    warning.gameObject.SetActive(true);
    //    warning.color = Color.red;
    //    yield return new WaitForSeconds(1f);
    //    warning.gameObject.SetActive(false);
    //    warning.color = Color.green;
    //}

    IEnumerator ShowNextsequence() //List<string> generatedNotes
    {
        Debug.Log(exState);
        if (nextKeyIndex == 0) {
            //NOTA: SUSTITUIR BANDERA ERROR POR ENUM ESTADOS: ERROR, NEXTEXERCISE. 
            if (exState == ExerciseState.errorMade)
            {
                exState = ExerciseState.didStartNextSequenceAfterError;
                foreach (var key in keys)
                {
                    key.enabled = false;
                }
                warning.text = "INCORRECTO";
                warning.gameObject.SetActive(true);
                warning.color = Color.red;
                
                yield return new WaitForSeconds(1.5f);

                resetColors();

                //genNotesP.NotesLevel1();
                //genNotes = genNotesP.generatedNotes;
                warning.gameObject.SetActive(false);
                warning.color = Color.green;
            }

            //if (aciertos == 3) {
            //    foreach (var key in keys)
            //    {
            //        key.enabled = false;
            //    }
            //    warning.text = "SIGUIENTE EJERCICIO";
            //    warning.gameObject.SetActive(true);
            //    warning.color = Color.blue;

            //    yield return new WaitForSeconds(1.5f);

            //    resetColors();
            //    genNotesP.NotesLevel1();
            //    genNotes = genNotesP.generatedNotes;
            //    warning.gameObject.SetActive(false);
            //    warning.color = Color.green;
            //}

            foreach (var key in keys)
            {
                key.enabled = false;
            }
            warning.text = "ATENCION!";
            warning.gameObject.SetActive(true);
        }

        if (exState == ExerciseState.willStartNextsequence)
        {
            exState = ExerciseState.didStartNextSequence;
            nextExerciseCoroutine = false;
            warning.text = "BIEN HECHO!";
            warning.gameObject.SetActive(true);
            foreach (var key in keys)
            {
                key.enabled = false;
            }
            yield return new WaitForSeconds(1.5f);
            resetColors();

            //if (aciertos == 3 || aciertos == 6 || aciertos == 9)
            //{
            //    genNotesP.NotesLevel1();
            //    genNotes = genNotesP.generatedNotes;
            //}

            warning.text = "ATENCION!";
            warning.gameObject.SetActive(true);
        }

        pianoKeys[genNotes[transitionNextKeyIndex]].image.color = Color.green;
        pianoKeys[genNotes[nextKeyIndex]].image.color = Color.blue;
        pianoAudioSource.PlayOneShot(keysAndSounds[genNotes[nextKeyIndex]]);
        transitionNextKeyIndex = nextKeyIndex;
        nextKeyIndex++;
        yield return new WaitForSeconds(1f);

        if (nextKeyIndex < genNotes.Count)
        {
            StartCoroutine(ShowNextsequence());
        } else
        {
            //warning.gameObject.SetActive(false);
            pianoKeys[genNotes[transitionNextKeyIndex]].image.color = Color.green;
            transitionNextKeyIndex = 0;
            nextKeyIndex = 0;
            yield return new WaitForSeconds(1f);
            foreach (var genNote in genNotes)
            {
                //pianoKeys[genNote].image.color = Color.white;
                resetColors();
            }
            pianoKeys[genNotes[nextKeyIndex]].image.color = Color.blue;
            contadorAciertos_text.text = aciertos.ToString();
            warning.text = "COMIENZE";
            yield return new WaitForSeconds(1f);
            warning.gameObject.SetActive(false);
            foreach (var key in keys)
            {
                key.enabled = true;
            }
        }
    }

    //public void keysManager(List<string> generatedNotes)
    //{
    //    bool inicio = true;
    //    if (inicio == true) {
    //        pianoKeys[generatedNotes[0]].image.color = Color.blue;
    //    }

    //    foreach (var item in generatedNotes) {

    //        if (item == pianoKeys[] 
    //    }

    //    for (int i = 0; i < generatedNotes.Count; i++)
    //    {
    //        Dictionary<string, Button>.KeyCollection keyColl = pianoKeys.Keys;

    //        foreach (string s in keyColl)
    //        {
    //            Console.WriteLine("Key = {0}", s);
    //        }

    //        if (generatedNotes[i] == keyColl)
    //    }

    //    //Dictionary<string, Button>.KeyCollection keyColl = pianoKeys.Keys;

    //    foreach (string s in keyColl)
    //    {
    //        Console.WriteLine("Key = {0}", s);
    //    }


    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
}






//void KeysSuccess(string item)
//{

//    aciertos++;
//    contadorAciertos_text.text = aciertos.ToString();
//    pianoKeys[item].image.color = Color.green;
//    keyIndex++;
//    pianoKeys[genNotes[keyIndex]].image.color = Color.blue;
//    pianoKeys[genNotes[keyIndex]].onClick.AddListener(() => KeysSuccess(genNotes[keyIndex]));
//    pianoKeys[item].onClick.RemoveListener(() => KeysSuccess(item));

//    if (aciertos == 2 && activateLvl2 == false)
//    {
//        activateLvl2 = true;
//        keyIndex = 0;
//        genNotesP.NotesLevel2();
//        genNotes = genNotesP.generatedNotes;
//        keysManager(genNotes);
//    }
//    else if (aciertos == 6 && activatelvl3 == false)
//    {
//        activatelvl3 = true;
//        keyIndex = 0;
//        genNotesP.NotesLevel3();
//        genNotes = genNotesP.generatedNotes;
//        keysManager(genNotes);
//    }
//    else if (aciertos > 11 && activatelvl4 == false)
//    {
//        activatelvl4 = true;
//        keyIndex = 0;
//        genNotesP.NotesLevel4();
//        genNotes = genNotesP.generatedNotes;
//        keysManager(genNotes);
//    }
//    else if (aciertos > 16 && activatelvl5 == false)
//    {
//        activatelvl5 = true;
//        keyIndex = 0;
//        genNotesP.NotesLevel5();
//        genNotes = genNotesP.generatedNotes;
//        keysManager(genNotes);
//    }

//}