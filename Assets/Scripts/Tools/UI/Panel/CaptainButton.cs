using UnityEngine;
using System.Collections;

public class CaptainButton : BaseButton {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        UISprite sprite = this.gameObject.GetComponent<UISprite>();

        if (sprite.atlas)
        {
            GameObject captianobj = GameObject.Find("Upside/Left/Captain").gameObject;

            UISprite herosprite = captianobj.transform.FindChild("Sprite").GetComponent<UISprite>();
            if (herosprite.atlas == null && sprite.atlas)
            {
                herosprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
                herosprite.spriteName = sprite.spriteName;
                herosprite.MakePixelPerfect();

                sprite.atlas = null;
               // sprite.MakePixelPerfect();
            }
        }
        else
        {
            return;
        }
    }
}
