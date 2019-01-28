using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using UnityEngine;

#region Handler delegates

/*[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void AddToQueuePtr(IntPtr task);*/

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void OnConnectPacket(uint client_id);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void OnDisconnectPacket(uint client_id);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnHelloPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnMszPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnBctPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnMctPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnTnaPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPpoPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPlvPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPinPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPnwPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPexPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPbcPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnSmgPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPicPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPiePacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPfkPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnEnwPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPdrPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPgtPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnPdiPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnEhtPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnEdiPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnSgtPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnSstPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnSegPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnSucPacket(uint client_id, IntPtr packet);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void OnSbpPacket(uint client_id, IntPtr packet);
#endregion

//déclaration de la structure qui contient tous les prototypes des handlers
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct NetworkHandlers
{
   /* [MarshalAs(UnmanagedType.FunctionPtr)]
    public AddToQueuePtr onAddToQueue;*/
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnConnectPacket onConnect;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnDisconnectPacket onDisconnect;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnHelloPacket onHello;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnMszPacket onMsz;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnBctPacket onBct;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnTnaPacket onTna;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPpoPacket onPpo;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPlvPacket onPlv;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPinPacket onPin;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPnwPacket onPnw;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPexPacket onPex;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPbcPacket onPbc;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnSmgPacket onSmg;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPicPacket onPic;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPiePacket onPie;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPfkPacket onPfk;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnEnwPacket onEnw;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPdrPacket onPdr;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPgtPacket onPgt;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnPdiPacket onPdi;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnEhtPacket onEht;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnEdiPacket onEdi;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnSgtPacket onSgt;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnSstPacket onSst;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnSegPacket onSeg;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnSucPacket onSuc;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public OnSbpPacket onSbp;
};

public static class ServerCommandHandlers
{
    public static string Host = "127.0.0.1";
    public static ushort Port = 4242;
    public static uint ClientId = 0;
    [DllImport("libzappy_network.so", CharSet = CharSet.Unicode)]
    public static extern void send_unwrapped(uint id, byte[] unwrapped);
    //import de la fonction qui permet de te connecter
    [DllImport("libzappy_network.so", CharSet = CharSet.Unicode)]
    unsafe public static extern bool zappy_init_connector(byte[] host, ushort port, bool async, NetworkHandlers handlers, uint timeOut);

