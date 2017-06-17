using UnityEngine;
using UnityEngine.UI;

namespace WakuTest.Sample
{
	[RequireComponent(typeof(Button))]
	public class IncrementText : MonoBehaviour
	{
		public Text Target;
		private int Counter = 0;

		void Start()
		{
			Target.text = Counter.ToString();
			GetComponent<Button>().onClick.AddListener(() =>
			{
				Counter++;
				Target.text = Counter.ToString();
			});
		}
	}
}