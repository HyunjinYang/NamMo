using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
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
    #region Characters
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
    #endregion
    #region Tutorial
    [Serializable]
    public class TutorialInfo
    {
        public Define.TutorialType tutorialType;
        public List<string> tutorialText;
    }
    [Serializable]
    public class TutorialInfoData : ILoader<Define.TutorialType, TutorialInfo>
    {
        public List<TutorialInfo> tutorialInfos = new List<TutorialInfo>();
        public Dictionary<Define.TutorialType, TutorialInfo> MakeDict()
        {
            Dictionary<Define.TutorialType, TutorialInfo> dict = new Dictionary<Define.TutorialType, TutorialInfo>();
            foreach(TutorialInfo tutorialInfo in tutorialInfos)
            {
                dict.Add(tutorialInfo.tutorialType, tutorialInfo);
            }
            return dict;
        }
    }
    #endregion
    #region Enemy
    [Serializable]
    public class Enemy
    {
        public int id;
        public string name;
        public string prefabPath;
    }
    public class EnemyData : ILoader<int, Enemy>
    {
        public List<Enemy> enemies = new List<Enemy>();
        public Dictionary<int, Enemy> MakeDict()
        {
            Dictionary<int, Enemy> dict = new Dictionary<int, Enemy>();
            foreach(Enemy enemy in enemies)
            {
                dict.Add(enemy.id, enemy);
            }
            return dict;
        }
    }
    #endregion
    #region Stage
    [Serializable]
    public class EachEnemyInfo
    {
        public int managedId;
        public int enemyId;
        public float posX;
        public float posY;
        public bool alive;
    }
    [Serializable]
    public class StageEnemy
    {
        public Define.Scene scene;
        public List<EachEnemyInfo> enemies = new List<EachEnemyInfo>();
    }
    public class StageEnemyData : ILoader<Define.Scene, StageEnemy>
    {
        public List<StageEnemy> enemies = new List<StageEnemy>();
        public Dictionary<Define.Scene, StageEnemy> MakeDict()
        {
            Dictionary<Define.Scene, StageEnemy> dict = new Dictionary<Define.Scene, StageEnemy>();
            foreach (StageEnemy enemy in enemies)
            {
                dict.Add(enemy.scene, enemy);
            }
            return dict;
        }
    }
    #endregion
    #region AttackStrength
    [Serializable]
    public class EnemyAttackReactValue
    {
        public float knockbackPower;
        public float bindTime;
    }
    [Serializable]
    public class EnemyAttackReact
    {
        public Define.GameplayAbility reactAbility;
        public List<EnemyAttackReactValue> reactValues = new List<EnemyAttackReactValue>();
    }
    public class EnemyAttackReactData : ILoader<Define.GameplayAbility, EnemyAttackReact>
    {
        public List<EnemyAttackReact> enemyAttackReacts = new List<EnemyAttackReact>();
        public Dictionary<Define.GameplayAbility, EnemyAttackReact> MakeDict()
        {
            Dictionary<Define.GameplayAbility, EnemyAttackReact> dict = new Dictionary<Define.GameplayAbility, EnemyAttackReact>();
            foreach (EnemyAttackReact react in enemyAttackReacts)
            {
                dict.Add(react.reactAbility, react);
            }
            return dict;
        }
    }
    #endregion
}