using System.Collections.Generic;
using System.Linq;
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
		public IEnumerator OpenAndCloseWindow()
		{
			yield return SetupEventSystem();
			yield return Button("OpenWindowButton").Click();
			yield return Wait("CloseWindowButton");
			yield return Button("CloseWindowButton").Click();
		}

		[UnityTest]
		public IEnumerator MonkeyTest_RandomButton()
		{
			yield return SetupEventSystem();

			Debug.Log("--- MonkeyTest ---");
			for (int i = 0; i < 50; ++i)
			{
				yield return Wait(() => {
					var button = GameObject.FindObjectsOfType<Button>().Where(x => x.IsClickable()).Where(x => x.name != "RaiseExceptionButton").RandomAtOrDefault();
					if (button == null) return false;

					Debug.Log(Time.time + " Click " + button.name);
					button.Click();
					return true;
				});
			}
		}
	}
}