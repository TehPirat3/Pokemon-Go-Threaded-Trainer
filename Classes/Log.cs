using PokemonGoBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGoBot.Classes
{
    public class Log : ILog
    {
        public void Log_(int id, Color color, String message)
        {
            try
            {
                var consoleControl = (RichTextBox)Main.panel.Controls[$"{id}"].Controls[$"console{id}"];
                //if (InvokeRequired)
                //{
                    consoleControl.Invoke((MethodInvoker)delegate ()
                    {
                        consoleControl.SelectionStart = consoleControl.TextLength;
                        consoleControl.SelectionLength = 0;
                        consoleControl.SelectionColor = color;
                        consoleControl.AppendText($"[{ DateTime.Now.ToString("HH:mm:ss")}] { message}\n");
                        consoleControl.ScrollToCaret();
                    });
                    return;
                /*}
                else
                {
                    consoleControl.SelectionStart = consoleControl.TextLength;
                    consoleControl.SelectionLength = 0;
                    consoleControl.SelectionColor = color;
                    consoleControl.AppendText(message);
                    consoleControl.ScrollToCaret();
                }*/
            }
            catch (Exception ex) { Main.errors++; }
        }
    }
}
