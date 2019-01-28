using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

/*
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
unsafe public delegate void callback(uint client_id, IntPtr packet);

public static class ServerPoller
{
    private static object _lock = new object();

    static Queue<PendingTask> pending = new Queue<PendingTask>();

    public unsafe static void AddToQueue(PendingTask task)
    {
        Debug.Log("Try add task to queue");
        lock (_lock)
        {
            pending.Enqueue(task);
        }
        Debug.Log("Finished adding task to queue");
    }

    public static void PollQueue()
    {
        Debug.Log("polling");
        PendingTask task = new PendingTask();
        lock (_lock)
        {
            if (pending.Count <= 0)
            {
                return;
            }
            Debug.Log("Dequeue task");
            task = pending.Dequeue();
        }
        task.handler(task.client_id, task.packet);
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct PendingTask
{
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public callback handler;
    public uint client_id;
    public IntPtr packet;
}*/