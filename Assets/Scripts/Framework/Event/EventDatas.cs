using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPS_Homework_Framework
{

    // Event Types
    // 加载场景完毕
    // 进入游戏
    // 延时
    // 伤害计算
    public enum EventID
    {
        LOADING,
        ENTERGAME,
        TIMEDELAY,
        TAKEDAMAGE,
    }

    #region Event Args
    
    
    public class BaseEventArgs : EventArgs
    {
        
    }

    public class EventArgsOneFloat : BaseEventArgs
    {
        public float floatArg;
        public EventArgsOneFloat(float arg)
        {
            floatArg = arg;
        }
        
    }
    
    #endregion
    
    // Event
    public class Event
    {
        private object mSender;
        private BaseEventArgs mEventArgs;
        private EventID mEventId;
        
        public object Sender
        {
            get
            {
                return mSender;
            }
        }

        public BaseEventArgs EventArgs
        {
            get
            {
                return mEventArgs;
            }
        }

        public EventID EventId
        {
            get
            {
                return mEventId;
            }
        }

        public Event(EventID eventID,object sender, BaseEventArgs baseEventArgs)
        {
            mEventId = eventID;
            mSender = sender;
            mEventArgs = baseEventArgs;
        }
        
    }
    
    
}
