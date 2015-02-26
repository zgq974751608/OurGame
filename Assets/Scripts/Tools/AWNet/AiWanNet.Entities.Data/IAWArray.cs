using AiWanNet.Util;
using System;
using System.Collections;
namespace AiWanNet.Entities.Data
{
	public interface IAWArray : ICollection, IEnumerable
	{
		bool Contains(object obj);
		object GetElementAt(int index);
		AWDataWrapper GetWrappedElementAt(int index);
		object RemoveElementAt(int index);
		int Size();
		ByteArray ToBinary();
		string GetDump(bool format);
		string GetDump();
		string GetHexDump();
		void AddNull();
		void AddBool(bool val);
		void AddByte(byte val);
		void AddShort(short val);
		void AddInt(int val);
		void AddLong(long val);
		void AddFloat(float val);
		void AddDouble(double val);
		void AddUtfString(string val);
		void AddBoolArray(bool[] val);
		void AddByteArray(ByteArray val);
		void AddShortArray(short[] val);
		void AddIntArray(int[] val);
		void AddLongArray(long[] val);
		void AddFloatArray(float[] val);
		void AddDoubleArray(double[] val);
		void AddUtfStringArray(string[] val);
		void AddSFSArray(IAWArray val);
		void AddSFSObject(IAWObject val);
		void AddClass(object val);
		void Add(AWDataWrapper val);
		bool IsNull(int index);
		bool GetBool(int index);
		byte GetByte(int index);
		short GetShort(int index);
		int GetInt(int index);
		long GetLong(int index);
		float GetFloat(int index);
		double GetDouble(int index);
		string GetUtfString(int index);
		bool[] GetBoolArray(int index);
		ByteArray GetByteArray(int index);
		short[] GetShortArray(int index);
		int[] GetIntArray(int index);
		long[] GetLongArray(int index);
		float[] GetFloatArray(int index);
		double[] GetDoubleArray(int index);
		string[] GetUtfStringArray(int index);
		IAWArray GetAWArray(int index);
		IAWObject GetAWObject(int index);
		object GetClass(int index);
	}
}
