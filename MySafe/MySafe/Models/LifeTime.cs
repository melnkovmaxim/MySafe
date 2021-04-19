using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImTools;
using MySafe.Core.Commands;

namespace MySafe.Presentation.Models
{
    public class LifeTime: BindableBase
    {
        public TimeSpan LifeTimeSpan { get; }
        public DateTime DeathTime { get; private set; }
        public bool IsDead { get; private set; }
        public string RemainingLifeTimeMessage { get; set; }
        public TimeSpan RemainingTime => DeathTime - DateTime.Now;

        private string GetMessage()
        {
            var remainingMinutes = RemainingTime.Minutes.ToString("00");
            var remainingSeconds = RemainingTime.Seconds.ToString("00");

            return $"Осталось {remainingMinutes}:{remainingSeconds}";
        }

        public void UpdateMessage()
        {
            RemainingLifeTimeMessage = GetMessage();
            IsDead = DateTime.Now > DeathTime;
        }

        public void Restart()
        {
            DeathTime = DateTime.Now.Add(LifeTimeSpan);
            UpdateMessage();
        }

        public LifeTime(TimeSpan lifeTimeSpan)
        {
            LifeTimeSpan = lifeTimeSpan;
            Restart();
        }
    }
}
