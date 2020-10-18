using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Sandbox
    {
        private float GetNewRotation(float oldRot) => (oldRot - 90f) % 360f;

        [Test]
        public void RotationIncreaseBy90() {
            var rot = 0f;
            rot = GetNewRotation(rot);
            Assert.That(rot, Is.EqualTo(-90f));
            rot = GetNewRotation(rot);
            Assert.That(rot, Is.EqualTo(-180f));
            rot = GetNewRotation(rot);
            Assert.That(rot, Is.EqualTo(-270f));
            rot = GetNewRotation(rot);
            Assert.That(rot, Is.EqualTo(0f));
            rot = GetNewRotation(rot);
            Assert.That(rot, Is.EqualTo(-90f));
        }

    }
}
