using System;
using System.Windows.Forms;

namespace Ui
{
    public static class ControlExtentions
    {
        /// <summary>
        /// Вызов делегата через control.Invoke, если это необходимо.
        /// </summary>
        /// <param name="control">Элемент управления</param>
        /// <param name="doit">Делегат с некоторым действием</param>
        public static void InvokeIfNeeded(this Control control, Action doit)
        {
            try
            {
                if (control.InvokeRequired)
                    control.Invoke(doit);
                else
                    doit();
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}