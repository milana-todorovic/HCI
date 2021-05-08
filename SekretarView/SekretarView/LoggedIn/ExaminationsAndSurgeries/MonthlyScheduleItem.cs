using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SekretarView
{
    class MonthlyScheduleItem
    {
        private DateTime date;
        private int numberOfEvents;
        private Boolean isToday;
        private Boolean isCorrectMonth;

        public DateTime Date { get => date; set => date = value; }
        public int NumberOfEvents { get => numberOfEvents; set => numberOfEvents = value; }
        public bool IsToday { get => isToday; set => isToday = value; }
        public bool IsCorrectMonth { get => isCorrectMonth; set => isCorrectMonth = value; }
    }
}
