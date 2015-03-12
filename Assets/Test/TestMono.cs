/*****
************Add by 朱桂清

************创建于2015-01-10

************
*****/

using UnityEngine;
using System.Collections;

public class TestMono : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*
		Test t = new Test();
		BaseTest bt = t as BaseTest;
		bt.A();
		Test t1 = bt as Test;
		t1.A();
		*/
		MGConfigDataLoader.Instance().MemoryConfigData("Configuration/Config/Sample");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
