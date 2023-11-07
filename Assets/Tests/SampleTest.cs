using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SampleTest
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(4, 4)]
    [TestCase(15, 15)]
    [TestCase(32, 32)]
    [TestCase(64, 64)]
    [TestCase(128, 128)]
    public void TickEngineBehaviourTest(int tickRate, int expectedTickCount)
    {
        float delta = 1f/144f;
        int tickCount = 0;
        TickEngine tickEngine = new TickEngine(tickRate);

        tickEngine.OnTick += () => tickCount++;

        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            tickEngine.UpdateTicks(delta);
            elapsedTime += delta;
        }
        Assert.AreEqual(expectedTickCount, tickCount);
    }
}
