using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SoundBoardDotNet;

namespace SoundBoardTests
{
    [TestClass]
    public class VersionTests
    {
        [TestMethod]
        public void TestPatchIncrement()
        {
            var version = new VersionNumber(0, 0, 0);
            version.UpdateVersion();
            version.Number.Should().BeEquivalentTo(new int[] { 0, 0, 1 });
        }

        [TestMethod]
        public void TestMinorIncrement()
        {
            var version = new VersionNumber(1, 2, 5);
            version.UpdateVersion(1);
            version.Number.Should().BeEquivalentTo(new int[] { 1, 3, 0 });
        }

        [TestMethod]
        public void TestSupportedVersion()
        {
            IVersion version1 = new SemanticVersion(2, 4, 1);
            version1.AddSupportedVersion(new VersionNumber(1, 0, 0));
            version1.AddSupportedVersion(new VersionNumber(2, 0, 0));

            version1.IsSupported(new VersionNumber(1, 3, 5)).Should().BeTrue();
            version1.IsSupported(new VersionNumber(2, 5, 7)).Should().BeTrue();
            version1.IsSupported(new VersionNumber(0, 4, 9)).Should().BeFalse();


            IVersion version2 = new GeneralVersion(new VersionNumber(5, 2, 6, 1));

            version2.AddSupportedVersion(new VersionNumber(3, 0, 0, 0), 1);
            version2.AddSupportedVersion(new VersionNumber(3, 1, 0, 0), 1);
            version2.AddSupportedVersion(new VersionNumber(3, 2, 0, 0), 1);


            version2.AddSupportedVersion(new VersionNumber(4, 0, 0, 0), 1);
            version2.AddSupportedVersion(new VersionNumber(4, 1, 0, 0), 1);
            // Ex: version (4, 2, 0, 0) had a devastating bug and is not supported
            version2.AddSupportedVersion(new VersionNumber(4, 3, 0, 0), 1);


            version2.AddSupportedVersion(new VersionNumber(5, 0, 0, 0), 1);
            version2.AddSupportedVersion(new VersionNumber(5, 1, 0, 0), 1);
            version2.AddSupportedVersion(new VersionNumber(5, 2, 0, 0), 1);


            version2.IsSupported(new VersionNumber(5, 2, 189, 5)).Should().BeTrue();
            version2.IsSupported(new VersionNumber(2, 5, 2, 6)).Should().BeFalse();
            version2.IsSupported(new VersionNumber(4, 2, 0, 1)).Should().BeFalse();
        }

        [TestMethod]
        public void SemanticCompatibility()
        {
            IVersion version = new SemanticVersion(2, 5, 3);
            version.IsCompatible(new VersionNumber(2, 10, 6)).Should().BeTrue();
            version.IsCompatible(new VersionNumber(1, 50, 2)).Should().BeFalse();
            version.IsCompatible(new VersionNumber(3, 4, 10)).Should().BeFalse();
        }
    }
}
