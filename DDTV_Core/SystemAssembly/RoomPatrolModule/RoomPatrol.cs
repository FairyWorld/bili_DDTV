﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DDTV_Core.SystemAssembly.RoomPatrolModule
{
    internal class RoomPatrol
    {
        public static void Init()
        {
            BilibiliModule.Rooms.Rooms.UpdateRoomInfo();
            foreach (var item in BilibiliModule.Rooms.Rooms.RoomInfo)
            {
                if (item.Value.live_status==1)
                {
                    Log.Log.AddLog(nameof(RoomPatrol), Log.LogClass.LogType.Info, $"检测到【{item.Value.room_id}-{item.Value.uname}】开播-标题[{item.Value.title}]");
                    if (item.Value.IsAutoRec)
                    {
                        //自动录制
                        Log.Log.AddLog(nameof(RoomPatrol), Log.LogClass.LogType.Info, $"根据配置开始自动录制【{item.Value.room_id}-{item.Value.uname}】的直播流");
                        //这下面应该写录制的操作了(施工中)
                    }
                }
            }
            Task.Run(() => 
            {
                while(true)
                {
                    Thread.Sleep(10*1000);
                    Patrol();
                }
            });
        }
        /// <summary>
        /// 循环任务
        /// </summary>
        public static void Patrol()
        {

            Dictionary<long, int> keyValuePairs = new Dictionary<long, int>();
            foreach (var item in BilibiliModule.Rooms.Rooms.RoomInfo)
            {
                keyValuePairs.Add(item.Key, item.Value.live_status);
            }
            BilibiliModule.Rooms.Rooms.UpdateRoomInfo();
            foreach (var item in BilibiliModule.Rooms.Rooms.RoomInfo)
            {
                if(item.Value.uid==408490081)
                {
                    ;
                }
                if(item.Value.live_status==1)
                {
                    if(keyValuePairs[item.Value.uid]!=1)
                    {
                        Log.Log.AddLog(nameof(RoomPatrol), Log.LogClass.LogType.Info, $"检测到【{item.Value.room_id}-{item.Value.uname}】开播-标题[{item.Value.title}]");
                        //开播了
                        if(item.Value.IsAutoRec)
                        {
                            //自动录制警告！
                            Log.Log.AddLog(nameof(RoomPatrol), Log.LogClass.LogType.Info, $"根据配置开始自动录制【{item.Value.room_id}-{item.Value.uname}】的直播流");
                            //这下面应该写录制的操作了(施工中)
                        }
                        if (item.Value.IsRemind)
                        {
                            //开播提醒警告！
                            Log.Log.AddLog(nameof(RoomPatrol), Log.LogClass.LogType.Info, $"开播提醒:【{item.Value.room_id}-{item.Value.uname}】");
                        }
                    }
                }
            }
            Log.Log.AddLog(nameof(RoomPatrol), Log.LogClass.LogType.Debug, $"周期性更新房间信息成功");
        }
    }
}
