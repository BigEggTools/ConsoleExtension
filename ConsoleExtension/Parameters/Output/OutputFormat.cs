namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using BigEgg.Tools.ConsoleExtension.Parameters.Output.Text;

    internal static class OutputFormat
    {
        private static VersionInfo versionInfo = new VersionInfo();
        private static ApplicationHeader applicationHeader = new ApplicationHeader();
        private static ErrorHeader errorHeader = new ErrorHeader();
        private static DuplicateProperty duplicateProperty = new DuplicateProperty();

        public static VersionInfo VERSION_INFO { get { return versionInfo; } }
        public static ApplicationHeader HEADER { get { return applicationHeader; } }
        public static ErrorHeader ERROR_HEADER { get { return errorHeader; } }
        public static DuplicateProperty DUPLICATE_PROPERTY { get { return duplicateProperty; } }
    }
}