    //import de la fonction qui te permet de poll() les events (à foutre dans une boucle inf, genre onUpdate() jsp où sur unity)
    [DllImport("libzappy_network.so", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern void zappy_sync_poll();

    public static void Init()
    {
        Debug.Log("Init with host : " + Host + " and port : " + Port);
        if (zappy_init_connector(Encoding.ASCII.GetBytes(Host), Port, true, netWorkHandler, 5))
        {
            Debug.Log("Connection successful");
        }
        else
        {
            Debug.Log("Failed to connect");
            GameManager.Instance.InterruptGame();
        }
    }

    public static void SendCmd(string cmd)
    {
        if (ClientId != 0)
        {
            send_unwrapped(ClientId, Encoding.ASCII.GetBytes(cmd));
        }
    }

    //association des function pointers à tes handlers
    public static NetworkHandlers netWorkHandler = new NetworkHandlers()
    {
        onConnect = new OnConnectPacket(Handle_connect),
        onDisconnect = new OnDisconnectPacket(Handle_disconnect),
        onHello = new OnHelloPacket(Handle_hello),
        onMsz = new OnMszPacket(Handle_msz),
        onBct = new OnBctPacket(Handle_bct),
        onTna = new OnTnaPacket(Handle_tna),
        onPpo = new OnPpoPacket(Handle_ppo),
        onPlv = new OnPlvPacket(Handle_plv),
        onPin = new OnPinPacket(Handle_pin),
        onPnw = new OnPnwPacket(Handle_pnw),
        onPex = new OnPexPacket(Handle_pex),
        onPbc = new OnPbcPacket(Handle_pbc),
        onSmg = new OnSmgPacket(Handle_smg),
        onPic = new OnPicPacket(Handle_pic),
        onPie = new OnPiePacket(Handle_pie),
        onPfk = new OnPfkPacket(Handle_pfk),
        onEnw = new OnEnwPacket(Handle_enw),
        onPdr = new OnPdrPacket(Handle_pdr),
        onPgt = new OnPgtPacket(Handle_pgt),
        onPdi = new OnPdiPacket(Handle_pdi),
        onEht = new OnEhtPacket(Handle_eht),
        onEdi = new OnEdiPacket(Handle_edi),
        onSgt = new OnSgtPacket(Handle_sgt),
        onSst = new OnSstPacket(Handle_sst),
        onSeg = new OnSegPacket(Handle_seg),
        onSuc = new OnSucPacket(Handle_suc),
        onSbp = new OnSbpPacket(Handle_sbp)
    };

    unsafe static void Handle_hello(uint client_id, IntPtr msgptr)
    {
        Debug.Log("Received Hello msg !!");
        SendCmd("WELCOME");
        GameManager.Instance.Connected();
    }

    static void Handle_connect(uint client_id)
    {
        Debug.Log("Received connect msg !!");
        ClientId = client_id;
    }

    static void Handle_disconnect(uint client_id)
    {
        Debug.Log("Received disconnect msg !!");
        GameManager.Instance.InterruptGame();
    }

    unsafe static void Handle_msz(uint client_id, IntPtr msgptr)
    {
        Debug.Log("Received map size msg :");
        Msg_msz msg = (Msg_msz)Marshal.PtrToStructure(msgptr, typeof(Msg_msz));
        Debug.Log("Map created with size X = " + msg.X + " Y = " + msg.Y);
        GameManager.Instance.InitMap(new Vector2(msg.X, msg.Y));
    }

    unsafe static void Handle_bct(uint client_id, IntPtr msgptr)
    {
        Debug.Log("Received tile msg :");
        Msg_bct msg = (Msg_bct)Marshal.PtrToStructure(msgptr, typeof(Msg_bct));
        Debug.Log("Tile (" + msg.X + "," + msg.Y + ") updated with items" + +msg.Q0 + " , " + msg.Q1 + " , " + msg.Q2 + " , " + msg.Q3 + " , " + msg.Q4 + " , " + msg.Q5 + " , " + msg.Q6);
        GameManager.Instance.Map.SetTile(new Vector2(msg.X, msg.Y), msg.Q0, msg.Q1, msg.Q2, msg.Q3, msg.Q4, msg.Q5, msg.Q6);
        Debug.Log("Ending bct handler");
    }

    unsafe static void Handle_mct(uint client_id, IntPtr msgptr)
    {
        Debug.Log("Received tiles msg :");
        Msg_mct msg = (Msg_mct)Marshal.PtrToStructure(msgptr, typeof(Msg_mct));
        Debug.Log("Tile (" + msg.X + "," + msg.Y + ") updated with items" + +msg.Q0 + " , " + msg.Q1 + " , " + msg.Q2 + " , " + msg.Q3 + " , " + msg.Q4 + " , " + msg.Q5 + " , " + msg.Q6);
        GameManager.Instance.Map.SetTile(new Vector2(msg.X, msg.Y), msg.Q0, msg.Q1, msg.Q2, msg.Q3, msg.Q4, msg.Q5, msg.Q6);
    }

    unsafe static void Handle_tna(uint client_id, IntPtr msgptr)
    {
        Debug.Log("Received team names msg :");
        Msg_tna msg = (Msg_tna)Marshal.PtrToStructure(msgptr, typeof(Msg_tna));
        Debug.Log("Team " + msg.teamNames + " initialized");
        GameManager.Instance.Teams[msg.teamNames] = new Team(GameManager.Instance.Teams.Count, msg.teamNames);
    }

    unsafe static void Handle_pnw(uint client_id, IntPtr msgptr)
    {
        Debug.Log("Received new player msg :");
        Msg_pnw msg = (Msg_pnw)Marshal.PtrToStructure(msgptr, typeof(Msg_pnw));
        Debug.Log("New player with id " + msg.Id + " in team " + msg.TeamName + " connected");
        GameManager.Instance.AddPlayer(msg.Id, msg.TeamName, msg.Level, new Vector2(msg.X, msg.Y), (Player.Orientation)msg.Direction);
        Debug.Log("Ending pnw handler");
    }

    unsafe static void Handle_ppo(uint client_id, IntPtr msgptr)
    {
        Debug.Log("Received player update msg :");
        Msg_ppo msg = (Msg_ppo)Marshal.PtrToStructure(msgptr, typeof(Msg_ppo));
        if (GameManager.Instance.Players.ContainsKey(msg.Id))
        {
            Debug.Log("Updated player " + msg.Id + ", position : (" + msg.X + "," + msg.Y + "), direction : " + ((Player.Orientation)msg.Direction).ToString());
            GameManager.Instance.Players[msg.Id].Position = new Vector2(msg.X, msg.Y);
            GameManager.Instance.Players[msg.Id].Direction = (Player.Orientation)msg.Direction;
        }
        else
            Debug.Log("Player with id " + msg.Id + " not found");
    }

    unsafe static void Handle_plv(uint client_id, IntPtr msgptr)
    {
        Msg_plv msg = (Msg_plv)Marshal.PtrToStructure(msgptr, typeof(Msg_plv));
        if (GameManager.Instance.Players.ContainsKey(msg.Id))
        {
            Debug.Log("Updated player " + msg.Id + ", level : " + msg.Level);
            GameManager.Instance.Players[msg.Id].Level = msg.Level;
        }
        else
            Debug.Log("Player with id " + msg.Id + " not found");
    }

    unsafe static void Handle_pin(uint client_id, IntPtr msgptr)
    {
        Msg_pin msg = (Msg_pin)Marshal.PtrToStructure(msgptr, typeof(Msg_pin));
        if (GameManager.Instance.Players.ContainsKey(msg.Id))
        {
            Debug.Log("Updated player " + msg.Id + ", inventory : " + msg.Q0 + " , " + msg.Q1 + " , " + msg.Q2 + " , " + msg.Q3 + " , " + msg.Q4 + " , " + msg.Q5 + " , " + msg.Q6);
            GameManager.Instance.Players[msg.Id].Inventory = new Inventory(msg.Q0, msg.Q1, msg.Q2, msg.Q3, msg.Q4, msg.Q5, msg.Q6);
        }
        else
            Debug.Log("Player with id " + msg.Id + " not found");
    }

    unsafe static void Handle_pex(uint client_id, IntPtr msgptr)
    {
        Msg_pex msg = (Msg_pex)Marshal.PtrToStructure(msgptr, typeof(Msg_pex));
        if (GameManager.Instance.Players.ContainsKey(msg.Id))
        {
            Debug.Log("Player " + msg.Id + " eject !");
            GameManager.Instance.Players[msg.Id].Eject();
        }
        else
            Debug.Log("Player with id " + msg.Id + " not found");
    }

    unsafe static void Handle_pbc(uint client_id, IntPtr msgptr)
    {
        Msg_pbc msg = (Msg_pbc)Marshal.PtrToStructure(msgptr, typeof(Msg_pbc));
        if (GameManager.Instance.Players.ContainsKey(msg.Id))
        {
            Debug.Log("Player " + msg.Id + " broadcast message : " + msg.Message);
            GameManager.Instance.Players[msg.Id].BroadcastMessage(msg.Message);
        }
        else
            Debug.Log("Player with id " + msg.Id + " not found");
    }

    unsafe static void Handle_smg(uint client_id, IntPtr msgptr)
    {

    }

    unsafe static void Handle_pic(uint client_id, IntPtr msgptr)
    {
        Msg_pic msg = (Msg_pic)Marshal.PtrToStructure(msgptr, typeof(Msg_pic));
        for (int i = 0; i < msg.PlayerCount; i++)
        {
            if (GameManager.Instance.Players.ContainsKey(msg.Ids[i]))
            {
                Debug.Log("Player " + msg.Ids[i] + " as started incantating");
                GameManager.Instance.Players[msg.Ids[i]].StartIncantation();
            }
            else
                Debug.Log("Player with id " + msg.Ids[i] + " not found");
        }

    }

    unsafe static void Handle_pie(uint client_id, IntPtr msgptr)
    {
        Msg_pie msg = (Msg_pie)Marshal.PtrToStructure(msgptr, typeof(Msg_pie));
        for (int i = 0; i < msg.PlayerCount; i++)
        {
            if (GameManager.Instance.Players.ContainsKey(msg.Ids[i]))
            {
                Debug.Log("Player " + msg.Ids[i] + " has stopped incantating");
                GameManager.Instance.Players[msg.Ids[i]].StopIncantation(true);
            }
            else
                Debug.Log("Player with id " + msg.Ids[i] + " not found");
        }
    }

    unsafe static void Handle_pfk(uint client_id, IntPtr msgptr)
    {

    }

    unsafe static void Handle_enw(uint client_id, IntPtr msgptr)
    {
        Msg_enw msg = (Msg_enw)Marshal.PtrToStructure(msgptr, typeof(Msg_enw));
        if (GameManager.Instance.Players.ContainsKey(msg.PlayerId))
        {
            Debug.Log("Player " + msg.PlayerId + " lay egg with id " + msg.EggId);
            GameManager.Instance.Players[msg.PlayerId].LayEgg(msg.EggId);
        }
        else
            Debug.Log("Player with id " + msg.PlayerId + " not found");
    }

    unsafe static void Handle_pdr(uint client_id, IntPtr msgptr)
    {
        Msg_pdr msg = (Msg_pdr)Marshal.PtrToStructure(msgptr, typeof(Msg_pdr));
        if (GameManager.Instance.Players.ContainsKey(msg.Id))
        {
            Debug.Log("Player " + msg.Id + " drop resource with id " + msg.ResourceId);
            GameManager.Instance.Players[msg.Id].DropResource(msg.ResourceId);
        }
        else
            Debug.Log("Player with id " + msg.Id + " not found");
    }

    unsafe static void Handle_pgt(uint client_id, IntPtr msgptr)
    {
        Msg_pgt msg = (Msg_pgt)Marshal.PtrToStructure(msgptr, typeof(Msg_pgt));
        if (GameManager.Instance.Players.ContainsKey(msg.Id))
        {
            Debug.Log("Player " + msg.Id + " get resource with id " + msg.ResourceId);
            GameManager.Instance.Players[msg.Id].GetResource(msg.ResourceId);
        }
        else
            Debug.Log("Player with id " + msg.Id + " not found");
    }

    unsafe static void Handle_pdi(uint client_id, IntPtr msgptr)
    {
        Msg_pdi msg = (Msg_pdi)Marshal.PtrToStructure(msgptr, typeof(Msg_pdi));
        if (GameManager.Instance.Players.ContainsKey(msg.Id))
        {
            Debug.Log("Player " + msg.Id + " death");
            GameManager.Instance.Players[msg.Id].Die();
        }
        else
            Debug.Log("Player with id " + msg.Id + " not found");
    }

    unsafe static void Handle_eht(uint client_id, IntPtr msgptr)
    {
        Msg_eht msg = (Msg_eht)Marshal.PtrToStructure(msgptr, typeof(Msg_eht));
        if (GameManager.Instance.Eggs.ContainsKey(msg.Id))
        {
            Debug.Log("Egg " + msg.Id + " hatching");
            GameManager.Instance.Eggs[msg.Id].Hatch();
        }
        else
            Debug.Log("Egg with id " + msg.Id + " not found");
    }

    unsafe static void Handle_edi(uint client_id, IntPtr msgptr)
    {

    }

    unsafe static void Handle_sgt(uint client_id, IntPtr msgptr)
    {
        Msg_sgt msg = (Msg_sgt)Marshal.PtrToStructure(msgptr, typeof(Msg_sgt));
        Debug.Log("Changing time unit from " + GameManager.Instance.TimeUnitReciprocal + " to " + msg.TimeUnit);
        GameManager.Instance.TimeUnitReciprocal = msg.TimeUnit;
    }

    unsafe static void Handle_sst(uint client_id, IntPtr msgptr)
    {
        Msg_sst msg = (Msg_sst)Marshal.PtrToStructure(msgptr, typeof(Msg_sst));
        Debug.Log("Changing time unit from " + GameManager.Instance.TimeUnitReciprocal + " to " + msg.TimeUnit);
        GameManager.Instance.SetTimeUnitReciprocal(msg.TimeUnit);
    }

    unsafe static void Handle_seg(uint client_id, IntPtr msgptr)
    {
        Msg_seg msg = (Msg_seg)Marshal.PtrToStructure(msgptr, typeof(Msg_seg));
        Debug.Log("Game End");
        GameManager.Instance.EndGame(msg.WinningTeamName);
    }

    unsafe static void Handle_suc(uint client_id, IntPtr msgptr)
    {
    }

    unsafe static void Handle_sbp(uint client_id, IntPtr msgptr)
    {
    }
}
