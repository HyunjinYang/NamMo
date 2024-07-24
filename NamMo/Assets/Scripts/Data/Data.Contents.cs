using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region PrologueFadeInScript
    [Serializable]
    public class PrologueFadeInScript
    {
        public int num;
        public List<string> scripts;
    }
    [Serializable] 
    public class PrologueFadeInScriptData : ILoader<int, PrologueFadeInScript>
    {
        public List<PrologueFadeInScript> fadeInScriptscripts = new List<PrologueFadeInScript>();
        public Dictionary<int, PrologueFadeInScript> MakeDict()
        {
            Dictionary<int, PrologueFadeInScript> dict = new Dictionary<int, PrologueFadeInScript>();
            foreach(PrologueFadeInScript script in fadeInScriptscripts)
                dict.Add(script.num, script);
            return dict;
        }
    }
    #endregion
}