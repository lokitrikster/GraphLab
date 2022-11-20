using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLab.Core.Algorithms.Utils
{
    /// <summary>
    /// Хранилище дополнительных данных (состояния)
    /// для использования в алгоритмах
    /// </summary>
    /// <typeparam name="T">Тип основных данных</typeparam>
    /// <typeparam name="TState">Тип состояния</typeparam>
    public class StateManager<T, TState>
    {
        private IDictionary<T, TState> _stateDict = new Dictionary<T, TState>();

        public StateManager(TState defaultState)
        {
            DefaultState = defaultState;
        }

        public TState DefaultState { get; private set; }

        public TState this[T index]
        {
            get
            {
                if (_stateDict.ContainsKey(index))
                    return _stateDict[index];
                return DefaultState;
            }
            set { _stateDict[index] = value; }
        }
    }
}
