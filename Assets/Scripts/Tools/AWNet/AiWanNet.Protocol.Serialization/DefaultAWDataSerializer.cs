using AiWanNet.Entities.Data;
using AiWanNet.Exceptions;
using AiWanNet.Util;
using System;
using System.Collections;
using System.Reflection;
namespace AiWanNet.Protocol.Serialization
{
    public class DefaultAWDataSerializer : IAWDataSerializer
	{
		private static readonly string CLASS_MARKER_KEY = "$C";
		private static readonly string CLASS_FIELDS_KEY = "$F";
		private static readonly string FIELD_NAME_KEY = "N";
		private static readonly string FIELD_VALUE_KEY = "V";
		private static DefaultAWDataSerializer instance;
		private static Assembly runningAssembly = null;
		public static DefaultAWDataSerializer Instance
		{
			get
			{
				if (DefaultAWDataSerializer.instance == null)
				{
					DefaultAWDataSerializer.instance = new DefaultAWDataSerializer();
				}
				return DefaultAWDataSerializer.instance;
			}
		}
		public static Assembly RunningAssembly
		{
			get
			{
				return DefaultAWDataSerializer.runningAssembly;
			}
			set
			{
				DefaultAWDataSerializer.runningAssembly = value;
			}
		}
		private DefaultAWDataSerializer()
		{
		}
		public ByteArray Object2Binary(IAWObject obj)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(Convert.ToByte(18));
			byteArray.WriteShort(Convert.ToInt16(obj.Size()));
			return this.Obj2bin(obj, byteArray);
		}
		private ByteArray Obj2bin(IAWObject obj, ByteArray buffer)
		{
			string[] keys = obj.GetKeys();
			string[] array = keys;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				AWDataWrapper data = obj.GetData(text);
				buffer = this.EncodeSFSObjectKey(buffer, text);
				buffer = this.EncodeObject(buffer, data.Type, data.Data);
			}
			return buffer;
		}
		public ByteArray Array2Binary(IAWArray array)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(Convert.ToByte(17));
			byteArray.WriteShort(Convert.ToInt16(array.Size()));
			return this.Arr2bin(array, byteArray);
		}
		private ByteArray Arr2bin(IAWArray array, ByteArray buffer)
		{
			for (int i = 0; i < array.Size(); i++)
			{
				AWDataWrapper wrappedElementAt = array.GetWrappedElementAt(i);
				buffer = this.EncodeObject(buffer, wrappedElementAt.Type, wrappedElementAt.Data);
			}
			return buffer;
		}
		public IAWObject Binary2Object(ByteArray data)
		{
			if (data.Length < 3)
			{
				throw new AWCodecError("Can't decode an AWObject. Byte data is insufficient. Size: " + data.Length + " byte(s)");
			}
			data.Position = 0;
			return this.DecodeSFSObject(data);
		}
		private IAWObject DecodeSFSObject(ByteArray buffer)
		{
			AWObject sFSObject = AWObject.NewInstance();
			byte b = buffer.ReadByte();
			if (b != Convert.ToByte(18))
			{
				throw new AWCodecError(string.Concat(new object[]
				{
					"Invalid AWDataType. Expected: ",
					AWDataType.SFS_OBJECT,
					", found: ",
					b
				}));
			}
			int num = (int)buffer.ReadShort();
			if (num < 0)
			{
				throw new AWCodecError("Can't decode AWObject. Size is negative: " + num);
			}
			try
			{
				for (int i = 0; i < num; i++)
				{
					string text = buffer.ReadUTF();
					AWDataWrapper sFSDataWrapper = this.DecodeObject(buffer);
					if (sFSDataWrapper == null)
					{
						throw new AWCodecError("Could not decode value for AWObject with key: " + text);
					}
					sFSObject.Put(text, sFSDataWrapper);
				}
			}
			catch (AWCodecError sFSCodecError)
			{
				throw sFSCodecError;
			}
			return sFSObject;
		}
		public IAWArray Binary2Array(ByteArray data)
		{
			if (data.Length < 3)
			{
				throw new AWCodecError("Can't decode an AWArray. Byte data is insufficient. Size: " + data.Length + " byte(s)");
			}
			data.Position = 0;
			return this.DecodeSFSArray(data);
		}
		private IAWArray DecodeSFSArray(ByteArray buffer)
		{
			IAWArray iSFSArray = AWArray.NewInstance();
			AWDataType sFSDataType = (AWDataType)Convert.ToInt32(buffer.ReadByte());
			if (sFSDataType != AWDataType.SFS_ARRAY)
			{
				throw new AWCodecError(string.Concat(new object[]
				{
					"Invalid AWDataType. Expected: ",
					AWDataType.SFS_ARRAY,
					", found: ",
					sFSDataType
				}));
			}
			int num = (int)buffer.ReadShort();
			if (num < 0)
			{
				throw new AWCodecError("Can't decode AWArray. Size is negative: " + num);
			}
			try
			{
				for (int i = 0; i < num; i++)
				{
					AWDataWrapper sFSDataWrapper = this.DecodeObject(buffer);
					if (sFSDataWrapper == null)
					{
						throw new AWCodecError("Could not decode AWArray item at index: " + i);
					}
					iSFSArray.Add(sFSDataWrapper);
				}
			}
			catch (AWCodecError sFSCodecError)
			{
				throw sFSCodecError;
			}
			return iSFSArray;
		}
		private AWDataWrapper DecodeObject(ByteArray buffer)
		{
			AWDataType sFSDataType = (AWDataType)Convert.ToInt32(buffer.ReadByte());
			AWDataWrapper result;
			if (sFSDataType == AWDataType.NULL)
			{
				result = this.BinDecode_NULL(buffer);
			}
			else
			{
				if (sFSDataType == AWDataType.BOOL)
				{
					result = this.BinDecode_BOOL(buffer);
				}
				else
				{
					if (sFSDataType == AWDataType.BOOL_ARRAY)
					{
						result = this.BinDecode_BOOL_ARRAY(buffer);
					}
					else
					{
						if (sFSDataType == AWDataType.BYTE)
						{
							result = this.BinDecode_BYTE(buffer);
						}
						else
						{
							if (sFSDataType == AWDataType.BYTE_ARRAY)
							{
								result = this.BinDecode_BYTE_ARRAY(buffer);
							}
							else
							{
								if (sFSDataType == AWDataType.SHORT)
								{
									result = this.BinDecode_SHORT(buffer);
								}
								else
								{
									if (sFSDataType == AWDataType.SHORT_ARRAY)
									{
										result = this.BinDecode_SHORT_ARRAY(buffer);
									}
									else
									{
										if (sFSDataType == AWDataType.INT)
										{
											result = this.BinDecode_INT(buffer);
										}
										else
										{
											if (sFSDataType == AWDataType.INT_ARRAY)
											{
												result = this.BinDecode_INT_ARRAY(buffer);
											}
											else
											{
												if (sFSDataType == AWDataType.LONG)
												{
													result = this.BinDecode_LONG(buffer);
												}
												else
												{
													if (sFSDataType == AWDataType.LONG_ARRAY)
													{
														result = this.BinDecode_LONG_ARRAY(buffer);
													}
													else
													{
														if (sFSDataType == AWDataType.FLOAT)
														{
															result = this.BinDecode_FLOAT(buffer);
														}
														else
														{
															if (sFSDataType == AWDataType.FLOAT_ARRAY)
															{
																result = this.BinDecode_FLOAT_ARRAY(buffer);
															}
															else
															{
																if (sFSDataType == AWDataType.DOUBLE)
																{
																	result = this.BinDecode_DOUBLE(buffer);
																}
																else
																{
																	if (sFSDataType == AWDataType.DOUBLE_ARRAY)
																	{
																		result = this.BinDecode_DOUBLE_ARRAY(buffer);
																	}
																	else
																	{
																		if (sFSDataType == AWDataType.UTF_STRING)
																		{
																			result = this.BinDecode_UTF_STRING(buffer);
																		}
																		else
																		{
																			if (sFSDataType == AWDataType.UTF_STRING_ARRAY)
																			{
																				result = this.BinDecode_UTF_STRING_ARRAY(buffer);
																			}
																			else
																			{
																				if (sFSDataType == AWDataType.SFS_ARRAY)
																				{
																					buffer.Position--;
																					result = new AWDataWrapper(17, this.DecodeSFSArray(buffer));
																				}
																				else
																				{
																					if (sFSDataType != AWDataType.SFS_OBJECT)
																					{
																						throw new Exception("Unknow AWDataType ID: " + sFSDataType);
																					}
																					buffer.Position--;
																					IAWObject iSFSObject = this.DecodeSFSObject(buffer);
																					byte type = Convert.ToByte(18);
																					object data = iSFSObject;
																					if (iSFSObject.ContainsKey(DefaultAWDataSerializer.CLASS_MARKER_KEY) && iSFSObject.ContainsKey(DefaultAWDataSerializer.CLASS_FIELDS_KEY))
																					{
																						type = Convert.ToByte(19);
																						data = this.Sfs2Cs(iSFSObject);
																					}
																					result = new AWDataWrapper((int)type, data);
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}
		private ByteArray EncodeObject(ByteArray buffer, int typeId, object data)
		{
			switch (typeId)
			{
			case 0:
				buffer = this.BinEncode_NULL(buffer);
				break;
			case 1:
				buffer = this.BinEncode_BOOL(buffer, (bool)data);
				break;
			case 2:
				buffer = this.BinEncode_BYTE(buffer, (byte)data);
				break;
			case 3:
				buffer = this.BinEncode_SHORT(buffer, (short)data);
				break;
			case 4:
				buffer = this.BinEncode_INT(buffer, (int)data);
				break;
			case 5:
				buffer = this.BinEncode_LONG(buffer, (long)data);
				break;
			case 6:
				buffer = this.BinEncode_FLOAT(buffer, (float)data);
				break;
			case 7:
				buffer = this.BinEncode_DOUBLE(buffer, (double)data);
				break;
			case 8:
				buffer = this.BinEncode_UTF_STRING(buffer, (string)data);
				break;
			case 9:
				buffer = this.BinEncode_BOOL_ARRAY(buffer, (bool[])data);
				break;
			case 10:
				buffer = this.BinEncode_BYTE_ARRAY(buffer, (ByteArray)data);
				break;
			case 11:
				buffer = this.BinEncode_SHORT_ARRAY(buffer, (short[])data);
				break;
			case 12:
				buffer = this.BinEncode_INT_ARRAY(buffer, (int[])data);
				break;
			case 13:
				buffer = this.BinEncode_LONG_ARRAY(buffer, (long[])data);
				break;
			case 14:
				buffer = this.BinEncode_FLOAT_ARRAY(buffer, (float[])data);
				break;
			case 15:
				buffer = this.BinEncode_DOUBLE_ARRAY(buffer, (double[])data);
				break;
			case 16:
				buffer = this.BinEncode_UTF_STRING_ARRAY(buffer, (string[])data);
				break;
			case 17:
				buffer = this.AddData(buffer, this.Array2Binary((IAWArray)data));
				break;
			case 18:
				buffer = this.AddData(buffer, this.Object2Binary((AWObject)data));
				break;
			case 19:
				buffer = this.AddData(buffer, this.Object2Binary(this.Cs2Sfs(data)));
				break;
			default:
				throw new AWCodecError("Unrecognized type in AWObject serialization: " + typeId);
			}
			return buffer;
		}
		private AWDataWrapper BinDecode_NULL(ByteArray buffer)
		{
			return new AWDataWrapper(AWDataType.NULL, null);
		}
		private AWDataWrapper BinDecode_BOOL(ByteArray buffer)
		{
			return new AWDataWrapper(AWDataType.BOOL, buffer.ReadBool());
		}
		private AWDataWrapper BinDecode_BYTE(ByteArray buffer)
		{
			return new AWDataWrapper(AWDataType.BYTE, buffer.ReadByte());
		}
		private AWDataWrapper BinDecode_SHORT(ByteArray buffer)
		{
			return new AWDataWrapper(AWDataType.SHORT, buffer.ReadShort());
		}
		private AWDataWrapper BinDecode_INT(ByteArray buffer)
		{
			return new AWDataWrapper(AWDataType.INT, buffer.ReadInt());
		}
		private AWDataWrapper BinDecode_LONG(ByteArray buffer)
		{
			return new AWDataWrapper(AWDataType.LONG, buffer.ReadLong());
		}
		private AWDataWrapper BinDecode_FLOAT(ByteArray buffer)
		{
			return new AWDataWrapper(AWDataType.FLOAT, buffer.ReadFloat());
		}
		private AWDataWrapper BinDecode_DOUBLE(ByteArray buffer)
		{
			return new AWDataWrapper(AWDataType.DOUBLE, buffer.ReadDouble());
		}
		private AWDataWrapper BinDecode_UTF_STRING(ByteArray buffer)
		{
			return new AWDataWrapper(AWDataType.UTF_STRING, buffer.ReadUTF());
		}
		private AWDataWrapper BinDecode_BOOL_ARRAY(ByteArray buffer)
		{
			int typedArraySize = this.GetTypedArraySize(buffer);
			bool[] array = new bool[typedArraySize];
			for (int i = 0; i < typedArraySize; i++)
			{
				array[i] = buffer.ReadBool();
			}
			return new AWDataWrapper(AWDataType.BOOL_ARRAY, array);
		}
		private AWDataWrapper BinDecode_BYTE_ARRAY(ByteArray buffer)
		{
			int num = buffer.ReadInt();
			if (num < 0)
			{
				throw new AWCodecError("Array negative size: " + num);
			}
			ByteArray data = new ByteArray(buffer.ReadBytes(num));
			return new AWDataWrapper(AWDataType.BYTE_ARRAY, data);
		}
		private AWDataWrapper BinDecode_SHORT_ARRAY(ByteArray buffer)
		{
			int typedArraySize = this.GetTypedArraySize(buffer);
			short[] array = new short[typedArraySize];
			for (int i = 0; i < typedArraySize; i++)
			{
				array[i] = buffer.ReadShort();
			}
			return new AWDataWrapper(AWDataType.SHORT_ARRAY, array);
		}
		private AWDataWrapper BinDecode_INT_ARRAY(ByteArray buffer)
		{
			int typedArraySize = this.GetTypedArraySize(buffer);
			int[] array = new int[typedArraySize];
			for (int i = 0; i < typedArraySize; i++)
			{
				array[i] = buffer.ReadInt();
			}
			return new AWDataWrapper(AWDataType.INT_ARRAY, array);
		}
		private AWDataWrapper BinDecode_LONG_ARRAY(ByteArray buffer)
		{
			int typedArraySize = this.GetTypedArraySize(buffer);
			long[] array = new long[typedArraySize];
			for (int i = 0; i < typedArraySize; i++)
			{
				array[i] = buffer.ReadLong();
			}
			return new AWDataWrapper(AWDataType.LONG_ARRAY, array);
		}
		private AWDataWrapper BinDecode_FLOAT_ARRAY(ByteArray buffer)
		{
			int typedArraySize = this.GetTypedArraySize(buffer);
			float[] array = new float[typedArraySize];
			for (int i = 0; i < typedArraySize; i++)
			{
				array[i] = buffer.ReadFloat();
			}
			return new AWDataWrapper(AWDataType.FLOAT_ARRAY, array);
		}
		private AWDataWrapper BinDecode_DOUBLE_ARRAY(ByteArray buffer)
		{
			int typedArraySize = this.GetTypedArraySize(buffer);
			double[] array = new double[typedArraySize];
			for (int i = 0; i < typedArraySize; i++)
			{
				array[i] = buffer.ReadDouble();
			}
			return new AWDataWrapper(AWDataType.DOUBLE_ARRAY, array);
		}
		private AWDataWrapper BinDecode_UTF_STRING_ARRAY(ByteArray buffer)
		{
			int typedArraySize = this.GetTypedArraySize(buffer);
			string[] array = new string[typedArraySize];
			for (int i = 0; i < typedArraySize; i++)
			{
				array[i] = buffer.ReadUTF();
			}
			return new AWDataWrapper(AWDataType.UTF_STRING_ARRAY, array);
		}
		private int GetTypedArraySize(ByteArray buffer)
		{
			short num = buffer.ReadShort();
			if (num < 0)
			{
				throw new AWCodecError("Array negative size: " + num);
			}
			return (int)num;
		}
		private ByteArray BinEncode_NULL(ByteArray buffer)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.NULL);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_BOOL(ByteArray buffer, bool val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.BOOL);
			byteArray.WriteBool(val);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_BYTE(ByteArray buffer, byte val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.BYTE);
			byteArray.WriteByte(val);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_SHORT(ByteArray buffer, short val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.SHORT);
			byteArray.WriteShort(val);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_INT(ByteArray buffer, int val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.INT);
			byteArray.WriteInt(val);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_LONG(ByteArray buffer, long val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.LONG);
			byteArray.WriteLong(val);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_FLOAT(ByteArray buffer, float val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.FLOAT);
			byteArray.WriteFloat(val);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_DOUBLE(ByteArray buffer, double val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.DOUBLE);
			byteArray.WriteDouble(val);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_INT(ByteArray buffer, double val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.DOUBLE);
			byteArray.WriteDouble(val);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_UTF_STRING(ByteArray buffer, string val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.UTF_STRING);
			byteArray.WriteUTF(val);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_BOOL_ARRAY(ByteArray buffer, bool[] val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.BOOL_ARRAY);
			byteArray.WriteShort(Convert.ToInt16(val.Length));
			for (int i = 0; i < val.Length; i++)
			{
				byteArray.WriteBool(val[i]);
			}
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_BYTE_ARRAY(ByteArray buffer, ByteArray val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.BYTE_ARRAY);
			byteArray.WriteInt(val.Length);
			byteArray.WriteBytes(val.Bytes);
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_SHORT_ARRAY(ByteArray buffer, short[] val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.SHORT_ARRAY);
			byteArray.WriteShort(Convert.ToInt16(val.Length));
			for (int i = 0; i < val.Length; i++)
			{
				byteArray.WriteShort(val[i]);
			}
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_INT_ARRAY(ByteArray buffer, int[] val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.INT_ARRAY);
			byteArray.WriteShort(Convert.ToInt16(val.Length));
			for (int i = 0; i < val.Length; i++)
			{
				byteArray.WriteInt(val[i]);
			}
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_LONG_ARRAY(ByteArray buffer, long[] val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.LONG_ARRAY);
			byteArray.WriteShort(Convert.ToInt16(val.Length));
			for (int i = 0; i < val.Length; i++)
			{
				byteArray.WriteLong(val[i]);
			}
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_FLOAT_ARRAY(ByteArray buffer, float[] val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.FLOAT_ARRAY);
			byteArray.WriteShort(Convert.ToInt16(val.Length));
			for (int i = 0; i < val.Length; i++)
			{
				byteArray.WriteFloat(val[i]);
			}
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_DOUBLE_ARRAY(ByteArray buffer, double[] val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.DOUBLE_ARRAY);
			byteArray.WriteShort(Convert.ToInt16(val.Length));
			for (int i = 0; i < val.Length; i++)
			{
				byteArray.WriteDouble(val[i]);
			}
			return this.AddData(buffer, byteArray);
		}
		private ByteArray BinEncode_UTF_STRING_ARRAY(ByteArray buffer, string[] val)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.WriteByte(AWDataType.UTF_STRING_ARRAY);
			byteArray.WriteShort(Convert.ToInt16(val.Length));
			for (int i = 0; i < val.Length; i++)
			{
				byteArray.WriteUTF(val[i]);
			}
			return this.AddData(buffer, byteArray);
		}
		private ByteArray EncodeSFSObjectKey(ByteArray buffer, string val)
		{
			buffer.WriteUTF(val);
			return buffer;
		}
		private ByteArray AddData(ByteArray buffer, ByteArray newData)
		{
			buffer.WriteBytes(newData.Bytes);
			return buffer;
		}
		public IAWObject Cs2Sfs(object csObj)
		{
			IAWObject iSFSObject = AWObject.NewInstance();
			this.ConvertCsObj(csObj, iSFSObject);
			return iSFSObject;
		}
		private void ConvertCsObj(object csObj, IAWObject sfsObj)
		{
			Type type = csObj.GetType();
			string fullName = type.FullName;
			if (!(csObj is SerializableAWType))
			{
				throw new AWCodecError(string.Concat(new object[]
				{
					"Cannot serialize object: ",
					csObj,
					", type: ",
					fullName,
					" -- It doesn't implement the SerializableAWType interface"
				}));
			}
			IAWArray iSFSArray = AWArray.NewInstance();
			sfsObj.PutUtfString(DefaultAWDataSerializer.CLASS_MARKER_KEY, fullName);
			sfsObj.PutSFSArray(DefaultAWDataSerializer.CLASS_FIELDS_KEY, iSFSArray);
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			FieldInfo[] array = fields;
			for (int i = 0; i < array.Length; i++)
			{
				FieldInfo fieldInfo = array[i];
				string name = fieldInfo.Name;
				object value = fieldInfo.GetValue(csObj);
				IAWObject iSFSObject = AWObject.NewInstance();
				AWDataWrapper sFSDataWrapper = this.WrapField(value);
				if (sFSDataWrapper == null)
				{
					throw new AWCodecError(string.Concat(new object[]
					{
						"Cannot serialize field of object: ",
						csObj,
						", field: ",
						name,
						", type: ",
						fieldInfo.GetType().Name,
						" -- unsupported type!"
					}));
				}
				iSFSObject.PutUtfString(DefaultAWDataSerializer.FIELD_NAME_KEY, name);
				iSFSObject.Put(DefaultAWDataSerializer.FIELD_VALUE_KEY, sFSDataWrapper);
				iSFSArray.AddSFSObject(iSFSObject);
			}
		}
		private AWDataWrapper WrapField(object val)
		{
			AWDataWrapper result;
			if (val == null)
			{
				result = new AWDataWrapper(AWDataType.NULL, null);
			}
			else
			{
				AWDataWrapper sFSDataWrapper = null;
				if (val is bool)
				{
					sFSDataWrapper = new AWDataWrapper(AWDataType.BOOL, val);
				}
				else
				{
					if (val is byte)
					{
						sFSDataWrapper = new AWDataWrapper(AWDataType.BYTE, val);
					}
					else
					{
						if (val is short)
						{
							sFSDataWrapper = new AWDataWrapper(AWDataType.SHORT, val);
						}
						else
						{
							if (val is int)
							{
								sFSDataWrapper = new AWDataWrapper(AWDataType.INT, val);
							}
							else
							{
								if (val is long)
								{
									sFSDataWrapper = new AWDataWrapper(AWDataType.LONG, val);
								}
								else
								{
									if (val is float)
									{
										sFSDataWrapper = new AWDataWrapper(AWDataType.FLOAT, val);
									}
									else
									{
										if (val is double)
										{
											sFSDataWrapper = new AWDataWrapper(AWDataType.DOUBLE, val);
										}
										else
										{
											if (val is string)
											{
												sFSDataWrapper = new AWDataWrapper(AWDataType.UTF_STRING, val);
											}
											else
											{
												if (val is ArrayList)
												{
													sFSDataWrapper = new AWDataWrapper(AWDataType.SFS_ARRAY, this.UnrollArray(val as ArrayList));
												}
												else
												{
													if (val is SerializableAWType)
													{
														sFSDataWrapper = new AWDataWrapper(AWDataType.SFS_OBJECT, this.Cs2Sfs(val));
													}
													else
													{
														if (val is Hashtable)
														{
															sFSDataWrapper = new AWDataWrapper(AWDataType.SFS_OBJECT, this.UnrollDictionary(val as Hashtable));
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				result = sFSDataWrapper;
			}
			return result;
		}
		private IAWArray UnrollArray(ArrayList arr)
		{
			IAWArray iSFSArray = AWArray.NewInstance();
			IEnumerator enumerator = arr.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object current = enumerator.Current;
					AWDataWrapper sFSDataWrapper = this.WrapField(current);
					if (sFSDataWrapper == null)
					{
						throw new AWCodecError("Cannot serialize field of array: " + current + " -- unsupported type!");
					}
					iSFSArray.Add(sFSDataWrapper);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return iSFSArray;
		}
		private IAWObject UnrollDictionary(Hashtable dict)
		{
			IAWObject iSFSObject = AWObject.NewInstance();
			IEnumerator enumerator = dict.Keys.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Current;
					AWDataWrapper sFSDataWrapper = this.WrapField(dict[text]);
					if (sFSDataWrapper == null)
					{
						throw new AWCodecError(string.Concat(new object[]
						{
							"Cannot serialize field of dictionary with key: ",
							text,
							", ",
							dict[text],
							" -- unsupported type!"
						}));
					}
					iSFSObject.Put(text, sFSDataWrapper);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return iSFSObject;
		}
		public object Sfs2Cs(IAWObject sfsObj)
		{
			if (!sfsObj.ContainsKey(DefaultAWDataSerializer.CLASS_MARKER_KEY) || !sfsObj.ContainsKey(DefaultAWDataSerializer.CLASS_FIELDS_KEY))
			{
				throw new AWCodecError("The AWObject passed does not represent any serialized class.");
			}
			string utfString = sfsObj.GetUtfString(DefaultAWDataSerializer.CLASS_MARKER_KEY);
			Type type;
			if (DefaultAWDataSerializer.runningAssembly == null)
			{
				type = Type.GetType(utfString);
			}
			else
			{
				type = DefaultAWDataSerializer.runningAssembly.GetType(utfString);
			}
			if (type == null)
			{
				throw new AWCodecError("Cannot find type: " + utfString);
			}
			object obj = Activator.CreateInstance(type);
			if (!(obj is SerializableAWType))
			{
				throw new AWCodecError(string.Concat(new object[]
				{
					"Cannot deserialize object: ",
					obj,
					", type: ",
					utfString,
					" -- It doesn't implement the SerializableAWType interface"
				}));
			}
			this.ConvertSFSObject(sfsObj.GetSFSArray(DefaultAWDataSerializer.CLASS_FIELDS_KEY), obj, type);
			return obj;
		}
		private void ConvertSFSObject(IAWArray fieldList, object csObj, Type objType)
		{
			for (int i = 0; i < fieldList.Size(); i++)
			{
				IAWObject sFSObject = fieldList.GetAWObject(i);
				string utfString = sFSObject.GetUtfString(DefaultAWDataSerializer.FIELD_NAME_KEY);
				AWDataWrapper data = sFSObject.GetData(DefaultAWDataSerializer.FIELD_VALUE_KEY);
				object value = this.UnwrapField(data);
				FieldInfo field = objType.GetField(utfString, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null)
				{
					field.SetValue(csObj, value);
				}
			}
		}
		private object UnwrapField(AWDataWrapper wrapper)
		{
			object result = null;
			int type = wrapper.Type;
			if (type <= 8)
			{
				result = wrapper.Data;
			}
			else
			{
				if (type == 17)
				{
					result = this.RebuildArray(wrapper.Data as IAWArray);
				}
				else
				{
					if (type == 18)
					{
						IAWObject iSFSObject = wrapper.Data as IAWObject;
						if (iSFSObject.ContainsKey(DefaultAWDataSerializer.CLASS_MARKER_KEY) && iSFSObject.ContainsKey(DefaultAWDataSerializer.CLASS_FIELDS_KEY))
						{
							result = this.Sfs2Cs(iSFSObject);
						}
						else
						{
							result = this.RebuildDict(wrapper.Data as IAWObject);
						}
					}
					else
					{
						if (type == 19)
						{
							result = wrapper.Data;
						}
					}
				}
			}
			return result;
		}
		private ArrayList RebuildArray(IAWArray sfsArr)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < sfsArr.Size(); i++)
			{
				arrayList.Add(this.UnwrapField(sfsArr.GetWrappedElementAt(i)));
			}
			return arrayList;
		}
		private Hashtable RebuildDict(IAWObject sfsObj)
		{
			Hashtable hashtable = new Hashtable();
			string[] keys = sfsObj.GetKeys();
			for (int i = 0; i < keys.Length; i++)
			{
				string key = keys[i];
				hashtable[key] = this.UnwrapField(sfsObj.GetData(key));
			}
			return hashtable;
		}
	}
}
