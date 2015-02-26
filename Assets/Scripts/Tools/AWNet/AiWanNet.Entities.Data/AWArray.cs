using AiWanNet.Exceptions;
using AiWanNet.Protocol.Serialization;
using AiWanNet.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace AiWanNet.Entities.Data
{
	public class AWArray : IAWArray, ICollection, IEnumerable
	{
        private IAWDataSerializer serializer;
		private List<AWDataWrapper> dataHolder;
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}
		int ICollection.Count
		{
			get
			{
				return this.dataHolder.Count;
			}
		}
		void ICollection.CopyTo(Array toArray, int index)
		{
			foreach (AWDataWrapper current in this.dataHolder)
			{
				toArray.SetValue(current, index);
				index++;
			}
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AWArrayEnumerator(this.dataHolder);
		}
		public static AWArray NewFromBinaryData(ByteArray ba)
		{
			return DefaultAWDataSerializer.Instance.Binary2Array(ba) as AWArray;
		}
		public static AWArray NewInstance()
		{
			return new AWArray();
		}
		public AWArray()
		{
			this.dataHolder = new List<AWDataWrapper>();
			this.serializer = DefaultAWDataSerializer.Instance;
		}
		public bool Contains(object obj)
		{
			if (obj is IAWArray || obj is IAWObject)
			{
				throw new AWError("IAWArray and IAWObject are not supported by this method.");
			}
			bool result;
			for (int i = 0; i < this.Size(); i++)
			{
				object elementAt = this.GetElementAt(i);
				if (object.Equals(elementAt, obj))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public AWDataWrapper GetWrappedElementAt(int index)
		{
			return this.dataHolder[index];
		}
		public object GetElementAt(int index)
		{
			object result = null;
			if (this.dataHolder[index] != null)
			{
				result = this.dataHolder[index].Data;
			}
			return result;
		}
		public object RemoveElementAt(int index)
		{
			object result;
			if (index >= this.dataHolder.Count)
			{
				result = null;
			}
			else
			{
				AWDataWrapper sFSDataWrapper = this.dataHolder[index];
				this.dataHolder.RemoveAt(index);
				result = sFSDataWrapper.Data;
			}
			return result;
		}
		public int Size()
		{
			return this.dataHolder.Count;
		}
		public ByteArray ToBinary()
		{
			return this.serializer.Array2Binary(this);
		}
		public string GetDump()
		{
			return this.GetDump(true);
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
		private string Dump()
		{
			StringBuilder stringBuilder = new StringBuilder(Convert.ToString(DefaultObjectDumpFormatter.TOKEN_INDENT_OPEN));
			for (int i = 0; i < this.dataHolder.Count; i++)
			{
				AWDataWrapper sFSDataWrapper = this.dataHolder[i];
				int type = sFSDataWrapper.Type;
				string value;
				if (type == 18)
				{
					value = (sFSDataWrapper.Data as AWObject).GetDump(false);
				}
				else
				{
					if (type == 17)
					{
						value = (sFSDataWrapper.Data as AWArray).GetDump(false);
					}
					else
					{
						if (type == 0)
						{
							value = "NULL";
						}
						else
						{
							if (type > 8 && type < 19)
							{
								value = "[" + sFSDataWrapper.Data + "]";
							}
							else
							{
								value = sFSDataWrapper.Data.ToString();
							}
						}
					}
				}
				stringBuilder.Append("(" + ((AWDataType)type).ToString().ToLower() + ") ");
				stringBuilder.Append(value);
				stringBuilder.Append(Convert.ToString(DefaultObjectDumpFormatter.TOKEN_DIVIDER));
			}
			string text = stringBuilder.ToString();
			if (this.Size() > 0)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text + Convert.ToString(DefaultObjectDumpFormatter.TOKEN_INDENT_CLOSE);
		}
		public string GetHexDump()
		{
			return DefaultObjectDumpFormatter.HexDump(this.ToBinary());
		}
		public void AddNull()
		{
			this.AddObject(null, AWDataType.NULL);
		}
		public void AddBool(bool val)
		{
			this.AddObject(val, AWDataType.BOOL);
		}
		public void AddByte(byte val)
		{
			this.AddObject(val, AWDataType.BYTE);
		}
		public void AddShort(short val)
		{
			this.AddObject(val, AWDataType.SHORT);
		}
		public void AddInt(int val)
		{
			this.AddObject(val, AWDataType.INT);
		}
		public void AddLong(long val)
		{
			this.AddObject(val, AWDataType.LONG);
		}
		public void AddFloat(float val)
		{
			this.AddObject(val, AWDataType.FLOAT);
		}
		public void AddDouble(double val)
		{
			this.AddObject(val, AWDataType.DOUBLE);
		}
		public void AddUtfString(string val)
		{
			this.AddObject(val, AWDataType.UTF_STRING);
		}
		public void AddBoolArray(bool[] val)
		{
			this.AddObject(val, AWDataType.BOOL_ARRAY);
		}
		public void AddByteArray(ByteArray val)
		{
			this.AddObject(val, AWDataType.BYTE_ARRAY);
		}
		public void AddShortArray(short[] val)
		{
			this.AddObject(val, AWDataType.SHORT_ARRAY);
		}
		public void AddIntArray(int[] val)
		{
			this.AddObject(val, AWDataType.INT_ARRAY);
		}
		public void AddLongArray(long[] val)
		{
			this.AddObject(val, AWDataType.LONG_ARRAY);
		}
		public void AddFloatArray(float[] val)
		{
			this.AddObject(val, AWDataType.FLOAT_ARRAY);
		}
		public void AddDoubleArray(double[] val)
		{
			this.AddObject(val, AWDataType.DOUBLE_ARRAY);
		}
		public void AddUtfStringArray(string[] val)
		{
			this.AddObject(val, AWDataType.UTF_STRING_ARRAY);
		}
		public void AddSFSArray(IAWArray val)
		{
			this.AddObject(val, AWDataType.SFS_ARRAY);
		}
		public void AddSFSObject(IAWObject val)
		{
			this.AddObject(val, AWDataType.SFS_OBJECT);
		}
		public void AddClass(object val)
		{
			this.AddObject(val, AWDataType.CLASS);
		}
		public void Add(AWDataWrapper wrappedObject)
		{
			this.dataHolder.Add(wrappedObject);
		}
		private void AddObject(object val, AWDataType tp)
		{
			this.Add(new AWDataWrapper((int)tp, val));
		}
		public bool IsNull(int index)
		{
			bool result;
			if (index >= this.dataHolder.Count)
			{
				result = true;
			}
			else
			{
				AWDataWrapper sFSDataWrapper = this.dataHolder[index];
				result = (sFSDataWrapper.Type == 0);
			}
			return result;
		}
		public T GetValue<T>(int index)
		{
			T result;
			if (index >= this.dataHolder.Count)
			{
				result = default(T);
			}
			else
			{
				AWDataWrapper sFSDataWrapper = this.dataHolder[index];
				result = (T)((object)sFSDataWrapper.Data);
			}
			return result;
		}
		public bool GetBool(int index)
		{
			return this.GetValue<bool>(index);
		}
		public byte GetByte(int index)
		{
			return this.GetValue<byte>(index);
		}
		public short GetShort(int index)
		{
			return this.GetValue<short>(index);
		}
		public int GetInt(int index)
		{
			return this.GetValue<int>(index);
		}
		public long GetLong(int index)
		{
			return this.GetValue<long>(index);
		}
		public float GetFloat(int index)
		{
			return this.GetValue<float>(index);
		}
		public double GetDouble(int index)
		{
			return this.GetValue<double>(index);
		}
		public string GetUtfString(int index)
		{
			return this.GetValue<string>(index);
		}
		private ICollection GetArray(int index)
		{
			return this.GetValue<ICollection>(index);
		}
		public bool[] GetBoolArray(int index)
		{
			return (bool[])this.GetArray(index);
		}
		public ByteArray GetByteArray(int index)
		{
			return this.GetValue<ByteArray>(index);
		}
		public short[] GetShortArray(int index)
		{
			return (short[])this.GetArray(index);
		}
		public int[] GetIntArray(int index)
		{
			return (int[])this.GetArray(index);
		}
		public long[] GetLongArray(int index)
		{
			return (long[])this.GetArray(index);
		}
		public float[] GetFloatArray(int index)
		{
			return (float[])this.GetArray(index);
		}
		public double[] GetDoubleArray(int index)
		{
			return (double[])this.GetArray(index);
		}
		public string[] GetUtfStringArray(int index)
		{
			return (string[])this.GetArray(index);
		}
		public IAWArray GetAWArray(int index)
		{
			return this.GetValue<IAWArray>(index);
		}
		public object GetClass(int index)
		{
			AWDataWrapper sFSDataWrapper = this.dataHolder[index];
			return (sFSDataWrapper == null) ? null : sFSDataWrapper.Data;
		}
		public IAWObject GetAWObject(int index)
		{
			return this.GetValue<IAWObject>(index);
		}
	}
}
