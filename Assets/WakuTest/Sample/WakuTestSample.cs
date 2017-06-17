using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
		public IEnumerator ClickAButton_ChangeText()
		{
			yield return SetupEventSystem();
			for (int i = 1; i < 10; ++i)
			{
				yield return Button("AButton").Click();
				Assert.AreEqual(Text("AText").text, i.ToString());
			}
		}

		[UnityTest]
		public IEnumerator ClickBButton_NoChangeTexts()
		{
			yield return SetupEventSystem();
			for (int i = 1; i < 10; ++i)
			{
				yield return Button("BButton").Click();
				Assert.AreEqual(Text("AText").text, "0");
				Assert.AreEqual(Text("CText").text, "0");
			}
		}

		[UnityTest]
		public IEnumerator MonkeyTest_RandomButton()
		{
			yield return SetupEventSystem();
			for (int i = 0; i < 50; ++i)
			{
				yield return RandomButton().Click();
			}
		}

		/*
		[UnityTest]
		public IEnumerator CannotClickBButton()
		{
			yield return null;
            Button("ActiveCoverButton").Click();
			yield return null;
			for (int i = 1; i < 10; ++i)
			{
				Button("BButton").Click();
			}
		}
		*/
	}
}