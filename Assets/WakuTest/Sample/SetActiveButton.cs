using UnityEngine;
using UnityEngine.UI;

namespace WakuTest.Sample
{
	[RequireComponent(typeof(Button))]
	public class SetActiveButton : MonoBehaviour
	{
		public GameObject Target;
		public float Delay;
		public bool Active;

		void Start()
		{
			GetComponent<Button>().onClick.AddListener(() =>
			{
				Invoke("OnClick", Delay);
			});
		}

		public void OnClick()
		{
			Target.SetActive(Active);
		}
	}
}