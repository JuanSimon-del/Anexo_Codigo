using System.Collections.Generic;
using System.Linq;

namespace SimulacionTFI.Aplication.Core
{
    public class EventCalendar
    {
        private List<Event> _events;

        public EventCalendar()
        {
            _events = new List<Event>();
        }

        /// <summary>
        /// Agrega un nuevo evento al calendario y ordena la lista.
        /// El evento que ocurra más temprano (menor valor en DÍAS) quedará primero.
        /// </summary>
        public void AddEvent(Event newEvent)
        {
            _events.Add(newEvent);
            _events = _events.OrderBy(e => e.EventTime).ToList();
        }

        /// <summary>
        /// Saca y devuelve el evento más próximo a ocurrir.
        /// </summary>
        public Event GetNextEvent()
        {
            if (_events.Count == 0)
                return null;

            var nextEvent = _events.First();
            _events.RemoveAt(0); // Lo borramos porque ya va a ser procesado

            return nextEvent;
        }

        public bool HasEvents()
        {
            return _events.Count > 0;
        }
    }
}