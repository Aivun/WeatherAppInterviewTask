using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WeatherSDK
{
    [CreateAssetMenu(fileName = "CodesDatabase", menuName = "ScriptableObjects/CodesDatabase", order = 1)]
    public class CodesDatabase : ScriptableObject
    {
        public List<CodesDatabaseEntry> Codes;

        public string GetTextForCode(int code)
        {
            var codeEntry = Codes.FirstOrDefault(x => x.Code == code);
            if (codeEntry != null)
            {
                return codeEntry.Text.Trim();
            }
            return "Unknown";
        }
    }

    [Serializable]
    public class CodesDatabaseEntry
    {
        public int Code;
        public string Text;
    }
}
