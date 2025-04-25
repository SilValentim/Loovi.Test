using System;
using Loovi.Test.Application.Tasks.CreateTask;
using static System.DateTime;

namespace Loovi.Test.Unit.Application.Tasks.Builders
{
    public class CreateTaskCommandBuilder
    {
        private string _title = "Valid Task Title";
        private string _description = "This is a valid task description.";
        private DateTime _dueDate = UtcNow.AddDays(1);

        public static CreateTaskCommandBuilder New()
        {
            return new CreateTaskCommandBuilder();
        }

        public CreateTaskCommandBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public CreateTaskCommandBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public CreateTaskCommandBuilder WithDueDate(DateTime dueDate)
        {
            _dueDate = dueDate;
            return this;
        }

        public CreateTaskCommand Build()
        {
            return new CreateTaskCommand
            {
                Title = _title,
                Description = _description,
                DueDate = _dueDate
            };
        }
    }
}
