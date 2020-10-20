using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.DataBinding
{
    public delegate void UndoneEventHandler(IActionHook sender);
    public delegate void RedoneEventHandler(IActionHook sender);

    public interface IActionHook
    {
        event UndoneEventHandler Undone;
        event RedoneEventHandler Redone;

        bool UsesCopy { get; }
        bool IsUndone { get; }
        object Subject { get; }
        void Undo();
        void Redo();
    }
}
