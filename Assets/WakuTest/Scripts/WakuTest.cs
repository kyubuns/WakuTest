using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace WakuTest
{
	public abstract class UISceneTest
	{
		private string SceneName;

		public UISceneTest(string sceneName)
		{
			SceneName = sceneName;
		}

		[SetUp]
		public void LoadScene()
		{
			SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
		}

		[TearDown]
		public void UnloadScene()
		{
			SceneManager.UnloadSceneAsync("Sample");
		}

		public static IEnumerator SetupEventSystem()
		{
			yield return null;
		}

		public Button Button(string name)
		{
			var component = GameObject(name).GetComponentInParent<Button>();
			Assert.NotNull(component, string.Format("GameObject({0}) is not Button", name));
			return component;
		}

		public Text Text(string name)
		{
			var component = GameObject(name).GetComponentInParent<Text>();
			Assert.NotNull(component, string.Format("GameObject({0}) is not Text", name));
			return component;
		}

		public Button[] GetAllClickableButton()
		{
			return UnityEngine.GameObject.FindObjectsOfType<Button>().Where(x => Extensions.CanClick(x)).ToArray();
		}

		public IEnumerator Find(string name)
		{
			var startTime = Time.time;
			while (true)
			{
				var go = UnityEngine.GameObject.Find(name);
				if (go) yield break;
				if (startTime + 1.0 < Time.time)
				{
					Assert.Fail("GameObject({0}) can not found", name);
					yield break;
				}
				yield return null;
			}
		}

		public GameObject GameObject(string name)
		{
			var go = UnityEngine.GameObject.Find(name);
			Assert.NotNull(go, string.Format("GameObject({0}) not found", name));
			return go;
		}
	}

	public static class Extensions
	{
		public static IEnumerator Click(this Button self)
		{
			Assert.IsTrue(self.gameObject.activeInHierarchy, string.Format("Button({0}) is inactive", self.name));
			Assert.IsTrue(self.interactable, string.Format("Button({0}) is not interactable", self.name));
			CanClick(self, withAssert: true);
			self.onClick.Invoke();
			yield return null;
		}

		public static bool CanClick(Button button, bool withAssert = false)
		{
			var canvas = button.GetComponentInParent<Canvas>();

			// TODO: 本当は5箇所ぐらいでチェックしたい
			// (実は1ドットだけ押せるみたいなバグの検知)
			var rect = button.GetComponent<RectTransform>();
			var center = rect.position;
			var pos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, center);

			var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
			eventDataCurrentPosition.position = pos;
			var results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

			var result = (results[0].gameObject == button.gameObject);

			if (withAssert && !result)
			{
				Assert.Fail("Button({0}) can not touch(over {1})", button.name, results[0].gameObject.name);
			}

			return result;
		}
	}
}
