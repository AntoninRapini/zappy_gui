using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_connect
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_disconnect
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_hello
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_msz
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint X;
    public uint Y;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_bct
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint X;
    public uint Y;
    public uint Q0;
    public uint Q1;
    public uint Q2;
    public uint Q3;
    public uint Q4;
    public uint Q5;
    public uint Q6;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_mct
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint X;
    public uint Y;
    public uint Q0;
    public uint Q1;
    public uint Q2;
    public uint Q3;
    public uint Q4;
    public uint Q5;
    public uint Q6;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_tna
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
    public string teamNames;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_ppo
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
    public uint X;
    public uint Y;
    public uint Direction;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_plv
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
    public uint Level;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pin
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
    public uint X;
    public uint Y;
    public uint Q0;
    public uint Q1;
    public uint Q2;
    public uint Q3;
    public uint Q4;
    public uint Q5;
    public uint Q6;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pnw
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
    public uint X;
    public uint Y;
    public uint Direction;
    public uint Level;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
    public string TeamName;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pex
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pbc
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
    public string Message;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_smg
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
    public string Message;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pic
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint X;
    public uint Y;
    public uint Level;
    public uint PlayerCount;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public uint[] Ids;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pfk
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pie
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint X;
    public uint Y;
    public uint Level;
    public uint PlayerCount;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public uint[] Ids;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_enw
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint EggId;
    public uint PlayerId;
    public uint X;
    public uint Y;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pdr
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
    public uint ResourceId;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pgt
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
    public uint ResourceId;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_pdi
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_eht
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint Id;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_edi
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public uint Eggid;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_sgt
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint TimeUnit;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_sst
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    public uint TimeUnit;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_seg
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
    public string WinningTeamName;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_suc
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public unsafe struct Msg_sbp
{
    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Header;
}