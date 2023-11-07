using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TickEngineTest
{


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(4, 4)]
    public IEnumerator TickEngineBehaviourTest(int tickRate, int expectedTickCount)
    {
        int tickCount = 0;
        TickEngine tickEngine = new TickEngine(tickRate);

        tickEngine.OnTick += () => tickCount++;

        float elapsedTime = 0;
        while (elapsedTime <= 1f)
        {
            tickEngine.UpdateTicks(Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
        Assert.AreEqual(expectedTickCount, tickCount);

    }
}
