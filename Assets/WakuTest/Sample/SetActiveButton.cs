using UnityEngine;
using UnityEngine.UI;

namespace WakuTest.Sample
{
	[RequireComponent(typeof(Button))]
	public class SetActiveButton : MonoBehaviour
	{
		public GameObject Target;
		public bool Active;

		void Start()
		{
			GetComponent<Button>().onClick.AddListener(() =>
			{
				Target.SetActive(Active);
			});
		}
	}
}