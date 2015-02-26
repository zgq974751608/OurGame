using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoTblConfig;
using clientTBL;
using LitJson;
using Frankfort.Threading;
using System.Threading;

public class PreLoadConfig : MonoBehaviour {
    public int dungeonId;
    private ThreadPoolScheduler myThreadScheduler;
    public int maxThreads = 2;
    public static string text;
    private FlockingDataWorker[] workerObjects;
    public static string summontext;


    void Start()
    {
        DontDestroyOnLoad(this);
        myThreadScheduler = UThread.CreateThreadPoolScheduler();
        //myThreadScheduler.ForceToMainThread = true;
        //UThread.StartSingleThread(LoadConfig, System.Threading.ThreadPriority.Normal, true);
        workerObjects = new FlockingDataWorker[1];
        int maxThreads = -1;
        if (maxThreads <= 0)
            maxThreads = Mathf.Max(SystemInfo.processorCount - 1, 1);

         FlockingDataWorker workerBlock = new FlockingDataWorker();
         workerObjects[0] = workerBlock;
 
        myThreadScheduler.ForceToMainThread = true;
        myThreadScheduler.StartASyncThreads(workerObjects, onThreadWorkComplete, null, maxThreads);
       
        StartCoroutine(LoadPosFile());
        StartCoroutine(LoadSummonPosFile());
        
    }

    float time;
  
    IEnumerator LoadPosFile()
    {
        string path = Util.AppContentDataUri + RotateSequence.posfile + ".txt";
        WWW www = new WWW(path);
        yield return www;
        text = www.text;  
    }

    IEnumerator LoadSummonPosFile()
    {
        string path = Util.AppContentDataUri + SummonPanel.posfile + ".txt";
        WWW www = new WWW(path);
        yield return www;
        summontext = www.text;  
    }

    void onThreadWorkComplete(IThreadWorkerObject[] finishedObjects)
    {
        Debug.Log("onThreadWorkComplete");
    }
}

public class FlockingDataWorker : IThreadWorkerObject
{
    public  FlockingDataWorker()
    {
        //LoadConfig();
    }

    public void AbortThreadedWork()
    {
        
    }

    public void ExecuteThreadedWork()
    {
        LoadConfig();
    }

    void LoadConfig()
    {
        Util.Load<MsgAttackData, AttackData>();
        Util.Load<MsgAwakeningSkill, AwakeningSkill>();
        Util.Load<MsgAwakeningSkillLevel, AwakeningSkillLevel>();
        Util.Load<MsgElementFactor, ElementFactor>();
        Util.Load<MsgBuffData, BuffData>();
        Util.Load<MsgCombinationData, CombinationData>();
        Util.Load<MsgConstData, ConstData>();
        Util.Load<MsgCurrency, Currency>();
        Util.Load<MsgDungeon, Dungeon>();

        Util.Load<MsgEnemyData, EnemyData>();
        Util.Load<MsgEvaluateBonus, EvaluateBonus>();
        Util.Load<MsgFieldEffect, FieldEffect>();
        Util.Load<MsgHeroData, HeroData>();
        Util.Load<MsgHeroLvData, HeroLvData>();
        Util.Load<MsgHeroEvolution, HeroEvolution>();
        Util.Load<MsgPotentialData, PotentialData>();
        Util.Load<MsgItemData, ItemData>();
        Util.Load<MsgLeaderSkill, LeaderSkill>();
        Util.Load<MsgMonsterData, MonsterData>();
        Util.Load<MsgMonsterSkill, MonsterSkill>();
        Util.Load<MsgNormalSkill, NormalSkill>();
        Util.Load<MsgNormalSkillLvUp, NormalSkillLvUp>();

        Util.Load<MsgRoleData, RoleData>();
        Util.Load<MsgRuneData, RuneData>();
        Util.Load<MsgRuneSet, RuneSet>();
        Util.Load<MsgRuneEnhance, RuneEnhance>();
        Util.Load<MsgRuneSynchro, RuneSynchro>();
        Util.Load<MsgSpecialSkill, SpecialSkill>();
        Util.Load<MsgSpecialSkillLvUp, SpecialSkillLvUp>();
        Util.Load<MsgDecisionFactor, DecisionFactor>();
        Util.Load<MsgTaskData, TaskData>();
        Util.Load<MsgHeroText, HeroText>();
        Util.Load<MsgDungeonText, DungeonText>();
        Util.Load<MsgMonsterText, MonsterText>();
        Util.Load<MsgFieldText, FieldText>();
        Util.Load<MsgCombinationText, CombinationText>();
        Util.Load<MsgSkillText, SkillText>();
        Util.Load<MsgCurrencyText, CurrencyText>();
        Util.Load<MsgBuffText, BuffText>();
        Util.Load<MsgItemText, ItemText>();
        Util.Load<MsgRuneText, RuneText>();
        Util.Load<MsgTaskText, TaskText>();
        Util.Load<MsgWorldData, WorldData>();
        Util.Load<MsgSummonData, SummonData>();
        Util.Load<MsgSummonPrize, SummonPrize>();
        Util.Load<MsgStarWeight, StarWeight>();
        Util.Load<MsgSpecialEffect, SpecialEffect>();
        Util.Load<MsgFlyingEffect, FlyingEffect>();
        Util.Load<MsgDistanceFactor, DistanceFactor>();
        Util.Load<MsgEvaluateData, EvaluateData>();
        Util.Load<MsgPromptText, PromptText>();
        Util.Load<MsgStoryText, StoryText>();
        //NGUIDebug.Log((Time.realtimeSinceStartup - time).ToString());
        LoadConst();
            

        //LoadScene.LoadDungeon(dungeonId);
    }

