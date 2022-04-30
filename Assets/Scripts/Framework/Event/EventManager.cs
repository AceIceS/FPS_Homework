using System;
using System.Collections.Generic;

namespace FPS_Homework_Framework
{

    public class EventManager : BaseManager<EventManager>, IGameFrameworkModule
    {

        private Dictionary<EventID, List<Event>> mEventId2Event;
        private Dictionary<object, List<EventHandler<BaseEventArgs>>> mEventSender2EventHandler;
        
        public void Initialize()
        {
            mEventId2Event = new Dictionary<EventID, List<Event>>();
            mEventSender2EventHandler = new Dictionary<object, List<EventHandler<BaseEventArgs>>>();
        }
        
        public void TriggerEventWithEventID(EventID eventID)
        {
            if (mEventId2Event.TryGetValue(eventID, out List<Event> events))
            {
                foreach (var triggeredEvent in events)
                {
                    List<EventHandler<BaseEventArgs>> eventHandlers = mEventSender2EventHandler[triggeredEvent.Sender];
                    foreach (var eventHandler in eventHandlers)
                    {
                        eventHandler(triggeredEvent.Sender, triggeredEvent.EventArgs);
                    }
                }
            }
            
        }

        public void TriggerEventWithEventIdAndArgs(EventID eventID,BaseEventArgs args)
        {
            if (mEventId2Event.TryGetValue(eventID, out List<Event> events))
            {
                foreach (var triggeredEvent in events)
                {
                    List<EventHandler<BaseEventArgs>> eventHandlers = mEventSender2EventHandler[triggeredEvent.Sender];
                    foreach (var eventHandler in eventHandlers)
                    {
                        eventHandler(triggeredEvent.Sender, args);
                    }
                }
            }
            
        }
        
        public void RegisterEvent(Event theEvent,EventHandler<BaseEventArgs> handler)
        {
            if (mEventId2Event.ContainsKey(theEvent.EventId))
            {
                mEventId2Event[theEvent.EventId].Add(theEvent);
                mEventSender2EventHandler[theEvent.Sender].Add(handler);
            }
            else
            {
                mEventId2Event.Add(theEvent.EventId, new List<Event>() {theEvent});
                mEventSender2EventHandler.Add(theEvent.Sender,
                    new List<EventHandler<BaseEventArgs>>() { handler });
            }
            
        }
        
        public void OnGameStart()
        {
            
        }
        
        public void InitializeModuleBeforeOnStart()
        {
            
        }
        
        // 事件轮询
        public void UpdateModule()
        {
            
        }

        public void FixUpdateModule()
        {
            
        }

        public void LateUpdateModule()
        {
            
        }
    }

}
