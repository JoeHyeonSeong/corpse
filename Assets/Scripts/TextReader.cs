using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public class TextReader {
    static public string ReadFile(string fileName)
    {
        TextAsset reader =(TextAsset) Resources.Load(fileName);
        return reader.text;
    } 
	
}
