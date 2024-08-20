using Customize.Domain.Extensions;

namespace Customize.Tests.Extensions
{
    public static class FixtureExtensions
    {
        private static string GetPath(string fixturePath)
        {
            var binFolder = AppContext.BaseDirectory.LastIndexOf("bin");
            var projectPath = AppContext.BaseDirectory.Substring(0, binFolder);
            var filePath = Path.Combine(projectPath, "Fixtures", fixturePath);

            return filePath;
        }

        public static string FromFixture(this string fixturePath)
        {
            var filePath = GetPath(fixturePath);    
            return File.ReadAllText(filePath);
        }

        public static T? FromFixture<T>(this string fixturePath) 
        {
            return FromFixture(fixturePath).Deserialize<T>();
        }
    }
}
