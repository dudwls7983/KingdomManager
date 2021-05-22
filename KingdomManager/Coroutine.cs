using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://waterbeetle.tistory.com/4
namespace KingdomManager
{
    class CoroutineManager
    {
        private static CoroutineManager _instance;
        private CoroutineManager() { }

        List<IEnumerator> m_routines;

        static CoroutineManager()
        {
            _instance = new CoroutineManager();
            _instance.m_routines = new List<IEnumerator>();
        }

        public static IEnumerator StartCoroutine(IEnumerator routine)
        {
            _instance.m_routines.Add(routine);
            return routine;
        }

        public static bool StopCoroutine(IEnumerator routine)
        {
            return _instance.m_routines.Remove(routine);
        }

        public static void StopAllCoroutines()
        {
            _instance.m_routines.Clear();
        }

        public static bool IsRunning(IEnumerator routine)
        {
            return _instance.m_routines.Contains(routine);
        }

        public static int Runnings()
        {
            return _instance.m_routines.Count;
        }

        private static bool Process(IEnumerator routine)
        {
            do
            {
                object current = routine.Current;
                if (current is IEnumerator)
                {
                    IEnumerator other_routine = current as IEnumerator;
                    if (!Process(other_routine)) return false;
                    else continue;
                }
                else if (current is CustomYieldInstruction)
                {
                    CustomYieldInstruction yieldInstruction = current as CustomYieldInstruction;
                    if (yieldInstruction.keepWaiting) return false;
                }
            }
            while (routine.MoveNext());

            if (!routine.MoveNext()) _instance.m_routines.Remove(routine);
            return true;
        }

        public static void Update()
        {
            for (int i = 0; i < _instance.m_routines.Count; i++)
            {
                Process(_instance.m_routines[i]);
            }
        }
    }
    
    abstract class CustomYieldInstruction
    {
        public abstract bool keepWaiting { get; }
    }

    class WaitForSeconds : CustomYieldInstruction
    {
        const float TicksPerSecond = TimeSpan.TicksPerSecond;
        long m_end;

        public WaitForSeconds(float seconds)
        {
            m_end = DateTime.Now.Ticks + (long)(TicksPerSecond * seconds);
        }

        public override bool keepWaiting
        {
            get { return DateTime.Now.Ticks < m_end; }
        }
    }

    class WaitForUpdate : CustomYieldInstruction
    {
        bool m_nextframe_has_passed;

        public WaitForUpdate()
        {
            m_nextframe_has_passed = false;
        }

        public override bool keepWaiting
        {
            get
            {
                if (!m_nextframe_has_passed)
                {
                    m_nextframe_has_passed = true;
                    return true;
                }
                return false;
            }
        }
    }
}
