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
			Assert.NotNull(component, "GameObject({0}) is not Button", name);
			return component;
		}

		public Text Text(string name)
		{
			var component = GameObject(name).GetComponentInParent<Text>();
			Assert.NotNull(component, "GameObject({0}) is not Text", name);
			return component;
		}

		public IEnumerator Wait(Func<bool> action, float timeout = 1.0f)
		{
			var startTime = Time.time;
			while (startTime + timeout > Time.time)
			{
				if (action()) yield break;
				yield return null;
			}
			Assert.Fail("Wait Timeout");
		}

		public IEnumerator Wait(string name, float timeout = 1.0f)
		{
			var startTime = Time.time;
			while (startTime + timeout > Time.time)
			{
				var go = UnityEngine.GameObject.Find(name);
				if (go) yield break;
				yield return null;
			}
			Assert.Fail("Wait GameObject({0}) Timeout", name);
		}

		private GameObject GameObject(string name)
		{
			var go = UnityEngine.GameObject.Find(name);
			Assert.NotNull(go, "GameObject({0}) not found", name);
			return go;
		}
	}

	public static class Extensions
	{
		public static IEnumerator Click(this Button self)
		{
			Assert.IsNotNull(self, "Button is null");
			self.IsClickable(withAssert: true);
			self.onClick.Invoke();
			yield return null;
		}

		public static T RandomAt<T>(this IEnumerable<T> self)
		{
			return self.ElementAt(UnityEngine.Random.Range(0, self.Count()));
		}

		public static T RandomAtOrDefault<T>(this IEnumerable<T> self)
		{
			return self.Count() == 0 ? default(T) : RandomAt(self);
		}

		public static bool IsClickable(this Button button, bool withAssert = false)
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

			var raycastResult = (results[0].gameObject.GetComponentInParent<Button>() == button);

			if (withAssert)
			{
				Assert.IsTrue(raycastResult, "Button({0}) can not touch(over {1})", button.name, results[0].gameObject.name);
				Assert.IsTrue(button.gameObject.activeInHierarchy, "Button({0}) is inactive", button.name);
				Assert.IsTrue(button.interactable, "Button({0}) is not interactable", button.name);
			}

			if (!raycastResult) return false;
			if (!button.gameObject.activeInHierarchy) return false;
			if (!button.interactable) return false;

			return true;
		}
	}
}
