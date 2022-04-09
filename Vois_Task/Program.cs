using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace Vois_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Mohamed Tharwat\\Downloads\\chromedriver_win32";
            String url = "http://automationpractice.com/index.php";
            
            //Test Data
            string testMail = "bikafor778@procowork.com";
            string fname="Muhamed";
            string lname="Tharwat";
            string passwd= "Tharwat@CCC";
            string stname1="shobra";
            string stname2= "St";
            string company="3s";
            string address="Cairo";
            string city="Cairo";
            string pcode="11311";
            string mobPhone="01123456788";
            string alias="egypt";
            string stateName = "Alabama";


            IWebDriver driver = new ChromeDriver(@path);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Url = url;

            IWebElement signInBtn = driver.FindElement(By.ClassName("login"));
            signInBtn.Click();

            IWebElement createEmailTxt = driver.FindElement(By.Id("email_create"));
            createEmailTxt.SendKeys(testMail);
            IWebElement createEmailBtn = driver.FindElement(By.CssSelector("#SubmitCreate"));
            createEmailBtn.Click();

            //to handle multiple runs with different mails as email can not be duplicated
            Thread.Sleep(4000);
            IWebElement alert = driver.FindElement(By.Id("create_account_error"));
            if(alert.Displayed)
            {
                Random rnd = new Random();
                int num = rnd.Next(200);
                testMail = string.Concat("AA"+num, testMail);
                createEmailTxt.Clear();
                createEmailTxt.SendKeys(testMail);
                createEmailBtn.Click();
            }

            
            IWebElement createform = driver.FindElement(By.Id("account-creation_form"));
            IList <IWebElement> inputs = createform.FindElements(By.TagName("input"));
            if (!inputs[0].Selected)
            {
                inputs[0].Click();
            }
            inputs[2].SendKeys(fname);
            inputs[3].SendKeys(lname);
            if(inputs[4].Text!=testMail)
            {
                inputs[4].Clear();
                inputs[4].SendKeys(testMail);
            }
            inputs[5].SendKeys(passwd);
            inputs[8].Clear();
            inputs[8].SendKeys(stname1);
            inputs[9].Clear();
            inputs[9].SendKeys(stname2);
            inputs[10].SendKeys(company);
            inputs[11].SendKeys(address);
            inputs[13].SendKeys(city);
            inputs[14].SendKeys(pcode);
            inputs[16].Clear();
            inputs[16].SendKeys(mobPhone);
            inputs[17].Clear();
            inputs[17].SendKeys(alias);

            SelectElement state = new SelectElement( driver.FindElement(By.Id("id_state")) );
            state.SelectByText(stateName);

            IWebElement registerBtn = driver.FindElement(By.CssSelector("#submitAccount > span"));
            registerBtn.Click();

            IWebElement womenTab = driver.FindElement(By.ClassName("sf-with-ul"));
            Actions a = new Actions(driver);
            a.MoveToElement(womenTab).Perform();
            IWebElement cateBtn = driver.FindElement(By.CssSelector("[title='Blouses']"));
            cateBtn.Click();
            IWebElement addCartBtn = driver.FindElement(By.CssSelector("[title='Add to cart'] > span"));
            addCartBtn.Click();





        }
    }
}


