using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace WakuTest.Sample
{
	public class WakuTestSample : UISceneTest
	{
		public WakuTestSample() : base("Sample")
		{
		}

		[UnityTest]
		public IEnumerator ClickAButton()
		{
			for (int i = 1; i < 10; ++i)
			{
                Button("AButton").Click();
				Assert.AreEqual(Text("AText").text, i.ToString());
				yield return null;
			}
		}
	}
}