﻿using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class TimeControllerTests
    {
        private const float _defaultTime = 1.0f;
        private const float _minTimeValue = 0.05f;
        private const float _maxTimeValue = 10.0f;
        private const float _timeFactor = 0.05f;

        private TimeController CreateDefaultTimeController()
        {
            return new TimeController();
        }

        [Test]
        public void MinTime_DefaultValue_IsCorrect()
        {
            var sut = CreateDefaultTimeController();

            var result = sut.MinTime;

            Assert.That(result, Is.EqualTo(_minTimeValue));
        }

        [Test]
        public void MaxTime_DefaultValue_IsCorrect()
        {
            var sut = CreateDefaultTimeController();

            var result = sut.MaxTime;

            Assert.That(result, Is.EqualTo(_maxTimeValue));
        }

        [Test]
        public void ResetTime_SetsTimeScaleToPositiveOne()
        {
            var sut = CreateDefaultTimeController();

            sut.ResetTime();
            var result = Time.timeScale;

            Assert.That(result, Is.EqualTo(1.0f));
        }

        [Test]
        public void TogglePause_SetsTimeScaleToZero()
        {
            var sut = CreateDefaultTimeController();
            
            sut.TogglePause();
            var result = Time.timeScale;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void TogglePause_GameIsPaused_SetsTimeScaleToValueBeforePause()
        {
            var sut = CreateDefaultTimeController();
            sut.TogglePause();

            sut.TogglePause();
            var result = Time.timeScale;

            Assert.That(result, Is.EqualTo(_defaultTime));
        }

        [Test]
        public void SlowDownTime_TimeScaleMinusTimeFactorIsGreaterThanMinTime_RemovesTimeFactorFromTimeScale()
        {
            var sut = CreateDefaultTimeController();

            sut.SlowDownTime();
            var result = Time.timeScale;

            Assert.That(result, Is.EqualTo(_defaultTime - _timeFactor));
        }

        [Test]
        public void SlowDownTime_TimeScaleMinusTimeFactorIsLessThanMinTime_SetsTimeScaleToMinTime()
        {
            var sut = CreateDefaultTimeController();
            Time.timeScale = _minTimeValue;

            sut.SlowDownTime();
            var result = Time.timeScale;

            Assert.That(result, Is.EqualTo(_minTimeValue));
        }

        [Test]
        public void SpeedUpTime_TimeScalePlusTimeFactorIsLessThanMaxTime_AddsTimeFactorToTimeScale()
        {
            var sut = CreateDefaultTimeController();

            sut.SpeedUpTime();
            var result = Time.timeScale;

            Assert.That(result, Is.EqualTo(_defaultTime + _timeFactor));
        }

        [Test]
        public void SpeedUpTime_TimeScalePlusTimeFactorIsGreaterThanMaxTime_SetsTimeScaleToMaxTime()
        {
            var sut = CreateDefaultTimeController();
            Time.timeScale = _maxTimeValue;

            sut.SpeedUpTime();
            var result = Time.timeScale;

            Assert.That(result, Is.EqualTo(_maxTimeValue));
        }

        [TestCase(_minTimeValue - 0.000001f)]
        [TestCase(_maxTimeValue + 0.000001f)]
        public void SetTime_OutsideTimeScaleRange_DoesNotSetTimeScaleToArgument(float timeScale)
        {
            var sut = CreateDefaultTimeController();

            sut.SetTime(timeScale);
            var result = Time.timeScale;

            Assert.That(result, Is.EqualTo(_defaultTime));
        }

        [TestCase(_minTimeValue)]
        [TestCase(_maxTimeValue)]
        public void SetTime_InsideTimeScaleRange_SetsTimeScaleToArgument(float timeScale)
        {
            var sut = CreateDefaultTimeController();

            sut.SetTime(timeScale);
            var result = Time.timeScale;

            Assert.That(result, Is.EqualTo(timeScale));
        }
    }
}
