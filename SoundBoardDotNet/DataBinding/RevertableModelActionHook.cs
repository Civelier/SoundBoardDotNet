using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.DataBinding
{
    public class RevertableModelActionHook : IActionHook
    {
        private RevertableModel _model;
        private RevertableModel _reference;
        private RevertableModel _copy;
        private bool _isUndone;

        public bool IsUndone => _isUndone;

        public object Subject => _reference;

        public bool UsesCopy => true;

        public event UndoneEventHandler Undone;
        public event RedoneEventHandler Redone;

        public RevertableModelActionHook(RevertableModel reference)
        {
            _reference = reference;
            _copy = reference.Copy();
        }

        public void Redo()
        {
            if (_isUndone)
            {
                _reference.Reload(_model);
                var handler = Redone;
                handler?.Invoke(this);
                _isUndone = false;
            }
        }

        public void Undo()
        {
            if (!_isUndone)
            {
                _model = _reference.Copy();
                _reference.Reload(_copy);
                var handler = Undone;
                handler?.Invoke(this);
                _isUndone = true;
            }
        }
    }
}
