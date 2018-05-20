using System.Diagnostics;

namespace NppNetInf
{
    public abstract class PluginMain
    {
        public abstract string PluginName { get; }

        public PluginMain()
        {
#if RUN_DEBUGGER
            Debugger.Launch();
#endif
        }

        public abstract void CommandMenuInit();

        public virtual void SetToolBarIcon()
        {
        }

        public virtual void OnNotification(ScNotification notification)
        {
        }

        public virtual void PluginCleanUp()
        {
        }
    }
}
