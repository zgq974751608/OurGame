using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DungeonStoryPlayer {
    /// <summary>
    /// 剧情对话配置
    /// </summary>
    public static DungeonStoryData data;
    /// <summary>
    /// 当前进行中的对话段
    /// </summary>
    public static List<DungeonStoryData.Dialog> curDialogBlock;
    public static CallBack curCallBack;

    static DungeonStoryData.Role _PlayerRole;
    public static DungeonStoryData.Role PlayerRole
    {
        get 
        {
            if (_PlayerRole == null)
            {
                /* 从PlayerData中拿角色数据 */
                _PlayerRole = new DungeonStoryData.Role();
            }
            return _PlayerRole;
        }
    }

    static int index;

    public delegate void CallBack();

    public static void PlayStory(DungeonStoryData dt,CallBack callBack)
    {
        data = dt;
        ViewMapper<FightPanel>.instance.BindEvent(PlayNext);
        PlayPreFightDialog(callBack);
    }

    public static void PlayPreFightDialog(CallBack callBack)
    {
        if (data == null || data.preFightDialog.Count == 0)
        {
            ViewMapper<FightPanel>.instance.HideStoryDialog();
            if (callBack != null)
                callBack();
            return;
        }
        curCallBack = callBack;
        curDialogBlock = new List<DungeonStoryData.Dialog>(data.preFightDialog);
        index = 0;
        PlayNext();
    }

    public static void PlayPreBossDialog(CallBack callBack)
    {        
        if (data == null || data.preBossDialog.Count == 0)
        {
            if (callBack != null)
                callBack();
            return;
        }
        curCallBack = callBack;
        curDialogBlock = new List<DungeonStoryData.Dialog>(data.preBossDialog);
        index = 0;
        PlayNext();
    }

    public static void PlayAfterBossDialog(CallBack callBack)
    {
        if (data == null || data.afterBossDialog.Count == 0)
        {
            if (callBack != null)
                callBack();
            return;
        }
        curCallBack = callBack;
        curDialogBlock = new List<DungeonStoryData.Dialog>(data.afterBossDialog);
        index = 0;
        PlayNext();
    }

    public static void PlayNext(params object[] objs)
    {
        if (index == curDialogBlock.Count)
        {
            ViewMapper<FightPanel>.instance.HideStoryDialog();
            if (curCallBack != null)
                curCallBack();
            return;
        }
        DungeonStoryData.Dialog dialog = curDialogBlock[index];
        DungeonStoryData.Role role;
        if (dialog.roleIndex == -1)
            role = PlayerRole;
        else
            role = data.roles[dialog.roleIndex];
        ViewMapper<FightPanel>.instance.DisplayStoryDialog(role,dialog.dialogText);
        index++;
    }
}
