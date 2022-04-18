using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            if (app.Project.GetProjectsList(account).Count() == 0)
            {
                app.Project.CreateProject(account, new ProjectData("TestProject"));
            }

            List<ProjectData> oldList = app.Project.GetProjectsList(account);

            ProjectData toBeRemoved = oldList[0];

            app.Project.Delete(account, toBeRemoved);

            List<ProjectData> newList = app.Project.GetProjectsList(account);

            Assert.AreEqual(oldList.Count - 1, newList.Count());
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
