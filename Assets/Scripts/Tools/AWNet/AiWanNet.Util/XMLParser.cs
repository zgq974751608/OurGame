﻿using System;
namespace AiWanNet.Util
{
	public class XMLParser
	{
		private char LT = '<';
		private char GT = '>';
		private char SPACE = ' ';
		private char QUOTE = '"';
		private char QUOTE2 = '\'';
		private char SLASH = '/';
		private char QMARK = '?';
		private char EQUALS = '=';
		private char EXCLAMATION = '!';
		private char DASH = '-';
		private char SQR = ']';
		public XMLNode Parse(string content)
		{
			XMLNode xMLNode = new XMLNode();
			xMLNode["_text"] = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			XMLNodeList xMLNodeList = new XMLNodeList();
			XMLNode xMLNode2 = xMLNode;
			for (int i = 0; i < content.Length; i++)
			{
				char c = content[i];
				char c2 = '~';
				char c3 = '~';
				char c4 = '~';
				if (i + 1 < content.Length)
				{
					c2 = content[i + 1];
				}
				if (i + 2 < content.Length)
				{
					c3 = content[i + 2];
				}
				if (i > 0)
				{
					c4 = content[i - 1];
				}
				if (flag6)
				{
					if (c == this.QMARK && c2 == this.GT)
					{
						flag6 = false;
						i++;
					}
				}
				else
				{
					if (!flag5 && c == this.LT && c2 == this.QMARK)
					{
						flag6 = true;
					}
					else
					{
						if (flag7)
						{
							if (c4 == this.DASH && c == this.DASH && c2 == this.GT)
							{
								flag7 = false;
								i++;
							}
						}
						else
						{
							if (!flag5 && c == this.LT && c2 == this.EXCLAMATION)
							{
								if (content.Length > i + 9 && content.Substring(i, 9) == "<![CDATA[")
								{
									flag8 = true;
									i += 8;
								}
								else
								{
									flag7 = true;
								}
							}
							else
							{
								if (flag8)
								{
									if (c == this.SQR && c2 == this.SQR && c3 == this.GT)
									{
										flag8 = false;
										i += 2;
									}
									else
									{
										text4 += c;
									}
								}
								else
								{
									if (flag)
									{
										if (flag2)
										{
											if (c == this.SPACE)
											{
												flag2 = false;
											}
											else
											{
												if (c == this.GT)
												{
													flag2 = false;
													flag = false;
												}
											}
											if (!flag2 && text3.Length > 0)
											{
												if (text3[0] == this.SLASH)
												{
													if (text4.Length > 0)
													{
														XMLNode xMLNode3 = new XMLNode();
														(xMLNode3 = xMLNode2)["_text"] = xMLNode3["_text"] + text4;
													}
													text4 = "";
													text3 = "";
													xMLNode2 = xMLNodeList.Pop();
												}
												else
												{
													if (text4.Length > 0)
													{
														XMLNode xMLNode3 = new XMLNode();
														(xMLNode3 = xMLNode2)["_text"] = xMLNode3["_text"] + text4;
													}
													text4 = "";
													XMLNode xMLNode4 = new XMLNode();
													xMLNode4["_text"] = "";
													xMLNode4["_name"] = text3;
													if (xMLNode2[text3] == null)
													{
														xMLNode2[text3] = new XMLNodeList();
													}
													XMLNodeList xMLNodeList2 = (XMLNodeList)xMLNode2[text3];
													xMLNodeList2.Push(xMLNode4);
													xMLNodeList.Push(xMLNode2);
													xMLNode2 = xMLNode4;
													text3 = "";
												}
											}
											else
											{
												text3 += c;
											}
										}
										else
										{
											if (!flag5 && c == this.SLASH && c2 == this.GT)
											{
												flag = false;
												flag3 = false;
												flag4 = false;
												if (text.Length > 0)
												{
													if (text2.Length > 0)
													{
														xMLNode2["@" + text] = text2;
													}
													else
													{
														xMLNode2["@" + text] = true;
													}
												}
												i++;
												xMLNode2 = xMLNodeList.Pop();
												text = "";
												text2 = "";
											}
											else
											{
												if (!flag5 && c == this.GT)
												{
													flag = false;
													flag3 = false;
													flag4 = false;
													if (text.Length > 0)
													{
														xMLNode2["@" + text] = text2;
													}
													text = "";
													text2 = "";
												}
												else
												{
													if (flag3)
													{
														if (c == this.SPACE || c == this.EQUALS)
														{
															flag3 = false;
															flag4 = true;
														}
														else
														{
															text += c;
														}
													}
													else
													{
														if (flag4)
														{
															if (c == this.QUOTE || c == this.QUOTE2)
															{
																if (flag5)
																{
																	flag4 = false;
																	xMLNode2["@" + text] = text2;
																	text2 = "";
																	text = "";
																	flag5 = false;
																}
																else
																{
																	flag5 = true;
																}
															}
															else
															{
																if (flag5)
																{
																	text2 += c;
																}
																else
																{
																	if (c == this.SPACE)
																	{
																		flag4 = false;
																		xMLNode2["@" + text] = text2;
																		text2 = "";
																		text = "";
																	}
																}
															}
														}
														else
														{
															if (c != this.SPACE)
															{
																flag3 = true;
																text = "" + c;
																text2 = "";
																flag5 = false;
															}
														}
													}
												}
											}
										}
									}
									else
									{
										if (c == this.LT)
										{
											flag = true;
											flag2 = true;
										}
										else
										{
											text4 += c;
										}
									}
								}
							}
						}
					}
				}
			}
			return xMLNode;
		}
	}
}
