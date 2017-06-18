using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WakuTest
{
	public class Monkey : MonoBehaviour
	{
		private float LastButtonClicked;

		public void Start()
		{
			StartCoroutine(Run());
		}

		public IEnumerator Run()
		{
			LastButtonClicked = Time.time;
			while (LastButtonClicked + 1.0 > Time.time)
			{
				yield return RandomScroll();
				yield return ClickRandomButton();
			}
			Debug.LogError("LastButtonClicked + 1.0 < Time.time");
		}

		private IEnumerator RandomScroll()
		{
			var scrollRects = GameObject.FindObjectsOfType<ScrollRect>();
			foreach (var scrollRect in scrollRects)
			{
				scrollRect.verticalNormalizedPosition = Random.Range(0.0f, 1.0f);
				scrollRect.horizontalNormalizedPosition = Random.Range(0.0f, 1.0f);
			}
			yield return null;
		}

		private IEnumerator ClickRandomButton()
		{
			var button = GameObject.FindObjectsOfType<Button>().Where(x => x.IsClickable()).RandomAtOrDefault();
			if (button == null) yield break;

			LastButtonClicked = Time.time;
			Debug.Log("[Monkey] Click " + button.name);
			button.Click();
			yield return null;
		}
	}
}