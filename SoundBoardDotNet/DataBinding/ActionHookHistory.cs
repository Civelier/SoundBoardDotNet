using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.DataBinding
{
    public class ActionHookHistory
    {
        private static ActionHookHistory instance;

        private static ActionHookHistory _instance
        {
            get => instance = instance ?? new ActionHookHistory();
        }

        private int _actionCapacity = 20;

        private int actionCapacity
        {
            get => _actionCapacity;
            set
            {
                if (value > _actionCapacity)
                {
                    var temp = new Stack<IActionHook>(value);
                    foreach (var action in _actionStack)
                    {
                        temp.Push(action);
                    }
                    _actionStack = temp;
                }
                if (value < _actionCapacity)
                {
                    var temp = new Stack<IActionHook>(value);
                    int i = 0;
                    foreach (var action in _actionStack)
                    {
                        if (i > value) break;
                        temp.Push(action);
                    }
                }
                _actionCapacity = value;
            }
        }

        private Stack<IActionHook> _actionStack;
        private Stack<IActionHook> _redoStack = new Stack<IActionHook>();

        private ActionHookHistory()
        {
            _actionStack = new Stack<IActionHook>(_actionCapacity);
        }

        public static void Undo()
        {
            if (_instance._actionStack.Count > 0)
            {
                var action = _instance._actionStack.Pop();
                action.Undo();
                _instance._redoStack.Push(action);
            }
        }

        public static void Redo()
        {
            if (_instance._redoStack.Count > 0)
            {
                var action = _instance._redoStack.Pop();
                action.Redo();
                _instance._actionStack.Push(action);
            }
        }

        public static void PushAction(IActionHook actionHook)
        {
            _instance._actionStack.Push(actionHook);
            _instance._redoStack.Clear();
        }

        public static void UndoAll()
        {
            while (_instance._actionStack.Count > 0)
            {
                _instance._actionStack.Pop().Undo();
            }
        }

        public static void ClearStack()
        {
            _instance._actionStack.Clear();
            _instance._redoStack.Clear();
        }
    }
}
