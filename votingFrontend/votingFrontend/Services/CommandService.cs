// <copyright file="CommandService.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Services
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Service that allows Commands to be passed between view and view model
    /// </summary>
    public class CommandService : ICommand
    {
        private Predicate<object> canExecute;
        private Action<object> execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandService"/> class.
        /// </summary>
        /// <param name="execute">The method to execute</param>
        /// <param name="canExecute">If the command is able to execute</param>
        public CommandService(Action<object> execute, Predicate<object> canExecute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandService"/> class.
        /// </summary>
        /// <param name="execute">The method to execute</param>
        public CommandService(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            if (!this.CanExecute(parameter))
            {
                return;
            }

            this.execute(parameter);
        }

        /// <summary>
        /// Handles if the execution status has changed
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
