using System;
using UnityEngine;
using UnityEngine.UI;

namespace WakuTest.Sample
{
	[RequireComponent(typeof(Button))]
	public class RaiseExceptionButton : MonoBehaviour
	{
		void Start()
		{
			GetComponent<Button>().onClick.AddListener(() =>
			{
				throw new Exception("click raise exception button");
			});
		}
	}
}