using UnityEngine;
using System.Collections;

public class HeroButton : BaseButton {

    private float currentTime;
    float pressTime;
    bool longPress = false;
    public float pressDetection = 0.1f;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        currentTime = Time.realtimeSinceStartup;
        if (currentTime > pressTime + pressDetection && longPress == true)
        {
            io.panelManager.CreatePanel(DialogType.HeroTips);
            longPress = false;
        }
	}

    public void OnPress(bool press)
    {
        UISprite sprite = this.gameObject.GetComponent<UISprite>();

        if (sprite.atlas)
        {
            foreach (var element in ArrangementPanel.spritename)
            {
                if (sprite.spriteName == element.Value)
                {
                    Const.heroid = element.Key;
                    break;
                }
            }
        }

        longPress = press;

        if (currentTime <= pressTime + pressDetection)
        {
           //UISprite sprite = this.gameObject.GetComponent<UISprite>();
        
            if (sprite.atlas)
            {
                foreach (var element in ArrangementPanel.spritename)
                {
                    if (sprite.spriteName == element.Value)
                    {
                        PlayerData.GetInstance().CurrentFightHero.Add(element.Key);
                        Const.heroid = element.Key;
                        break;
                    }
                }

                sprite.atlas = null;
                //sprite.MakePixelPerfect();
            }
            else
            {
                return;
            }

            GameObject heroobj = GameObject.Find("Upside/Left").gameObject;
            int count = heroobj.transform.childCount;
            for (int i = 0; i < count; i++ )
            {
                UISprite herosprite = heroobj.transform.GetChild(i).FindChild("Sprite").GetComponent<UISprite>();
                if (herosprite.atlas == null)
                {
                    herosprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
                    herosprite.spriteName = sprite.spriteName;
                    //herosprite.MakePixelPerfect();
                    break;
                }
            }
      }
        pressTime = Time.realtimeSinceStartup;  
        
    }
    
   }
