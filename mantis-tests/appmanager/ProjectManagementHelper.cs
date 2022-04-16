using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) 
            : base(manager)
        {
        }

        internal List<ProjectData> GetProjectsList()
        {
            throw new NotImplementedException();
        }

        internal void Delete(ProjectData item)
        {
            throw new NotImplementedException();
        }

        internal void CreateProject(ProjectData project)
        {
            throw new NotImplementedException();
        }
    }
}
