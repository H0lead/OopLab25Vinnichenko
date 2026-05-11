using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Lab25
{
    // Опис класу Sport, так званий "шаблон", який зручно використовувати для передачі даних у методи.

    // Має рядок "name"
    // Рядок "description"
    // Логічна зміна "isOlympic"

    // Є також конструктор для створення об'єкта класу.
    internal class Sport
    {
        private string name;
        private string description;
        private bool isOlympic;

        public Sport(string name, string description, bool isOlympic) 
        {
            this.name = name;
            this.description = description;
            this.isOlympic = isOlympic;
        }

        public string Name {  get { return name; } }
        public string Description { get { return description; } }
        public bool IsOlympic { get { return isOlympic; } }
    }
}
