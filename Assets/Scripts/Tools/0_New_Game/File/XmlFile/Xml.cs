using Mono.Xml;
using System;
using System.Security;
public class Xml
{
	private static Xml helper;
	private SecurityParser xmlDoc;
	private Xml(string file)
	{
		this.InitHelper(file);
	}
	private void InitHelper(string filecContent)
	{
		//string xml = MGFileUtil.LoadXml(file);
		this.xmlDoc = new SecurityParser();
		this.xmlDoc.LoadXml(filecContent);
	}
	public SecurityElement LoadXml()
	{
		return this.xmlDoc.ToXml();
	}
	public static Xml GetHelper(string file)
	{
		if (Xml.helper == null)
		{
			Xml.helper = new Xml(file);
		}
		else
		{
			Xml.helper.InitHelper(file);
		}
		return Xml.helper;
	}
}
