using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PowerScada
{
    public class EditbuttonCommand
    {
        public EditbuttonCommand() { }

        public EditbuttonCommand(EditCommandExecutor editexecutor)
        {
            this.editexecutor = editexecutor;
        }

        public EditbuttonCommand(EditCommandValidator editvalidator, EditCommandExecutor editexecutor)
            : this(editexecutor)
        {
            this.editvalidator = editvalidator;
        }

        private EditCommandValidator editvalidator;
        /// <summary>
        /// edit buttonlar için eklendi.
        /// </summary>
        private EditCommandExecutor editexecutor;

        public virtual void Execute(object nesne)
        {
            if (editexecutor == null) return;

            if (editvalidator != null)
                if (!editvalidator()) return;

            editexecutor(nesne);
        }
    }

    public delegate bool EditCommandValidator();

    public delegate void EditCommandExecutor(object o);


}
