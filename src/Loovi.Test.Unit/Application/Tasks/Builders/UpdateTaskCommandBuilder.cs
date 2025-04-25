using Loovi.Test.Application.Tasks.UpdateTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Unit.Application.Tasks.Builders
{
    public class UpdateTaskCommandBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _title = "Updated Task Title";
        private string _description = "Updated task description.";
        private DateTime _dueDate = DateTime.UtcNow.AddDays(3);

        public static UpdateTaskCommandBuilder New()
        {
            return new UpdateTaskCommandBuilder();
        }

        public UpdateTaskCommandBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public UpdateTaskCommandBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public UpdateTaskCommandBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public UpdateTaskCommandBuilder WithDueDate(DateTime dueDate)
        {
            _dueDate = dueDate;
            return this;
        }

        public UpdateTaskCommand Build()
        {
            return new UpdateTaskCommand
            {
                Id = _id,
                Title = _title,
                Description = _description,
                DueDate = _dueDate
            };
        }
    }
}
