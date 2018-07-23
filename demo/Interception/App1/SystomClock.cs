﻿using System;

namespace App
{
    public class SystomClock : ISystomClock
    {
        //[CacheReturnValue]
        public DateTime GetCurrentTime(DateTimeKind dateTimeKind)
        {
            switch (dateTimeKind)
            {
                case DateTimeKind.Local: return DateTime.UtcNow.ToLocalTime();
                case DateTimeKind.Unspecified: return DateTime.Now;
                default: return DateTime.UtcNow;
            }
        }
    }
}
