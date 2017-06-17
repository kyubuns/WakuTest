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
			yield return SetupEventSystemAsync();
			for (int i = 1; i < 10; ++i)
			{
				Button("AButton").Click();
				Assert.AreEqual(Text("AText").text, i.ToString());
			}
		}

		[UnityTest]
		public IEnumerator ClickBButton_NoChangeTexts()
		{
			yield return SetupEventSystemAsync();
			for (int i = 1; i < 10; ++i)
			{
				Button("BButton").Click();
				Assert.AreEqual(Text("AText").text, "0");
				Assert.AreEqual(Text("CText").text, "0");
			}
		}

		[UnityTest]
		public IEnumerator OpenAndCloseWindow()
		{
			yield return SetupEventSystemAsync();
			Button("OpenWindowButton").Click();
			yield return WaitAsync("CloseWindowButton");
			Button("CloseWindowButton").Click();
		}

		[UnityTest]
		public IEnumerator MonkeyTest_RandomButton()
		{
			yield return SetupEventSystemAsync();

			Debug.Log("--- MonkeyTest ---");
			for (int i = 0; i < 50; ++i)
			{
				yield return WaitAsync(() => {
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