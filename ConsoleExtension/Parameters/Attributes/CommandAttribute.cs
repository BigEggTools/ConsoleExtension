﻿namespace BigEgg.Tools.ConsoleExtension.Parameters
{
    using System;

    /// <summary>
    /// The attribute of adding the metadata of the parameter model
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CommandAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="helpMessage">The help message of the command.</param>
        public CommandAttribute(string name, string helpMessage)
        {

            Name = name;
            HelpMessage = helpMessage;
        }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the command.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the help message.
        /// </summary>
        /// <value>
        /// The help message.
        /// </value>
        /// <exception cref="System.ArgumentException">Throw if set help message to null, empty or whitespace</exception>
        public string HelpMessage { get; private set; }
    }
}
