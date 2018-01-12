using IsoUnity.Sequences;

namespace IsoUnity.Events {
	public class IsoSwitchesEventManager : EventedEventManager {

        public override void ReceiveEvent (IGameEvent ev)
		{
            base.ReceiveEvent(ev);
            // Old name support
            if (ev.Name == "ChangeSwitch")
            {
                object value = ev.getParameter("value");
                string iswitch = (string) ev.getParameter("switch");
                this.ChangeSwitch(iswitch, value);
            }
		}

        [GameEvent(true, false)]
        public void ChangeSwitch(string @switch, object value)
        {
            // When there is a sequence we try to save it as local var but if not, we save it as global
            if (Sequence.current != null
                && (Sequence.current.ContainsVariable(@switch) || !IsoSwitchesManager.getInstance().getIsoSwitches().containsSwitch(@switch)))
            {
                // Save as local
                Sequence.current.SetVariable(@switch, value);
            }
            else
            {
                // Save as global
                IsoSwitchesManager.getInstance().getIsoSwitches().getSwitch(@switch).State = value;
            }
        }
	}
}