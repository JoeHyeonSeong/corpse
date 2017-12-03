using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomanNumber : MonoBehaviour {
    enum Numeral
    {
        I = 1, IV = 4, V = 5, IX = 9, X = 10
    }

    public static string Roman(long n)
    {

        if (n <= 0)
        {
        throw new System.Exception();
        }

        System.Text.StringBuilder buf = new System.Text.StringBuilder();

        Numeral[] values = (Numeral[])System.Enum.GetValues(typeof(Numeral));
        for (int i = values.Length - 1; i >= 0; i--)
        {
            while (n >= (int)values[i])
            {
                buf.Append(values[i]);
                n -= (int)values[i];
            }
        }
        return buf.ToString();
    }
}