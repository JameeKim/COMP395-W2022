using UnityEngine;
using UnityEngine.UI;

namespace Week05
{
    [DisallowMultipleComponent]
    public class TitleSetter : MonoBehaviour
    {
        [SerializeField]
        private Text titleComponent;

        [SerializeField]
        private string titleText = "Title";

        void OnEnable()
        {
            titleComponent.text = titleText;
        }
    }
}
