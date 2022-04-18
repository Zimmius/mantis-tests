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

        public List<ProjectData> GetProjectsList(AccountData account)
        {
            List<ProjectData> projectList = new List<ProjectData>();
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] apiProjects = client.mc_projects_get_user_accessible(account.Name, account.Password);
            foreach (Mantis.ProjectData project in apiProjects)
            {
                projectList.Add(new ProjectData(project.name)
                {
                    Description = project.description,
                    Id = project.id
                });
            }
            return new List<ProjectData>(projectList);
        }

        public void Delete(ProjectData project)
        {
            goToEditPage(project.Name);
            RemoveProject();
            SubmitRemoveProject();
        }

        public void Delete(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            client.mc_project_delete(account.Name, account.Password, project.Id);
        }

        public void SubmitRemoveProject()
        {
            driver.FindElement(By.CssSelector(".btn.btn-primary.btn-white.btn-round")).Click();
        }

        public void RemoveProject()
        {
            driver.FindElement(By.CssSelector(".btn.btn-primary.btn-sm.btn-white.btn-round")).Click();
        }

        public void goToEditPage(string name)
        {
            driver.FindElement(By.LinkText(name)).Click();
        }

        public void CreateProject(ProjectData project)
        {
            manager.Navigator.GoToManagePage();
            manager.Management.GoToProjectManagementTab();

            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
        }

        public void CreateProject(AccountData account, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            project.description = projectData.Description;
            client.mc_project_add(account.Name, account.Password, project);
        }

        public void SubmitProjectCreation()
        {
            driver.FindElement(By.CssSelector(".btn.btn-primary.btn-white.btn-round")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public void FillProjectForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
            Type(By.Name("description"), project.Description);
        }

        public void InitProjectCreation()
        {
            driver.FindElement(By.CssSelector(".btn.btn-primary.btn-white.btn-round")).Click();
        }
    }
}
