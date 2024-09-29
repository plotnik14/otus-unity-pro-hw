using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Utils
{
    public static class Extensions
    {
        public static void SafeInvoke([CanBeNull] this Action action)
        {
            if (action == null)
            {
                return;
            }

            try
            {
                action();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void SafeInvoke<T1>([CanBeNull] this Action<T1> action, T1 t1)
        {
            if (action == null)
            {
                return;
            }

            try
            {
                action(t1);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void SafeInvoke<T1, T2>([CanBeNull] this Action<T1, T2> action, T1 t1, T2 t2)
        {
            if (action == null)
            {
                return;
            }

            try
            {
                action(t1, t2);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void SafeInvoke<T1, T2, T3>([CanBeNull] this Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3)
        {
            if (action == null)
            {
                return;
            }

            try
            {
                action(t1, t2, t3);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void SafeInvoke<T1, T2, T3, T4>([CanBeNull] this Action<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            if (action == null)
            {
                return;
            }

            try
            {
                action(t1, t2, t3, t4);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}