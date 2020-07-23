using System.Linq;
using UnityEngine;

namespace Artees.BDD
{
    public class UnityShouldListenerInitializer : MonoBehaviour
    {
        private readonly UnityShouldListener _listener = new UnityShouldListener();
        
        private void Awake()
        {
            if (ShouldAssertions.Listeners.Any(i => i is UnityShouldListener)) return;
            ShouldAssertions.Listeners.Add(_listener);
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            _listener.Dispose();
        }
    }
}