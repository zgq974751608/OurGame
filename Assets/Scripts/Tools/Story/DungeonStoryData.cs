using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonStoryData : ScriptableObject {
    [System.Serializable]
    public class Dialog
    {
        //角色
        public int roleIndex;       
        //对话文本
        public string dialogText;
    }

    [System.Serializable]
    public class Role
    {
        //头像图集
        public UIAtlas atlas;
        //头像精灵名称
        public string headShot;
        //名字
        public string name;
        //头像的位置
        public bool isLeft;
        //角色id（方便配置）
        public string nameId;
    }

    /// <summary>
    /// 故事中所有出现的角色
    /// </summary>
    public List<Role> roles = new List<Role>();

    /// <summary>
    /// 第一次战斗前的对话
    /// </summary>
    public List<Dialog> preFightDialog = new List<Dialog>();

    /// <summary>
    /// boss战前对话
    /// </summary>
    public List<Dialog> preBossDialog = new List<Dialog>();

    /// <summary>
    /// boss战后对话
    /// </summary>
    public List<Dialog> afterBossDialog = new List<Dialog>();
}
