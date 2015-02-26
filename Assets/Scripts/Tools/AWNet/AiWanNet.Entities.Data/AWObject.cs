using AiWanNet.Protocol.Serialization;
using AiWanNet.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace AiWanNet.Entities.Data
{
	public class AWObject : IAWObject
	{
		private Dictionary<string, AWDataWrapper> dataHolder;
        private IAWDataSerializer serializer;
		public static AWObject NewFromBinaryData(ByteArray ba)
		{
			return DefaultAWDataSerializer.Instance.Binary2Object(ba) as AWObject;
		}
		public static AWObject NewInstance()
		{
			return new AWObject();
		}
		public AWObject()
		{
			this.dataHolder = new Dictionary<string, AWDataWrapper>();
			this.serializer = DefaultAWDataSerializer.Instance;
		}
		private string Dump()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Convert.ToString(DefaultObjectDumpFormatter.TOKEN_INDENT_OPEN));
			foreach (KeyValuePair<string, AWDataWrapper> current in this.dataHolder)
			{
				AWDataWrapper value = current.Value;
				string key = current.Key;
				int type = value.Type;
				stringBuilder.Append("(" + ((AWDataType)type).ToString().ToLower() + ")");
				stringBuilder.Append(" " + key + ": ");
				if (type == 18)
				{
					stringBuilder.Append((value.Data as AWObject).GetDump(false));
				}
				else
				{
					if (type == 17)
					{
						stringBuilder.Append((value.Data as AWArray).GetDump(false));
					}
					else
					{
						if (type > 8 && type < 19)
						{
							stringBuilder.Append("[" + value.Data + "]");
						}
						else
						{
							stringBuilder.Append(value.Data);
						}
					}
				}
				stringBuilder.Append(DefaultObjectDumpFormatter.TOKEN_DIVIDER);
			}
			string text = stringBuilder.ToString();
			if (this.Size() > 0)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text + DefaultObjectDumpFormatter.TOKEN_INDENT_CLOSE;
		}
		public AWDataWrapper GetData(string key)
		{
			return this.dataHolder[key];
		}
		public T GetValue<T>(string key)
		{
			T result;
			if (!this.dataHolder.ContainsKey(key))
			{
				result = default(T);
			}
			else
			{
				result = (T)((object)this.dataHolder[key].Data);
			}
			return result;
		}
		public bool GetBool(string key)
		{
			return this.GetValue<bool>(key);
		}
		public byte GetByte(string key)
		{
			return this.GetValue<byte>(key);
		}
		public short GetShort(string key)
		{
			return this.GetValue<short>(key);
		}
		public int GetInt(string key)
		{
			return this.GetValue<int>(key);
		}
		public long GetLong(string key)
		{
			return this.GetValue<long>(key);
		}
		public float GetFloat(string key)
		{
			return this.GetValue<float>(key);
		}
		public double GetDouble(string key)
		{
			return this.GetValue<double>(key);
		}
		public string GetUtfString(string key)
		{
			return this.GetValue<string>(key);
		}
		private ICollection GetArray(string key)
		{
			return this.GetValue<ICollection>(key);
		}
		public bool[] GetBoolArray(string key)
		{
			return (bool[])this.GetArray(key);
		}
		public ByteArray GetByteArray(string key)
		{
			return this.GetValue<ByteArray>(key);
		}
		public short[] GetShortArray(string key)
		{
			return (short[])this.GetArray(key);
		}
		public int[] GetIntArray(string key)
		{
			return (int[])this.GetArray(key);
		}
		public long[] GetLongArray(string key)
		{
			return (long[])this.GetArray(key);
		}
		public float[] GetFloatArray(string key)
		{
			return (float[])this.GetArray(key);
		}
		public double[] GetDoubleArray(string key)
		{
			return (double[])this.GetArray(key);
		}
		public string[] GetUtfStringArray(string key)
		{
			return (string[])this.GetArray(key);
		}
		public IAWArray GetSFSArray(string key)
		{
			return this.GetValue<IAWArray>(key);
		}
		public IAWObject GetSFSObject(string key)
		{
			return this.GetValue<IAWObject>(key);
		}
		public void PutNull(string key)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.NULL, null);
		}
		public void PutBool(string key, bool val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.BOOL, val);
		}
		public void PutByte(string key, byte val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.BYTE, val);
		}
		public void PutShort(string key, short val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.SHORT, val);
		}
		public void PutInt(string key, int val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.INT, val);
		}
		public void PutLong(string key, long val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.LONG, val);
		}
		public void PutFloat(string key, float val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.FLOAT, val);
		}
		public void PutDouble(string key, double val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.DOUBLE, val);
		}
		public void PutUtfString(string key, string val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.UTF_STRING, val);
		}
		public void PutBoolArray(string key, bool[] val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.BOOL_ARRAY, val);
		}
		public void PutByteArray(string key, ByteArray val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.BYTE_ARRAY, val);
		}
		public void PutShortArray(string key, short[] val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.SHORT_ARRAY, val);
		}
		public void PutIntArray(string key, int[] val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.INT_ARRAY, val);
		}
		public void PutLongArray(string key, long[] val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.LONG_ARRAY, val);
		}
		public void PutFloatArray(string key, float[] val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.FLOAT_ARRAY, val);
		}
		public void PutDoubleArray(string key, double[] val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.DOUBLE_ARRAY, val);
		}
		public void PutUtfStringArray(string key, string[] val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.UTF_STRING_ARRAY, val);
		}
		public void PutSFSArray(string key, IAWArray val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.SFS_ARRAY, val);
		}
		public void PutSFSObject(string key, IAWObject val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.SFS_OBJECT, val);
		}
		public void Put(string key, AWDataWrapper val)
		{
			this.dataHolder[key] = val;
		}
		public bool ContainsKey(string key)
		{
			return this.dataHolder.ContainsKey(key);
		}
		public object GetClass(string key)
		{
			AWDataWrapper sFSDataWrapper = this.dataHolder[key];
			object result;
			if (sFSDataWrapper != null)
			{
				result = sFSDataWrapper.Data;
			}
			else
			{
				result = null;
			}
			return result;
		}
		public string GetDump(bool format)
		{
			string result;
			if (!format)
			{
				result = this.Dump();
			}
			else
			{
				result = DefaultObjectDumpFormatter.PrettyPrintDump(this.Dump());
			}
			return result;
		}
		public string GetDump()
		{
			return this.GetDump(true);
		}
		public string GetHexDump()
		{
			return DefaultObjectDumpFormatter.HexDump(this.ToBinary());
		}
		public string[] GetKeys()
		{
			string[] array = new string[this.dataHolder.Keys.Count];
			this.dataHolder.Keys.CopyTo(array, 0);
			return array;
		}
		public bool IsNull(string key)
		{
			return !this.ContainsKey(key) || this.GetData(key).Data == null;
		}
		public void PutClass(string key, object val)
		{
			this.dataHolder[key] = new AWDataWrapper(AWDataType.CLASS, val);
		}
		public void RemoveElement(string key)
		{
			this.dataHolder.Remove(key);
		}
		public int Size()
		{
			return this.dataHolder.Count;
		}
		public ByteArray ToBinary()
		{
			return this.serializer.Object2Binary(this);
		}
	}
}
