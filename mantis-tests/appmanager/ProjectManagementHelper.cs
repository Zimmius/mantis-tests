using OpenQA.Selenium;
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

        public List<ProjectData> GetProjectsList()
        {
            List<ProjectData> projectList = new List<ProjectData>();
            manager.Navigator.GoToManagePage();
            manager.Management.GoToProjectManagementTab();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector(".table"))[0]
                .FindElements(By.CssSelector("tbody>tr"));
            foreach (IWebElement element in elements)
            {
                IList<IWebElement> cells = element.FindElements(By.CssSelector("td"));
                projectList.Add(new ProjectData(cells[0].Text) 
                {
                    Description = cells[4].Text
                });
            }
            return new List<ProjectData>(projectList);
        }

        internal void Delete(ProjectData project)
        {
            goToEditPage(project.Name);
            RemoveProject();
            SubmitRemoveProject();
        }

        private void SubmitRemoveProject()
        {
            driver.FindElement(By.CssSelector(".btn.btn-primary.btn-white.btn-round")).Click();
        }

        private void RemoveProject()
        {
            driver.FindElement(By.CssSelector(".btn.btn-primary.btn-sm.btn-white.btn-round")).Click();
        }

        private void goToEditPage(string name)
        {
            driver.FindElement(By.LinkText(name)).Click();
        }

        internal void CreateProject(ProjectData project)
        {
            manager.Navigator.GoToManagePage();
            manager.Management.GoToProjectManagementTab();

            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.CssSelector(".btn.btn-primary.btn-white.btn-round")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        private void FillProjectForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
            Type(By.Name("description"), project.Description);
        }

        private void InitProjectCreation()
        {
            driver.FindElement(By.CssSelector(".btn.btn-primary.btn-white.btn-round")).Click();
        }
    }
}
