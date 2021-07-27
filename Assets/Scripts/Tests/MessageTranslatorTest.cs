using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MessageTranslatorTest
{
    const string chars = "abcdefghijklmnopqrstuvwxyz ";

    [TestCase("hello world")]
    [TestCase("translation test string")]
    public void SimpleTranslationTest(string message)
    {
        byte[] translation = MessageTranslator.Encode(message);
        string decoded = MessageTranslator.Decode(translation);

        Assert.AreEqual(message, decoded);
    }

    [Test]
    public void AutoTranslationTest()
    {
        for (int i = 0; i < 1000; i++)
        {
            string msg = GetRandomString();
            SimpleTranslationTest(msg);
        }
    }

    private string GetRandomString()
    {
        System.Random rnd = new System.Random();

        int len = rnd.Next(1, 50);
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < len; i++)
        {
            sb.Append(chars[rnd.Next(0, chars.Length)]);
        }
        return sb.ToString();
    } 
}
