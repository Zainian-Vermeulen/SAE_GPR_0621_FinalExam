using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can encode and decode messages by applying a simple Ceasar Cipher.
/// https://en.wikipedia.org/wiki/Caesar_cipher
/// 
/// Specifically:
/// spaces get encoded to the value 100.
/// small letters get encoded to the represeting ceaser cypher shifted letter index (0-25)
/// any other character get encoded to 255 as error characters. 
/// Capital letters are not allowed.
/// </summary>

public static class MessageTranslator
{
    private const int CAESAR_SHIFT = 13;
    public static byte[] Encode(string message)
    {
        List<byte> data = new List<byte>();
        for (int i = 0; i < message.Length; i++)
        {
            char c = message[i];
            int val = (int)c;

            if (c == ' ')
            {
                data.Add(100);
            }
            else if (val >= 'a' && val <= 'z')
            {
                int letterIndex = val - 'a';
                val = (letterIndex + CAESAR_SHIFT) % 26;

                data.Add((byte)val);
            }
            else
            {
                data.Add(255);
            }
        }
        return data.ToArray();
    }

    public static string Decode(byte[] data)
    {
        string message = "";

        for (int i = 0; i < data.Length; i++)
        {
            int val = data[i];

            if (val == 100)
            {
                message += ' ';
            }
            else if (val < 26)
            {
                int value = (val - CAESAR_SHIFT) % 26;
                
                if (val <= 12)
                {
                    message += (char)(value + 'z' +1);
                }
                else
                    message += (char)(value + 'a');

            }
        }

        return message;
    }
}