    //赋值常数
    void LoadConst()
    {
        List<ConstData> constDataList = Util.GetList<MsgConstData, ConstData>();
        foreach (ConstData data in constDataList)
        {
            string constName = Util.GetConfigString(data.name);
            string constValue = JsonMapper.ToObject(Util.GetConfigString(data.value))[0].ToString();
            switch (constName)
            {
                case "CONST_MAX_LEVEL":
                    Const.CONST_MAX_LEVEL = int.Parse(constValue);
                    break;
                case "CONST_STAMINA_RECOVER":
                    Const.CONST_STAMINA_RECOVER = int.Parse(constValue);
                    break;
                case "CONST_STAMINA_GIFT":
                    Const.CONST_STAMINA_GIFT = constValue;
                    break;
                case "CONST_MAX_FRIEND":
                    Const.CONST_MAX_FRIEND = int.Parse(constValue);
                    break;
                case "CONST_HERO_LVUP_CONSUME":
                    Const.CONST_HERO_LVUP_CONSUME = int.Parse(constValue);
                    break;
                case "CONST_HERO_POTENTIAL_UP":
                    Const.CONST_HERO_POTENTIAL_UP = int.Parse(constValue);
                    break;
                case "CONST_HERO_POTENTIAL_COIN":
                    Const.CONST_HERO_POTENTIAL_COIN = int.Parse(constValue);
                    break;
                case "CONST_MAX_ENERGY":
                    Const.CONST_MAX_ENERGY = int.Parse(constValue);
                    break;
                case "CONST_ENERGY_RECOVER_TIME":
                    Const.CONST_ENERGY_RECOVER_TIME = int.Parse(constValue);
                    break;
                case "CONST_ENERGY_RECOVER_VAL":
                    Const.CONST_ENERGY_RECOVER_VAL = int.Parse(constValue);
                    break;
                case "CONST_MIN_ENERGY_LIMIT":
                    Const.CONST_MIN_ENERGY_LIMIT = int.Parse(constValue);
                    break;
                case "CONST_RUNE_MAX_LEVEL":
                    Const.CONST_RUNE_MAX_LEVEL = int.Parse(constValue);
                    break;
                case "CONST_RUNE_LEVEL_INTERVAL":
                    Const.CONST_RUNE_LEVEL_INTERVAL = int.Parse(constValue);
                    break;
                case "CONST_ATTACK_INTERVAL":
                    Const.CONST_ATTACK_INTERVAL = int.Parse(constValue);
                    break;
                case "CONST_BASIC_MISS_RATE":
                    Const.CONST_BASIC_MISS_RATE = float.Parse(constValue);
                    break;
                case "CONST_DODGE_FACTOR":
                    Const.CONST_DODGE_FACTOR = float.Parse(constValue);
                    break;
                case "CONST_MAX_MISS_RATE":
                    Const.CONST_MAX_MISS_RATE = float.Parse(constValue);
                    break;
                case "CONST_MIN_MISS_RATE":
                    Const.CONST_MIN_MISS_RATE = float.Parse(constValue);
                    break;
                case "CONST_BASIC_CRIT_RATE":
                    Const.CONST_BASIC_CRIT_RATE = float.Parse(constValue);
                    break;
                case "CONST_CRIT_FACTOR":
                    Const.CONST_CRIT_FACTOR = float.Parse(constValue);
                    break;
                case "CONST_MAX_CRIT_RATE":
                    Const.CONST_MAX_CRIT_RATE = float.Parse(constValue);
                    break;
                case "CONST_MIN_CRIT_RATE":
                    Const.CONST_MIN_CRIT_RATE = float.Parse(constValue);
                    break;
                case "CONST_BASIC_CRIT_DMG":
                    Const.CONST_BASIC_CRIT_DMG = int.Parse(constValue);
                    break;
                case "CONST_RESIST_FACTOR":
                    Const.CONST_RESIST_FACTOR = float.Parse(constValue);
                    break;
                case "CONST_MAX_DEF_EFFECT":
                    Const.CONST_MAX_DEF_EFFECT = int.Parse(constValue);
                    break;
                case "CONST_MIN_DEF_EFFECT":
                    Const.CONST_MIN_DEF_EFFECT = float.Parse(constValue);
                    break;
                case "CONST_DEF_FACTOR":
                    Const.CONST_DEF_FACTOR = float.Parse(constValue);
                    break;
                case "CONST_COMBO_TIME":
                    Const.CONST_COMBO_TIME = float.Parse(constValue);
                    break;
                case "CONST_SPECIALSKILL_RNDMAX":
                    Const.CONST_SPECIALSKILL_RNDMAX = int.Parse(constValue);
                    break;
                case "CONST_SPECIALSKILL_RNDMIN":
                    Const.CONST_SPECIALSKILL_RNDMIN = int.Parse(constValue);
                    break;
                case "CONST_MERCENARY_COOLDOWN":
                    Const.CONST_MERCENARY_COOLDOWN = int.Parse(constValue);
                    break;
                case "CONST_MERCENARY_TIME":
                    Const.CONST_MERCENARY_TIME = int.Parse(constValue);
                    break;
                case "CONST_MERCENARY_FEE":
                    Const.CONST_MERCENARY_FEE = float.Parse(constValue);
                    break;
                case "CONST_RUNE_EXPAND_INTERVAL":
                    Const.CONST_RUNE_EXPAND_INTERVAL = int.Parse(constValue);
                    break;
                case "CONST_MAX_SUMMON_VAL_INIT":
                    Const.CONST_MAX_SUMMON_VAL_INIT = int.Parse(constValue);
                    break;
                case "CONST_FIRST_HERO":
                    Const.CONST_FIRST_HERO = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_MAX_TIME_POINT":
                    Const.CONST_DUNGEON_MAX_TIME_POINT = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_TIME_FACTOR":
                    Const.CONST_DUNGEON_TIME_FACTOR = float.Parse(constValue);
                    break;
                case "CONST_DUNGEON_LIFE_PCT":
                    Const.CONST_DUNGEON_LIFE_PCT = float.Parse(constValue);
                    break;
                case "CONST_DUNGEON_MAX_LIFE_POINT":
                    Const.CONST_DUNGEON_MAX_LIFE_POINT = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_MAX_COMBO_POINT":
                    Const.CONST_DUNGEON_MAX_COMBO_POINT = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_COMBO_FACTOR":
                    Const.CONST_DUNGEON_COMBO_FACTOR = float.Parse(constValue);
                    break;
                case "CONST_DUNGEON_COMBO_BASIC":
                    Const.CONST_DUNGEON_COMBO_BASIC = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_TECH_BASIC":
                    Const.CONST_DUNGEON_TECH_BASIC = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_TECH_FACTOR":
                    Const.CONST_DUNGEON_TECH_FACTOR = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_DEATH_BASIC":
                    Const.CONST_DUNGEON_DEATH_BASIC = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_DEATH_FACTOR":
                    Const.CONST_DUNGEON_DEATH_FACTOR = float.Parse(constValue);
                    break;
                case "CONST_DUNGEON_SPECSKILL_FACTOR":
                    Const.CONST_DUNGEON_SPECSKILL_FACTOR = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_MAX_SPECSKILL_POINT":
                    Const.CONST_DUNGEON_MAX_SPECSKILL_POINT = int.Parse(constValue);
                    break;
                case "CONST_DUNGEON_SPECSKILL_BASIC":
                    Const.CONST_DUNGEON_SPECSKILL_BASIC = int.Parse(constValue);
                    break;               
            }
        }
    }
}

