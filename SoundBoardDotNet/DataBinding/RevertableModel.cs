using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.DataBinding
{
    public delegate void RevertableModelChangedEventHandler(RevertableModel sender);
    public abstract class RevertableModel
    {
        public RevertableModelChangedEventHandler Changed;

        public abstract RevertableModel Copy();

        public abstract void Reload(RevertableModel state);

        protected void BeforeChange()
        {
            ActionHookHistory.PushAction(new RevertableModelActionHook(this));
        }

        protected void OnChanged()
        {
            RevertableModelChangedEventHandler handler = Changed;
            handler?.Invoke(this);
        }
    }
}
