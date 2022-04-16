using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectData
    {
        public ProjectData (string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string State { get; set; }

        public bool InheritGlobal { get; set; }

        public string ViewState { get; set; }

        public string Description { get; set; }
    }
}
