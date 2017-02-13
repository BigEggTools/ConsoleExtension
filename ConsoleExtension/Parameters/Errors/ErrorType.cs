namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    /// <summary>
    /// The enumeration of error types
    /// </summary>
    internal enum ErrorType
    {
        /// <summary>
        /// User request the help.
        /// </summary>
        HelpRequest,
        /// <summary>
        /// User request the help on command.
        /// </summary>
        CommandHelpRequest,
        /// <summary>
        /// User request the version info.
        /// </summary>
        VersionRequest,
        /// <summary>
        /// User input duplicate property
        /// </summary>
        DuplicateProperty,
        /// <summary>
        /// User input nothing
        /// </summary>
        EmptyInput,
        /// <summary>
        /// User input unknown command
        /// </summary>
        UnKnownCommand,
        /// <summary>
        /// User not input specific the command
        /// </summary>
        MissingCommand,
        /// <summary>
        /// Developer used a type as command which don't have command attribute
        /// </summary>
        Develop_MissingCommand,
        /// <summary>
        /// Developer used a type as command which have invalid command attribute
        /// </summary>
        Develop_InvalidCommand,
        /// <summary>
        /// Developer mark 2 command as same name
        /// </summary>
        Develop_DuplicateCommand,
        /// <summary>
        /// Developer used a type as command which have invalid property attribute
        /// </summary>
        Develop_InvalidProperty,
        /// <summary>
        /// Developer used a type as command which have property attribute mismatch with the property type
        /// </summary>
        Develop_PropertyTypeMismatch,
        /// <summary>
        /// Developer used a type as command which have property attribute cannot write
        /// </summary>
        Develop_PropertyTypeCannotWrite,
        /// <summary>
        /// Developer used a type as command which have duplicate property name
        /// </summary>
        Develop_DuplicateProperty
    }
}
