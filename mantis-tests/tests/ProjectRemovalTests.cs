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
            if (app.Project.GetProjectsList().Count() == 0)
            {
                app.Project.CreateProject(new ProjectData("TestProject"));
            }

            List<ProjectData> oldList = app.Project.GetProjectsList();

            ProjectData toBeRemoved = oldList[0];

            app.Project.Delete(toBeRemoved);

            List<ProjectData> newList = app.Project.GetProjectsList();

            Assert.AreEqual(oldList.Count - 1, newList.Count());
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
