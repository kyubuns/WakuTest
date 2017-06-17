using UnityEngine;
using UnityEngine.UI;
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

		public Button Button(string name)
		{
			var component = GameObject(name).GetComponent<Button>();
			Assert.NotNull(component, string.Format("GameObject({0}) is not Button", name));
			return component;
		}

		public Text Text(string name)
		{
			var component = GameObject(name).GetComponent<Text>();
			Assert.NotNull(component, string.Format("GameObject({0}) is not Text", name));
			return component;
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
		public static void Click(this Button self)
		{
			// TODO: Raycast
			self.onClick.Invoke();
		}
	}
}