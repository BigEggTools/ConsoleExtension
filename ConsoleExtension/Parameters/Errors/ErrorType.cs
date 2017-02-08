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
        EmptyInput
    }
}
