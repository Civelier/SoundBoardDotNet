using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.DataBinding
{
    internal class UndoAllContext
    {
        private List<object> _trackedObjects = new List<object>();
        private Stack<IActionHook> _actions = new Stack<IActionHook>();

        public bool TrackAction(IActionHook action)
        {
            if (action.UsesCopy)
            {
                if (!_trackedObjects.Contains(action.Subject))
                {
                    _trackedObjects.Add(action.Subject);
                    _actions.Push(action);
                }
                return true;
            }
            else
            {
                if (_actions.Count != 0) return false;
                _trackedObjects.Add(action.Subject);
                _actions.Push(action);
                return true;
            }
        }

        public void Undo()
        {
            while (_actions.Count > 0)
            {
                _actions.Pop().Undo();
            }
        }
    }
}
