using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpBullet.UI
{
    public class BaseForm : Form
    {
        private List<Command> commands = new List<Command>();
        public List<Command> Commands { get { return commands; } }
       
        private List<CommandBinding> commandBindings = new List<CommandBinding>();
        public List<CommandBinding> CommandBindings { get { return commandBindings; } }

        public bool IsEnabled(string commandName)
        {
            Command command = FindCommand(commandName);
            if (command != null)
            {
                return command.IsEnabled(null);
            }
            return false;
        }

        public Command FindCommand(string commandName)
        {
            foreach (Command command in Commands)
            {
                if (command.Name == commandName)
                {
                    return command;
                }
            }
            return null;
        }

        public void Run(string commandName)
        {
            Command command = FindCommand(commandName);
            if (command != null)
            {
                command.Execute(null);
            }
        }
    }
}