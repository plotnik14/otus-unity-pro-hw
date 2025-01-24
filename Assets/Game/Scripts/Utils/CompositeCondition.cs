using System;
using System.Collections.Generic;

namespace Utils
{
    public class CompositeCondition : IDisposable
    {
        private readonly HashSet<Func<bool>> _conditions = new();

        public bool IsTrue
        {
            get
            {
                foreach (var condition in _conditions)
                {
                    if (!condition.Invoke())
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public void AddCondition(Func<bool> condition) => _conditions.Add(condition);

        public void RemoveCondition(Func<bool> condition) => _conditions.Remove(condition);

        public void Dispose() => _conditions.Clear();
    }
}