using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            ProjectData project = new ProjectData("TestProject")
            {
                Description = "TestProjectCreationTestAPI"
            };

            foreach (ProjectData item in app.Project.GetProjectsList(account))
            {
                if (item.Name == project.Name)
                {
                    app.Project.Delete(account, item);
                }
            }
            List<ProjectData> oldList = app.Project.GetProjectsList(account);

            app.Project.CreateProject(account, project);

            List<ProjectData> newList = app.Project.GetProjectsList(account);

            Assert.AreEqual(oldList.Count + 1, newList.Count());
            oldList.Add(project);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);


        }
    }
}
