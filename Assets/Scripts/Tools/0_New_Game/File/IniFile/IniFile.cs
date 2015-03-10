using System;
using System.Collections;
using UnityEngine;
public class IniFile
{
    private Hashtable ht;
    private string[] textData;
    public IniFile(string fileName)
    {
        string key = string.Empty;
        this.ht = new Hashtable();
        string text = Util.LoadText(fileName);
        if (text != null)
        {
            this.textData = text.Split(new char[]
			{
				'\n'
			});
            for (int i = 0; i < this.textData.Length; i++)
            {
                string text2 = this.textData[i].Trim();
                if (!text2.Equals(string.Empty))
                {
                    if (!text2.Substring(0, 2).Equals("//"))
                    {
                        if (text2.StartsWith("[") && text2.EndsWith("]"))
                        {
                            key = text2.Substring(1, text2.Length - 2);
                        }
                        else
                        {
                            string key2 = text2.Substring(0, text2.IndexOf('='));
                            string value = text2.Substring(text2.IndexOf('=') + 1, text2.Length - text2.IndexOf('=') - 1);
                            if (this.ht.ContainsKey(key))
                            {
                                Hashtable hashtable = (Hashtable)this.ht[key];
                                hashtable.Add(key2, value);
                            }
                            else
                            {
                                Hashtable hashtable2 = new Hashtable();
                                hashtable2.Add(key2, value);
                                this.ht.Add(key, hashtable2);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogError("Inifile:>" + fileName + " object is null!!");
            }
        }
    }
    public int ReadInt(string section, string ident, int defaultValue)
    {
        int result = defaultValue;
        if (this.ht.ContainsKey(section))
        {
            Hashtable hashtable = (Hashtable)this.ht[section];
            if (hashtable.ContainsKey(ident))
            {
                result = Convert.ToInt32(hashtable[ident]);
            }
        }
        return result;
    }
    public string ReadString(string section, string ident, string defaultVal)
    {
        string result = defaultVal;
        if (this.ht.ContainsKey(section))
        {
            Hashtable hashtable = (Hashtable)this.ht[section];
            if (hashtable.ContainsKey(ident))
            {
                result = hashtable[ident].ToString();
            }
        }
        return result;
    }
    public float ReadSingle(string section, string ident, float defaultValue)
    {
        float result = defaultValue;
        if (this.ht.ContainsKey(section))
        {
            Hashtable hashtable = (Hashtable)this.ht[section];
            if (hashtable.ContainsKey(ident))
            {
                result = Convert.ToSingle(hashtable[ident]);
            }
        }
        return result;
    }
    public double ReadDouble(string section, string ident, double defaultValue)
    {
        double result = defaultValue;
        if (this.ht.ContainsKey(section))
        {
            Hashtable hashtable = (Hashtable)this.ht[section];
            if (hashtable.ContainsKey(ident))
            {
                result = Convert.ToDouble(hashtable[ident]);
            }
        }
        return result;
    }
    public void WriteString(string section, string ident, string val)
    {
        if (this.SectionExists(section))
        {
            Hashtable hashtable = (Hashtable)this.ht[section];
            if (hashtable.ContainsKey(ident))
            {
                hashtable[ident] = val;
            }
            else
            {
                hashtable.Add(ident, val);
            }
        }
        else
        {
            Hashtable hashtable2 = new Hashtable();
            this.ht.Add(section, hashtable2);
            hashtable2.Add(ident, val);
        }
    }
    public bool SectionExists(string section)
    {
        return this.ht.ContainsKey(section);
    }
    public Hashtable GetSection(string section)
    {
        if (this.SectionExists(section))
        {
            return this.ht[section] as Hashtable;
        }
        return null;
    }
    public bool ValueExists(string section, string ident)
    {
        return this.SectionExists(section) && ((Hashtable)this.ht[section]).ContainsKey(ident);
    }
}

