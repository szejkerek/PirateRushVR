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
    public void TickEngineTestSimplePasses()
    {
        Assert.True(true);
    }
}
