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
    #region Conversation
    [Serializable]
    public class Conversation
    {
        public int num;
        public int characterId;
        public List<string> scripts;
    }
    [Serializable]
    public class ConversationData : ILoader<int, Conversation>
    {
        public List<Conversation> conversations = new List<Conversation>();
        public Dictionary<int, Conversation> MakeDict()
        {
            Dictionary<int, Conversation> dict = new Dictionary<int, Conversation>();
            foreach(Conversation conversation in conversations)
                dict.Add(conversation.num, conversation);
            return dict;
        }
    }
    #endregion
    [Serializable]
    public class CharacterInfo
    {
        public int id;
        public string iconPath;
        public List<string> names;
    }
    [Serializable]
    public class CharacterInfoData : ILoader<int, CharacterInfo>
    {
        public List<CharacterInfo> characterInfos = new List<CharacterInfo>();
        public Dictionary<int, CharacterInfo> MakeDict()
        {
            Dictionary<int, CharacterInfo> dict = new Dictionary<int, CharacterInfo>();
            foreach (CharacterInfo characterInfo in characterInfos)
                dict.Add(characterInfo.id, characterInfo);
            return dict;
        }
    }
}