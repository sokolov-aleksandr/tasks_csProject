using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tasks_csProject.App
{
    /// <summary>
    /// Набор методов, который должен быть у всех видов приложений
    /// </summary>
    public abstract class Application
    {
        public abstract Task RunApp();
    }
}
