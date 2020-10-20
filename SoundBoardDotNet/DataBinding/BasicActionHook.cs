using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.DataBinding
{
    public class BasicActionHook : IActionHook
    {
        private bool _isUndone;
        private object _subject;
        private Action _action;
        private Action _undo;

        public event UndoneEventHandler Undone;
        public event RedoneEventHandler Redone;

        public bool IsUndone => _isUndone;

        public object Subject => _subject;

        public bool UsesCopy => false;

        public BasicActionHook(object subject, Action action, Action undo)
        {
            _subject = subject;
            _action = action;
            _undo = undo;
        }

        public void Redo()
        {
            if (_isUndone)
            {
                _action();
                _isUndone = false;
                var handler = Redone;
                handler?.Invoke(this);
            }
        }

        public void Undo()
        {
            if (!_isUndone)
            {
                _undo();
                _isUndone = true;
                var handler = Undone;
                handler?.Invoke(this);
            }
        }
    }
}
