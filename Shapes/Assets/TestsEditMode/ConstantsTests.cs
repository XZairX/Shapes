﻿using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class ConstantsTests
    {
        private Constants CreateDefaultConstants()
        {
            return new GameObject().AddComponent<Constants>();
        }

        private void RemoveBoundaryGameTag()
        {
            GameObject.FindWithTag(Tags.BoundaryGame).tag = Tags.Debug;
        }

        private void RestoreBoundaryGameTag()
        {
            GameObject.FindWithTag(Tags.Debug).tag = Tags.BoundaryGame;
        }

        [Test]
        public void Awake_BoundaryDoesNotExist_SetsBoundaryWidthToZero()
        {
            RemoveBoundaryGameTag();
            var sut = CreateDefaultConstants();
            sut.runInEditMode = true;

            var result = sut.GameWidth;

            Assert.That(result, Is.Zero);
            RestoreBoundaryGameTag();
        }

        [Test]
        public void Awake_BoundaryDoesNotExist_SetsBoundaryHeightToZero()
        {
            RemoveBoundaryGameTag();
            var sut = CreateDefaultConstants();
            sut.runInEditMode = true;

            var result = sut.GameHeight;

            Assert.That(result, Is.Zero);
            RestoreBoundaryGameTag();
        }

        [Test]
        public void Awake_BoundaryExists_SetsBoundaryWidthToBoundaryChildren()
        {
            var boundary = GameObject.FindWithTag(Tags.BoundaryGame);
            var boundaryChild1 = boundary.transform.GetChild(1).position.x;
            var boundaryChild2 = boundary.transform.GetChild(3).position.x;
            var sut = CreateDefaultConstants();
            sut.runInEditMode = true;

            var result = sut.GameWidth;

            Assert.That(result, Is.EqualTo(boundaryChild1 - boundaryChild2));
        }

        [Test]
        public void Awake_BoundaryExists_SetsBoundaryHeightToBoundaryChildren()
        {
            var boundary = GameObject.FindWithTag(Tags.BoundaryGame);
            var boundaryChild1 = boundary.transform.GetChild(0).position.y;
            var boundaryChild2 = boundary.transform.GetChild(2).position.y;
            var sut = CreateDefaultConstants();
            sut.runInEditMode = true;

            var result = sut.GameHeight;

            Assert.That(result, Is.EqualTo(boundaryChild1 - boundaryChild2));
        }

        [Test]
        public void Awake_SetsHalfBoundaryWidthToBoundaryWidthDividedByTwo()
        {
            var sut = CreateDefaultConstants();
            sut.runInEditMode = true;

            var result = sut.HalfGameWidth;

            Assert.That(result, Is.EqualTo(sut.GameWidth / 2.0f));
        }

        [Test]
        public void Awake_SetsHalfBoundaryHeightToBoundaryHeightDividedByTwo()
        {
            var sut = CreateDefaultConstants();
            sut.runInEditMode = true;

            var result = sut.HalfGameHeight;

            Assert.That(result, Is.EqualTo(sut.GameHeight / 2.0f));
        }
    }
}
