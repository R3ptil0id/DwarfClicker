using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    public class BaseControl : MonoBehaviour, IInitializable
    {
        public virtual void Initialize()
        {
            this.Inject();
        }
    }
}