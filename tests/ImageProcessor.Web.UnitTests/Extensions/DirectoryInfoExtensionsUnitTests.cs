﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryInfoExtensionsUnitTests.cs" company="James Jackson-South">
//   Copyright (c) James Jackson-South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ImageProcessor.Web.UnitTests.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Web.Extensions;

    using NUnit.Framework;

    /// <summary>
    /// The directory info extensions unit tests.
    /// </summary>
    public static class DirectoryInfoExtensionsUnitTests
    {
        /// <summary>
        /// The when safe enumerable directories.
        /// </summary>
        [TestFixture]
        public class WhenSafeEnumerableDirectories
        {
            /// <summary>
            /// The test directory root.
            /// </summary>
            private static readonly string TestDirectoryRoot = TestContext.CurrentContext.TestDirectory + @"\DirectoryInfoExtensionsTests";

            /// <summary>
            /// The directory count.
            /// </summary>
            private const int DirectoryCount = 4;

            /// <summary>
            /// The directory list.
            /// </summary>
            private IEnumerable<string> directoryList;

            /// <summary>
            /// The setup directories.
            /// </summary>
            [SetUp]
            public void SetupDirectories()
            {
                directoryList = Enumerable.Range(1, DirectoryCount).Select(i => string.Format("{0}/TestDirectory{1}", TestDirectoryRoot, i));
                foreach (var directory in directoryList)
                {
                    Directory.CreateDirectory(directory);
                }
            }

            /// <summary>
            /// The remove directories.
            /// </summary>
            [TearDown]
            public void RemoveDirectories()
            {
                Directory.Delete(TestDirectoryRoot, true);
            }

            /// <summary>
            /// The then should return enumerable directories given path with sub directories.
            /// </summary>
            [Test]
            public void ThenShouldReturnEnumerableDirectoriesGivenPathWithSubDirectories()
            {
                // Arrange
                var info = new DirectoryInfo(TestDirectoryRoot);

                // Act
                var directories = info.SafeEnumerateDirectories();

                // Assert
                Assert.That(directories, Is.EquivalentTo(directoryList.Select(s => new DirectoryInfo(s))));
            }

            /// <summary>
            /// The then should return empty enumerable directories given path with invalid directory
            /// </summary>
            [Test]
            public void ThenShouldReturnEmptyEnumerableDirectoriesGivenPathWithInvalidDirectory()
            {
                // Arrange
                var info = new DirectoryInfo(string.Format("{0}Bad", TestDirectoryRoot));

                // Act
                var directories = info.SafeEnumerateDirectories();

                // Assert
                Assert.That(directories, Is.EquivalentTo(Enumerable.Empty<DirectoryInfo>()));
            }
        }
    }
}