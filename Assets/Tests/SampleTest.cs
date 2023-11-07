using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SampleTest
{
    [UnityTest]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(4, 4)]
    public IEnumerator SampleTestSimplePasses(int tickRate, int expectedTickCount)
    {
        int tickCount = 0;
        TickEngine tickEngine = new TickEngine(tickRate);

        tickEngine.OnTick += () => tickCount++;

        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            tickEngine.UpdateTicks(Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Assert.AreEqual(expectedTickCount, tickCount);
    }
}
