using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using TestContext = NUnit.Framework.TestContext;

namespace mantis_tests
{
    /// <summary>
    /// Сводное описание для AccountCreationTests
    /// </summary>
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void setUpConfig()
        {
            app.Ftp.BackupFile("config/config_inc.php");
            using (Stream localFaile = File.Open(TestContext.CurrentContext.TestDirectory + "/config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("config/config_inc.php", localFaile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain"
            };

            app.Registration.Register(account);
        }

        [OneTimeTearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackupFile("config/config_inc.php");
        }
    }
}
