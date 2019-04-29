using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NppNetInf
{
    public class ClikeStringArray : IDisposable
    {
        private IntPtr _nativeArray;
        private List<IntPtr> _nativeItems;
        private bool _disposed;

        public ClikeStringArray(int num, int stringCapacity)
        {
            _nativeArray = Marshal.AllocHGlobal((num + 1) * IntPtr.Size);
            _nativeItems = new List<IntPtr>();
            for (int i = 0; i < num; i++)
            {
                IntPtr item = Marshal.AllocHGlobal(stringCapacity);
                Marshal.WriteIntPtr((IntPtr)((int)_nativeArray + i * IntPtr.Size), item);
                _nativeItems.Add(item);
            }
            Marshal.WriteIntPtr((IntPtr)((int)_nativeArray + num * IntPtr.Size), IntPtr.Zero);
        }

        public ClikeStringArray(List<string> lstStrings)
        {
            _nativeArray = Marshal.AllocHGlobal((lstStrings.Count + 1) * IntPtr.Size);
            _nativeItems = new List<IntPtr>();
            for (int i = 0; i < lstStrings.Count; i++)
            {
                IntPtr item = Marshal.StringToHGlobalUni(lstStrings[i]);
                Marshal.WriteIntPtr((IntPtr)((int)_nativeArray + i * IntPtr.Size), item);
                _nativeItems.Add(item);
            }
            Marshal.WriteIntPtr((IntPtr)((int)_nativeArray + lstStrings.Count * IntPtr.Size), IntPtr.Zero);
        }

        public IntPtr NativePointer => _nativeArray;

        public List<string> ManagedStringsAnsi => _getManagedItems(false);

        public List<string> ManagedStringsUnicode => _getManagedItems(true);

        private List<string> _getManagedItems(bool unicode)
        {
            List<string> managedItems = new List<string>();
            for (int i = 0; i < _nativeItems.Count; i++)
            {
                if (unicode) managedItems.Add(Marshal.PtrToStringUni(_nativeItems[i]));
                else managedItems.Add(Marshal.PtrToStringAnsi(_nativeItems[i]));
            }
            return managedItems;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                for (int i = 0; i < _nativeItems.Count; i++)
                    if (_nativeItems[i] != IntPtr.Zero) Marshal.FreeHGlobal(_nativeItems[i]);
                if (_nativeArray != IntPtr.Zero)
                    Marshal.FreeHGlobal(_nativeArray);
                _disposed = true;
            }
        }

        ~ClikeStringArray()
        {
            Dispose();
        }
    }
}