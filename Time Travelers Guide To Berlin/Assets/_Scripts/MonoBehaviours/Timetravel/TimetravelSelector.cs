namespace UnityEngine.UI.Extensions.Examples
{
    public class TimetravelSelector : MonoBehaviour
    {

        public bool debug;

        public void SelectionStartChange()
        {
            if (debug) 
            Debug.Log("Scroll Snap change started");
        }
        public void SelectionEndChange()
        {
            if (debug) 
            Debug.Log("Scroll Snap change finished");
        }
        public void PageChange(int page)
        {
            if (debug) 
            Debug.Log(string.Format("Scroll Snap page changed to {0}", page));
        }
    }
}