//UnitTest1.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathUtils;
using System.Collections.Generic;

namespace MathMethodTests
{
    [TestClass]
    public sealed class SequenceFinderTests
    {
        [TestMethod]
        public void FindAllSequences_NormalArray()
        {
            // Arrange
            int[] array = { 1, 2, 3, 1, 2, 3, 4, 1, 2 };

            // Act
            List<Sequence> sequences = SequenceFinder.FindAllSequences(array);

            // Assert
            Assert.AreEqual(3, sequences.Count);
            Assert.AreEqual(3, sequences[0].Length);
            Assert.AreEqual(4, sequences[1].Length);
            Assert.AreEqual(2, sequences[2].Length);
        }

        [TestMethod]
        public void FindAllSequences_EmptyArray()
        {
            // Arrange
            int[] array = { };

            // Act
            List<Sequence> sequences = SequenceFinder.FindAllSequences(array);

            // Assert
            Assert.AreEqual(0, sequences.Count);
        }

        [TestMethod]
        public void FindAllSequences_SingleElement()
        {
            // Arrange
            int[] array = { 42 };

            // Act
            List<Sequence> sequences = SequenceFinder.FindAllSequences(array);

            // Assert
            Assert.AreEqual(1, sequences.Count);
            Assert.AreEqual(1, sequences[0].Length);
        }

        [TestMethod]
        public void FindAllSequences_AllEqual()
        {
            // Arrange
            int[] array = { 5, 5, 5, 5, 5 };

            // Act
            List<Sequence> sequences = SequenceFinder.FindAllSequences(array);

            // Assert
            Assert.AreEqual(1, sequences.Count);
            Assert.AreEqual(5, sequences[0].Length);
        }

        [TestMethod]
        public void FindAllSequences_Descending()
        {
            // Arrange
            int[] array = { 5, 4, 3, 2, 1 };

            // Act
            List<Sequence> sequences = SequenceFinder.FindAllSequences(array);

            // Assert
            Assert.AreEqual(5, sequences.Count);
            foreach (var seq in sequences)
            {
                Assert.AreEqual(1, seq.Length);
            }
        }

        [TestMethod]
        public void GetTwoLongest_NormalCase()
        {
            // Arrange
            int[] array = { 1, 2, 3, 1, 2, 3, 4, 1, 2 };
            var allSequences = SequenceFinder.FindAllSequences(array);

            // Act
            var twoLongest = SequenceFinder.GetTwoLongest(allSequences);

            // Assert
            Assert.AreEqual(2, twoLongest.Count);
            Assert.AreEqual(4, twoLongest[0].Length);
            Assert.AreEqual(3, twoLongest[1].Length);
        }

        [TestMethod]
        public void GetTwoLongest_OnlyOneSequence()
        {
            // Arrange
            int[] array = { 1, 2, 3, 4, 5 };
            var allSequences = SequenceFinder.FindAllSequences(array);

            // Act
            var twoLongest = SequenceFinder.GetTwoLongest(allSequences);

            // Assert
            Assert.AreEqual(1, twoLongest.Count);
            Assert.AreEqual(5, twoLongest[0].Length);
        }
    }
}