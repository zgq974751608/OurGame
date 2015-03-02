using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

public class DispatchEventArgs : EventArgs
{
    public readonly int type;
    public object[] data;

	public DispatchEventArgs(int type, params object[] data)
    {
        this.type = type;
        this.data = data;
    }
    public DispatchEventArgs()
    {

    }
}

public class MGEventArgs<T> where T : struct{

}
