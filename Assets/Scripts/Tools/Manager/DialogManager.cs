using System;
using System.Collections;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    private Hashtable dialogs = new Hashtable();

    public bool DialogExist(DialogType type)
    {
        return this.dialogs.ContainsKey(type);
    }

    public DialogInfo AddDialog(DialogType type)
    {
        DialogInfo dialoginfo = new DialogInfo();
        dialoginfo.Type = type;
        this.dialogs.Add(type, dialoginfo);
        return dialoginfo;
    }

    public void ResetDialog()
    {
        IDictionaryEnumerator enumerator = this.dialogs.GetEnumerator();
        while (enumerator.MoveNext())
        {
            DialogInfo dialoginfo = enumerator.Value as DialogInfo;
            dialoginfo.AsynState = AsynState.Completed;
        }
    }

    public DialogInfo GetDialogInfo(DialogType type)
    {
        if (!this.DialogExist(type))
        {
            return this.AddDialog(type);
        }

        return this.dialogs[type] as DialogInfo;
    }

    public void RemoveDialog(DialogType type)
    {
        if (this.DialogExist(type))
        {
            this.dialogs.Remove(type);
        }
    }

    public void ClearDialog()
    {
        this.dialogs.Clear();
    }

    public Transform GetDialog(DialogType type)
    {
        if (type == DialogType.None)
        {
            return null;
        }

        string str = Util.ConvertPanelName(type);
        if (str == "MainPanel")
        {
            Transform obj = null;
            if(GameObject.Find("MainPanel"))
                 obj = GameObject.Find("MainPanel").transform;
          
                return obj;
        }
        else
        {
            return io.Gui.transform.Find(str);
        }
    }
}
