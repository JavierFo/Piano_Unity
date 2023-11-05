using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class NotesGenerator : MonoBehaviour
{

    AesExample cryptoGenerator = new AesExample();
    List<string> listOfNotes = new List<string>();
    string[] notesArray = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

// Start is called before the first frame update
void Start()
    {

        //listOfNotes = getLettersForPassword(notesArray, 4);
        
    }

    // Update is called once per frame
    void Update()
    {
        //string result = "List contents: ";
        //foreach (var item in listOfNotes)
        //{
        //    result += item.ToString() + ", ";
        //}
        //Debug.Log(result);
    }

    readonly Aes myAes = Aes.Create();

    string Bit64counter(UInt64 value) {

        string counter = value.ToString();
        while (counter.Length < 64) {
            counter = "0" + counter;
        }

        return counter;
        //return Data(counter.utf8)
    }

    UInt64 getNumberOfCipheredCounter(UInt64 counter)
    {
        List<string> arrayCypher = new List<string>();

        string counterThatWillBeCiphered = Bit64counter(counter);

        byte[] encrypted = cryptoGenerator.EncryptStringToBytes_Aes(counterThatWillBeCiphered, myAes.Key, myAes.IV);

        foreach (UInt64 number in encrypted)
        {
            if (number < 64) { arrayCypher.Add("0"); } else { arrayCypher.Add("1"); }
        }

        string concatenated = String.Join("", arrayCypher);
        UInt64 concatenatedToInt = (UInt64)ConvertClass.Convert(concatenated);

        return concatenatedToInt;
    }

    public List<string> getLettersForPassword(string[] charactersSet, int passwordlenght)
    {
        List<string> password = new List<string>();
        var rand = new System.Random();
        int counter = rand.Next(0, 100), characterToAssign = 0;
        string character = "", characterToCompare = "";

        do {

            int cipheredCounter = (int)getNumberOfCipheredCounter((UInt64)counter);
            counter = rand.Next(0, 100);
            characterToAssign = cipheredCounter % charactersSet.Length;
            if (characterToAssign <= charactersSet.Length && characterToAssign >= 0)
            {
                character = charactersSet[characterToAssign];

                if (character != characterToCompare)
                {
                    password.Add(character);
                }

                characterToCompare = character;
            }
            

        } while (password.Contains("") == false && password.Count < passwordlenght);

        //for (int i = 0; i < passwordlenght; i++)
        //{
        //    int cipheredCounter = (int)getNumberOfCipheredCounter((UInt64)counter);
        //    counter = rand.Next(0, 100);
        //    characterToAssign = cipheredCounter % charactersSet.Length;
        //    if (characterToAssign <= charactersSet.Length && characterToAssign >= 0)
        //    {
        //        character = charactersSet[characterToAssign];
        //    }
        //    password.Add(character);
        //}

        return password;
    }


}

class AesExample
{
    public byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new System.ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new System.ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new System.ArgumentNullException("IV");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new System.ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new System.ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new System.ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}

public static class ConvertClass
{
    public static int Convert(string str1)
    {
        if (str1 == "")
            throw new Exception("Invalid input");
        int val = 0, res = 0;
        for (int i = 0; i < str1.Length; i++)
        {
            try
            {
                val = Int32.Parse(str1[i].ToString());
                if (val == 1)
                    res += (int)Math.Pow(2, str1.Length - 1 - i);
                else if (val > 1)
                    throw new Exception("Invalid!");
            }
            catch
            {
                throw new Exception("Invalid!");
            }
        }
        return res;
    }
}