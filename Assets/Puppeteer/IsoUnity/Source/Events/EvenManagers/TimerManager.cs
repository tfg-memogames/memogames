using IsoUnity.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsoUnity.Events
{
    public class TimerManager : EventedEventManager
    {

        [GameEvent(true,false)]
        public IEnumerator Wait(float time, bool realtime = false)
        {
            if(realtime)
                yield return new WaitForSecondsRealtime(time);
            else
                yield return new WaitForSeconds(time);
        }
    }
}
