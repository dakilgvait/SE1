using Sandbox.ModAPI.Ingame;
using VRageMath;

namespace DGSpaceEngineers.Modules
{
    public class LoggerModule
    {
        public LoggerModule(IMyGridTerminalSystem gridSystem)
        {
            Initialize(gridSystem);
            Clear();
        }

        public IMyTextPanel TextPanel { get; private set; }

        protected virtual void Initialize(IMyGridTerminalSystem gridSystem)
        {
            var textPanels = new List<IMyTextPanel>();
            gridSystem.GetBlocksOfType(textPanels, x => x.CustomName.Contains("[Logger]"));

            TextPanel = textPanels.SingleOrDefault();
        }

        public void Debug(string message)
        {
            if (TextPanel == null) return;

            TextPanel.WriteText(message, true);

            TextPanel.FontColor = Color.White;
        }

        public void Information(string message)
        {
            if (TextPanel == null) return;

            TextPanel.WriteText(message, true);

            TextPanel.FontColor = Color.Green;
        }

        public void Warning(string message)
        {
            if (TextPanel == null) return;

            TextPanel.WriteText(message, true);

            TextPanel.FontColor = Color.Yellow;
        }

        public void Error(string message = null, Exception exception = null)
        {
            if (TextPanel == null) return;

            if (!string.IsNullOrEmpty(message))
            {

                TextPanel.WriteText(message, true);
            }

            if (exception != null)
            {
                TextPanel.WriteText(exception.Message, true);
                TextPanel.WriteText(exception.StackTrace, true);
            }

            TextPanel.FontColor = Color.Red;
        }

        public void Clear()
        {
            if (TextPanel == null) return;

            TextPanel.WriteText(string.Empty, false);
        }
    }
}