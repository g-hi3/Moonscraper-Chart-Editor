// Copyright (c) 2016-2020 Alexander Ong
// See LICENSE in project root for license information.

using UnityEngine;
using Util;

namespace MoonscraperChartEditor.Song
{
    [System.Serializable]
    public abstract class SongObject
    {
        private readonly IDebugWrapper _debug;
        
        /// <summary>
        /// The song this object is connected to.
        /// </summary>
        [System.NonSerialized]
        public Song song;
        /// <summary>
        /// The tick position of the object
        /// </summary>
        public uint tick;

#if APPLICATION_MOONSCRAPER
        /// <summary>
        /// Unity only.
        /// </summary>
        [System.NonSerialized]
        public SongObjectController controller;
#endif

        public abstract int classID { get; }

        public SongObject(uint _tick)
            : this(_tick, DebugWrapper.Instance)
        {
        }

        private protected SongObject(uint tick, IDebugWrapper debug)
        {
            this.tick = tick;
            _debug = debug;
        }

        /// <summary>
        /// Automatically converts the object's tick position into the time it will appear in the song.
        /// </summary>
        public float time
        {
            get
            {
                return song.TickToTime(tick, song.resolution);
            }
        }
        
        public bool CollidesWith(SongObject other) => tick == other?.tick;

        public abstract SongObject Clone();

        public T CloneAs<T>() where T : SongObject
        {
            T clone = this.Clone() as T;
            _debug.Assert(clone != null, "Clone As casting type was incorrect");
            return clone;
        }

        public abstract bool AllValuesCompare<T>(T songObject) where T : SongObject;

        public static bool operator ==(SongObject a, SongObject b)
        {
            bool aIsNull = ReferenceEquals(a, null);
            bool bIsNull = ReferenceEquals(b, null);

            if (aIsNull || bIsNull)
            {
                if (aIsNull == bIsNull)
                    return true;
                else
                    return false;
            }
            else
                return a.Equals(b);
        }

        protected virtual bool Equals(SongObject b)
        {
            return tick == b.tick && classID == b.classID;
        }

        public static bool operator !=(SongObject a, SongObject b)
        {
            return !(a == b);
        }

        protected virtual bool LessThan(SongObject b)
        {
            if (tick < b.tick)
                return true;
            else if (tick == b.tick && classID < b.classID)
                return true;
            else
                return false;
        }

        public static bool operator <(SongObject a, SongObject b)
        {
            return a.LessThan(b);
        }

        public static bool operator >(SongObject a, SongObject b)
        {
            if (a != b)
                return !(a < b);
            else
                return false;
        }

        public static bool operator <=(SongObject a, SongObject b)
        {
            return (a < b || a == b);
        }

        public static bool operator >=(SongObject a, SongObject b)
        {
            return (a > b || a == b);
        }

        public override bool Equals(System.Object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Allows different classes to be sorted and grouped together in arrays by giving each class a comparable numeric value that is greater or less than other classes.
        /// </summary>
        public enum ID
        {
            TimeSignature, 
            BPM, 
            Anchor, 
            Event, 
            Section, 
            Note, 
            Starpower, 
            ChartEvent, 
            DrumRoll,
        }
    }
}
