using Pokemon_Go_Threaded_Trainer.Forms;
using Pokemon_Go_Threaded_Trainer.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon_Go_Threaded_Trainer.Classes
{
    public class Log : ILog
    {
        public void Log_(int id, Color color, String message)
        {
            try
            {
                var consoleControl = (RichTextBox)Main.panel.Controls[$"{id}"].Controls[$"console{id}"];
                    consoleControl.Invoke((MethodInvoker)delegate ()
                    {
                        consoleControl.SelectionStart = consoleControl.TextLength;
                        consoleControl.SelectionLength = 0;
                        consoleControl.SelectionColor = color;
                        consoleControl.AppendText($"[{ DateTime.Now.ToString("HH:mm:ss")}] { message}\n");
                        consoleControl.ScrollToCaret();
                    });
                    return;
            }
            catch (Exception ex) { Main.errors++; }
        }
    }
}
